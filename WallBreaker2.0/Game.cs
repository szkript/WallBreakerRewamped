using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using WallBreaker2.GameObjects;

namespace WallBreaker2
{
    public class Game
    {
        private int Score;
        public Paddle paddle;
        public Game(System.Windows.Controls.Canvas pongCanvas)
        {
            paddle = new Paddle(150, 15, pongCanvas.Width);

            paddle.paddle.SetValue(Canvas.LeftProperty, pongCanvas.Width/2 - paddle.Width/2);
            paddle.paddle.SetValue(Canvas.TopProperty, (double)pongCanvas.Height - paddle.Height);
            pongCanvas.Children.Add(paddle.paddle);

        }

        internal void Start()
        {
            Score = 0;
            //InitBricks(rowOfBricks);
            //int ballStartingVerticalPosition = rowOfBricks * 37;
            //ball = new GameBall(Ball, PongCanvas.ActualWidth, PongCanvas.ActualHeight, ballStartingVerticalPosition);

            //GameTimeManager.GameTime(DispatcherTimer_Tick);
            GameTimeManager.StartGame(GameLoop);
        }
        private void GameLoop(object sender, EventArgs e)
        {
            //if (Paused) { return; }

            //CheckCollusion();
            //ball.Move();
            paddle.MovePaddle();
            //UpdateLiveScore();
        }
    }
}
