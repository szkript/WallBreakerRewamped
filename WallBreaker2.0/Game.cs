using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WallBreaker2.GameObjects;

namespace WallBreaker2
{
    public class Game
    {
        private Paddle paddle;
        public Game(System.Windows.Controls.Canvas pongCanvas)
        {
            paddle = new Paddle(150,20);

            paddle.GetPaddle().SetValue(Canvas.LeftProperty, pongCanvas.Width/2 - paddle.Width/2);
            paddle.GetPaddle().SetValue(Canvas.TopProperty, (double)pongCanvas.Height - paddle.Height);
            pongCanvas.Children.Add(paddle.GetPaddle());

        }
    }
}
