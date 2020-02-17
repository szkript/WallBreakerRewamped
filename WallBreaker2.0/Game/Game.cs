using System;
using System.Collections.Generic;
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
        public Game(Canvas wallbreakerCanvas)
        {
            WallbreakerCanvas = wallbreakerCanvas;
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
        public void CheckCollusion()
        {
            string ballHeight = ball.Position.Y < offset ? "upper" : "lower";
            switch (ballHeight)
            {
                case ("upper"):

                    break;
                case ("lower"):

                    break;
                default:
              break;
            }
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
                    break;
                case GameState.Restart:
                    StopGame();
                    WallbreakerCanvas.Children.Clear();
                    Start();
                    TogglePause();
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
                    posLeft += brick.Rectangle.Width + 5;
                }
                posTop += 30;
            }
        }
    }
}
