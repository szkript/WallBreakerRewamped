﻿using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Paddle : GameObject
    {
        public Rectangle paddle;
        private int Speed;
        public bool MoveLeft { set; get; } = false;
        public bool MoveRight { set; get; } = false;
        internal double CanvasWidth;

        public Paddle(double Width, double Height, double canvasWidth) : base(Width, Height)
        {
            this.CanvasWidth = canvasWidth;
            this.Speed = 8;
            paddle = CreateRectangle(Brushes.Black, Brushes.Green);
        }
        public void MovePaddle()
        {
            if (MoveLeft)
            {
                if ((double)paddle.GetValue(Canvas.LeftProperty) <= 0)
                {
                    paddle.SetValue(Canvas.LeftProperty, 0.0);
                }
                else
                {
                    paddle.SetValue(Canvas.LeftProperty, (double)paddle.GetValue(Canvas.LeftProperty) - Speed);
                }
            }
            if (MoveRight)
            {
                if ((double)paddle.GetValue(Canvas.LeftProperty) + paddle.ActualWidth >= CanvasWidth)
                {
                    paddle.SetValue(Canvas.LeftProperty, CanvasWidth - paddle.ActualWidth);
                }
                else
                {
                    paddle.SetValue(Canvas.LeftProperty, (double)paddle.GetValue(Canvas.LeftProperty) + Speed);
                }
            }

        }
    }
}
