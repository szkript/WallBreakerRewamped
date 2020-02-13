using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WallBreaker2.GameObjects;

namespace WallBreaker2
{
    public class Game
    {
        private int Score;
        public Paddle paddle;

        private bool Paused { get; set; } = false;

        public Game(Canvas wallbreakerCanvas)
        {
            InitGameComponents(wallbreakerCanvas);
        }

        private void InitGameComponents(Canvas wallbreakerCanvas)
        {
            paddle = new Paddle(150, 15, wallbreakerCanvas.Width);

            paddle.paddle.SetValue(Canvas.LeftProperty, wallbreakerCanvas.Width / 2 - paddle.Width / 2);
            paddle.paddle.SetValue(Canvas.TopProperty, (double)wallbreakerCanvas.Height - paddle.Height);
            wallbreakerCanvas.Children.Add(paddle.paddle);
        }

        internal void Start()
        {
            Score = 0;
            //InitBricks(rowOfBricks);
            //int ballStartingVerticalPosition = rowOfBricks * 37;
            //ball = new GameBall(Ball, PongCanvas.ActualWidth, PongCanvas.ActualHeight, ballStartingVerticalPosition);

            GameTimeManager.GameTime(GameTime_Tick);
            GameTimeManager.StartGame(GameLoop);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            if (Paused) { return; }

            //CheckCollusion();
            //ball.Move();
            paddle.MovePaddle();
            //UpdateLiveScore();
        }
        public void TogglePause(GameState pauseState)
        {
            //MessageBoxResult result = MessageBox.Show("Email sent! \n Would you like to write an other email?", "New Email", MessageBoxButton.YesNo);
            //switch (result)
            //{
            //    case MessageBoxResult.Yes:
            //        EmailRecipientTxt.Text = "";
            //        EmailSubjectTxt.Text = "";
            //        EmailBodyTxt.Text = "";
            //        return;
            //    case MessageBoxResult.No:
            //        Close();
            //        break;
            //}
            switch (pauseState)
            {
                case GameState.Exit:
                    break;
                case GameState.SimplePause:
                    TogglePause();
                    MessageBox.Show("Your Score :" + Score);
                    TogglePause();
                    break;
                case GameState.GameOver:
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

    }
}
