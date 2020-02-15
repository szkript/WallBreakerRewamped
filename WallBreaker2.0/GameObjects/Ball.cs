using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Ball : GameObject
    {
        public Rectangle ball;
        private double CanvasWidth;
        private double CanvasHeight;
        private int Speed;

        public Ball(double width, double height, Canvas canvas):base(width,height)
        {
            CanvasWidth = canvas.Width;
            CanvasHeight = canvas.Height;
            Speed = 6;
            ball = CreateRectangle(Brushes.Black,Brushes.Black);
        }
    }
}
