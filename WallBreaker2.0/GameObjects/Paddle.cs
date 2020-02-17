using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Paddle : GameObject
    {
        //public Rectangle paddle;
        private int Speed;
        public bool MoveLeft { set; get; } = false;
        public bool MoveRight { set; get; } = false;

        public Paddle(double width, double height, Canvas wallbreakerCanvas) : base(width, height, wallbreakerCanvas)
        {
            Position = new Vector2((float)(wallbreakerCanvas.Width / 2 - Width / 2), (float)(wallbreakerCanvas.Height - Height));
            Speed = 8;
            Rectangle = CreateRectangle(Brushes.Black, Brushes.Green);
            AddToCanvas(Rectangle);
        }
        public void MovePaddle()
        {
            if (MoveLeft)
            {
                if ((double)Rectangle.GetValue(Canvas.LeftProperty) <= 0)
                {
                    Rectangle.SetValue(Canvas.LeftProperty, 0.0);
                }
                else
                {
                    Rectangle.SetValue(Canvas.LeftProperty, (double)Rectangle.GetValue(Canvas.LeftProperty) - Speed);
                }
            }
            if (MoveRight)
            {
                if ((double)Rectangle.GetValue(Canvas.LeftProperty) + Rectangle.ActualWidth >= WallbreakerCanvas.Width)
                {
                    Rectangle.SetValue(Canvas.LeftProperty, WallbreakerCanvas.Width - Rectangle.ActualWidth);
                }
                else
                {
                    Rectangle.SetValue(Canvas.LeftProperty, (double)Rectangle.GetValue(Canvas.LeftProperty) + Speed);
                }
            }
            Position.X = (float)(double)Rectangle.GetValue(Canvas.LeftProperty);
        }
    }
}
