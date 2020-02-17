using System.Numerics;
using System.Windows.Controls;
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

        public Paddle(double width, double height, Canvas wallbreakerCanvas) : base(width, height, wallbreakerCanvas)
        {
            Position = new Vector2((float)(wallbreakerCanvas.Width / 2 - Width / 2), (float)(wallbreakerCanvas.Height - Height));
            this.Speed = 8;
            paddle = CreateRectangle(Brushes.Black, Brushes.Green);
            paddle.SetValue(Canvas.LeftProperty, (double)Position.X);
            paddle.SetValue(Canvas.TopProperty, (double)Position.Y);
            WallbreakerCanvas.Children.Add(paddle);

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
                if ((double)paddle.GetValue(Canvas.LeftProperty) + paddle.ActualWidth >= WallbreakerCanvas.Width)
                {
                    paddle.SetValue(Canvas.LeftProperty, WallbreakerCanvas.Width - paddle.ActualWidth);
                }
                else
                {
                    paddle.SetValue(Canvas.LeftProperty, (double)paddle.GetValue(Canvas.LeftProperty) + Speed);
                }
            }

        }
    }
}
