using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WallBreaker2.GameData;

namespace WallBreaker2.GameObjects
{
    public class Ball : GameObject
    {
        public Rectangle ball;
        private double CanvasWidth;
        private double CanvasHeight;
        private Vector2 Velocity;
        private int BallBaseSpeed = 6;
        internal Vector2 Position;


        public Ball(double width, double height, Canvas canvas) : base(width, height)
        {
            ball = CreateRectangle(Brushes.Black, Brushes.Black);
            CanvasWidth = canvas.Width;
            CanvasHeight = canvas.Height;
            Position = new Vector2((float)CanvasWidth / 2 - (float)ball.Width / 2, (float)CanvasHeight / 2 - 150);
            Velocity = new Vector2(BallBaseSpeed, BallBaseSpeed);
        }
    }
}
