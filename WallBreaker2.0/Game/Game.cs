using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WallBreaker2.GameObjects;

namespace WallBreaker2.GameData
{
    public class Game
    {
        private List<Brick> Bricks { get; set; }
        public Paddle paddle;
        public Ball ball;
        private int Score;
        private double offset;
        private bool Paused { get; set; } = false;
        private Canvas WallbreakerCanvas;
        private int RowOfBricks = 6;
        private Brick removableBrick;

        public Game(Canvas wallbreakerCanvas)
        {
            WallbreakerCanvas = wallbreakerCanvas;
        }
        public void CheckCollusion()
        {

            Side ballHeight = ball.Position.Y < offset ? Side.Top : Side.Bottom;
            switch (ballHeight)
            {
                case (Side.Top):
                    if (ContactsWithBrick())
                    {
                        ball.InverseDirection(removableBrick);
                        ReRenderCanvas();
                    }
                    break;
                case (Side.Bottom):
                    if (ContactsWithPaddle()) { ball.InverseDirection(paddle); }
                    //if (ContactsWithFloor()) { TogglePause(GameState.GameOver); }
                    break;
                default:
                    break;
            }
        }

        private void ReRenderCanvas()
        {

            if (removableBrick != null)
            {
                Bricks.Remove(removableBrick);
                WallbreakerCanvas.Children.Clear();
                foreach (Brick brick in Bricks)
                {
                    WallbreakerCanvas.Children.Add(brick.Rectangle);
                }
                WallbreakerCanvas.Children.Add(paddle.Rectangle);
                WallbreakerCanvas.Children.Add(ball.Rectangle);
            }
            if (Bricks.Count == 0) { TogglePause(GameState.Win); }
        }

        private bool ContactsWithBrick()
        {
            removableBrick = null;
            foreach (Brick brick in Bricks)
            {
                if (ball.Position.Y <= brick.Position.Y + brick.Height)
                {
                    List<int> brickBottomSide = Enumerable.Range((int)brick.Position.X, (int)brick.Width).ToList();
                    List<int> ballTopSide = Enumerable.Range((int)ball.Position.X, (int)ball.Width).ToList();

                    if (brickBottomSide.Any(x => ballTopSide.Contains(x)))
                    {
                        removableBrick = brick;
                        return true;
                    }
                }
            }
            return false;
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
        internal void Start()
        {
            Score = 0;
            InitGameComponents();

            GameTimeManager.GameTime(GameTime_Tick);
            GameTimeManager.StartGame(GameLoop);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            if (Paused) { return; }

            CheckCollusion();
            ball.Move();
            paddle.MovePaddle();
            //UpdateLiveScore();
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
                    MessageBox.Show("Your Score :" + Score);
                    TogglePause();
                    break;
                case GameState.GameOver:
                    StopGame();
                    MessageBox.Show("Meghattá'");
                    Paused = false;
                    break;
                case GameState.Restart:
                    StopGame();
                    WallbreakerCanvas.Children.Clear();
                    Start();
                    GameStatusEffect.SlowMotionIsReady = true;
                    TogglePause();
                    break;
                case GameState.Win:
                    MessageBox.Show("Kurvajóvagy");
                    break;
                default:
                    TogglePause();
                    break;
            }
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
