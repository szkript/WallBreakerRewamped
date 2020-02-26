using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;
using WallBreaker2.GameObjects;


namespace WallBreaker2.GameData
{
    public class Game
    {
        private List<Brick> Bricks { get; set; }
        public Paddle paddle;
        public Ball ball;
        private double offset;
        private bool Paused { get; set; } = false;
        private Canvas WallbreakerCanvas;
        private Canvas MenuCanvas;
        private int RowOfBricks = 6;
        private double CollusionRange = 7;
        private List<Brick> PossibleBricksInRange = new List<Brick>();

        public Game(Canvas wallbreakerCanvas, Canvas menuCanvas)
        {
            WallbreakerCanvas = wallbreakerCanvas;
            MenuCanvas = menuCanvas;
            InitMenu();
        }
        private void InitMenu()
        {
            StackPanel menu = (StackPanel)MenuCanvas.FindName("MenuPanel");
            Button button = new Button();
            button.Name = "SinglePlayer";
            button.Content = "Single Player";
            button.Click += new RoutedEventHandler(StartButton_Click);
            menu.Children.Add(button);
        }
        private void StartGame()
        {
            MenuCanvas.Visibility = Visibility.Hidden;
            InitGameComponents();

            GameTimeManager.GameTime(GameTime_Tick);
            GameTimeManager.StartGame(GameLoop);
        }
        void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        public void CheckCollusion()
        {
            Side ballHeight = ball.Position.Y < offset ? Side.Top : Side.Bottom;
            switch (ballHeight)
            {
                case (Side.Top):
                    BrickRangeChecker();
                    ReRenderCanvas();
                    break;
                case (Side.Bottom):
                    if (ContactsWithPaddle()) { ball.InverseDirection(paddle); }
                    if (ContactsWithFloor()) { TogglePause(GameState.GameOver); }
                    break;
                default:
                    break;
            }
        }
        private void ReRenderCanvas()
        {
            WallbreakerCanvas.Children.Clear();
            foreach (Brick brick in Bricks)
            {
                WallbreakerCanvas.Children.Add(brick.Rectangle);
            }
            WallbreakerCanvas.Children.Add(paddle.Rectangle);
            WallbreakerCanvas.Children.Add(ball.Rectangle);

            if (Bricks.Count == 0) { TogglePause(GameState.Win); }
        }
        private void BrickRangeChecker()
        {
            List<int> ballTopAndBotSide = Enumerable.Range((int)ball.Position.X, (int)ball.Width).ToList();
            List<int> ballLeftAndRightSide = Enumerable.Range((int)ball.Position.Y, (int)ball.Height).ToList();

            foreach (Brick brick in Bricks)
            {
                // ball top vs brick bottom
                if (brick.Position.Y + brick.Height + CollusionRange >= ball.Position.Y && brick.Position.Y + brick.Height - CollusionRange <= ball.Position.Y &&
                    ballTopAndBotSide.Any(x => brick.Position.X <= x && x <= brick.Position.X + brick.Width))
                {
                    Console.WriteLine("brick Bottom contact");
                    PossibleBricksInRange.Add(brick);
                }
                // ball bot vs brick top
                else if (brick.Position.Y + CollusionRange >= ball.Position.Y + ball.Height && ball.Position.Y + ball.Height >= brick.Position.Y - CollusionRange &&
                    ballTopAndBotSide.Any(x => brick.Position.X <= x && x <= brick.Position.X + brick.Width))
                {
                    Console.WriteLine("brick Top contact");
                    PossibleBricksInRange.Add(brick);
                }
                // ball left side vs brick right side (y)
                else if (brick.Position.X - CollusionRange <= ball.Position.X + ball.Width && ball.Position.X + ball.Width <= brick.Position.X + CollusionRange &&
                    ballLeftAndRightSide.Any(y => brick.Position.Y <= y && y <= brick.Position.Y + brick.Height))
                {
                    Console.WriteLine("Brick left contact");
                    PossibleBricksInRange.Add(brick);
                }
                // ball right side vs brick left side (y)
                else if (brick.Position.X + brick.Width - CollusionRange <= ball.Position.X && ball.Position.X <= brick.Position.X + brick.Width + CollusionRange &&
                    ballLeftAndRightSide.Any(y => brick.Position.Y <= y && y <= brick.Position.Y + brick.Height))
                {
                    Console.WriteLine("Brick right contact");
                    PossibleBricksInRange.Add(brick);
                }
            }
        }
        private bool ContactsWithPaddle()
        {
            if (ball.Position.Y + ball.Height >= paddle.Position.Y)
            {
                if (paddle.Position.X < ball.Position.X && ball.Position.X < paddle.Position.X + paddle.Width)
                {
                    return true;
                }
            }
            return false;
        }
        private bool ContactsWithFloor()
        {
            return ball.Position.Y + ball.Height >= WallbreakerCanvas.Height - paddle.Height / 2;
        }
        private void InitGameComponents()
        {
            offset = RowOfBricks * 31;
            // Create paddle
            paddle = new Paddle(150, 15, WallbreakerCanvas);
            // Create Ball
            ball = new Ball(10, 10, WallbreakerCanvas, offset);
            // Create bricks
            InitBricks(RowOfBricks);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            if (Paused) { return; }

            CheckCollusion();
            if (PossibleBricksInRange.Count > 0)
            {
                Brick contacted = null;
                int distance = Convert.ToInt32(Vector2.Distance(ball.Position, ball.PeekingMove()));
                ball.SetSpeed(1);
                for (int i = 0; i < distance; i++)
                {
                    ball.Move();
                    if (contacted == null)
                    {
                        contacted = ContactBrick();
                    }
                }
                ball.SetSpeed(GameSettings.BallBaseSpeed);
                if (contacted != null)
                {
                    Bricks.Remove(contacted);
                    PossibleBricksInRange.Clear();
                }
            }
            else
            {
                ball.Move();
            }
            paddle.MovePaddle();
        }

        private Brick ContactBrick()
        {
            List<int> ballTopAndBotSide = Enumerable.Range((int)ball.Position.X, (int)ball.Width).ToList();
            List<int> ballLeftAndRightSide = Enumerable.Range((int)ball.Position.Y, (int)ball.Height).ToList();

            foreach (Brick brick in PossibleBricksInRange)
            {
                if ((int)ball.Position.Y == brick.Position.Y + brick.Height &&
                    ballTopAndBotSide.Any(x => brick.Position.X <= x && x <= brick.Position.X + brick.Width))
                {
                    Console.WriteLine("bb");
                    ball.InverseDirection(Axis.Y);
                    return brick;
                }
                else if ((int)ball.Position.Y + ball.Height == brick.Position.Y &&
                    ballTopAndBotSide.Any(x => brick.Position.X <= x && x <= brick.Position.X + brick.Width))
                {
                    Console.WriteLine("bt");
                    ball.InverseDirection(Axis.Y);
                    return brick;
                }
            }
            return null;
        }

        public void TogglePause(GameState pauseState)
        {
            TogglePause();
            switch (pauseState)
            {
                case GameState.Exit:
                    //TODO: expand/create custom form for msgButtons(restart)
                    MessageBoxResult result = MessageBox.Show("Would you like to exit?", "Exit Game", MessageBoxButton.YesNo);
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            StopGame();
                            Application.Current.MainWindow.Close();
                            break;
                        case MessageBoxResult.No:
                            TogglePause();
                            break;
                    }
                    break;
                case GameState.SimplePause:
                    MessageBox.Show("Your Score :");
                    TogglePause();
                    break;
                case GameState.GameOver:
                    StopGame();
                    MessageBox.Show("Meghattá'");
                    Paused = false;
                    break;
                case GameState.Restart:
                    RestartGame();
                    break;
                case GameState.Win:
                    MessageBox.Show("Kurvajóvagy");
                    break;
                default:
                    TogglePause();
                    break;
            }
        }

        private void RestartGame()
        {
            StopGame();
            WallbreakerCanvas.Children.Clear();
            GameTimeManager.StopAllTimer();
            StartGame();
            TogglePause();
        }

        private void TogglePause()
        {
            if (Paused == false)
            {
                Paused = true;
            }
            else if (Paused == true)
            {
                Paused = false;
            }
        }
        private void StopGame()
        {
            GameTimeManager.StopGame();
        }
        private void GameTime_Tick(object sender, EventArgs e)
        {
            if (!Paused)
            {
                Mouse.OverrideCursor = Cursors.None;
            }
            else
            {
                Mouse.OverrideCursor = Cursors.AppStarting;
            }
        }
        private void InitBricks(int NumOfRows)
        {
            Bricks = new List<Brick>();
            // normal val = 5
            double posTop = 5;
            for (int i = 0; i < NumOfRows; i++)
            {
                double posLeft = 5;
                while (WallbreakerCanvas.Width > posLeft + 50)
                {
                    Vector2 position = new Vector2((int)posLeft, (int)posTop);
                    Brick brick = new Brick(50, 20, position, WallbreakerCanvas);
                    Bricks.Add(brick);
                    posLeft += brick.Rectangle.Width + 1;
                }
                posTop += 20;
            }
        }
    }
}
