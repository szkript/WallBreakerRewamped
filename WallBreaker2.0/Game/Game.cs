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
        private int Score;
        public Paddle paddle;
        public Ball ball;
        private int RowOfBricks = 2;
        private int offset = 50;
        private bool Paused { get; set; } = false;
        private Canvas WallbreakerCanvas;
        private List<Brick> Bricks { get; set; }
        public Game(Canvas wallbreakerCanvas)
        {
            WallbreakerCanvas = wallbreakerCanvas;
        }

        private void InitGameComponents()
        {
            // Create paddle
            paddle = new Paddle(150, 15, WallbreakerCanvas.Width);
            paddle.paddle.SetValue(Canvas.LeftProperty, WallbreakerCanvas.Width / 2 - paddle.Width / 2);
            paddle.paddle.SetValue(Canvas.TopProperty, (double)WallbreakerCanvas.Height - paddle.Height);
            // Create Ball
            WallbreakerCanvas.Children.Add(paddle.paddle);
            ball = new Ball(20, 20, WallbreakerCanvas);
            ball.ball.SetValue(Canvas.LeftProperty, (double)ball.Position.X);
            ball.ball.SetValue(Canvas.TopProperty, (double)ball.Position.Y + offset);
            WallbreakerCanvas.Children.Add(ball.ball);
            // Create bricks
            // Todo: create and init bricks
            InitBricks(RowOfBricks);
        }

        internal void Start()
        {
            Score = 0;
            InitGameComponents();
            //InitBricks(rowOfBricks);
            //int ballStartingVerticalPosition = rowOfBricks * 37;

            GameTimeManager.GameTime(GameTime_Tick);
            GameTimeManager.StartGame(GameLoop);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            if (Paused) { return; }

            //CheckCollusion();
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
                    Brick brick = new Brick(50,20,position);
                    Canvas.SetLeft(brick.brick, (double)brick.Position.X);
                    Canvas.SetTop(brick.brick, (double)brick.Position.Y);
                    Bricks.Add(brick);
                    WallbreakerCanvas.Children.Add(brick.brick);
                    posLeft += brick.brick.Width + 5;
                }
                posTop += 30;
            }
        }
    }
}
