using System;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Ball : GameObject
    {
        public Rectangle ball;
        private Vector2 Velocity;
        private int BallBaseSpeed = 6;

        public Vector2 Direction { get; set; }

        public Ball(double width, double height, Canvas wallbreakerCanvas, double offset) : base(width, height, wallbreakerCanvas)
        {
            ball = CreateRectangle(Brushes.Black, Brushes.Black);
            Position = new Vector2((float)WallbreakerCanvas.Width / 2 - (float)ball.Width / 2, (float)offset);
            Velocity = new Vector2(BallBaseSpeed, BallBaseSpeed);
            Random random = new Random();
            int randomDirX = -100 + random.Next(0, 201);
            int randomDirY = 0 + random.Next(0, 201);
            Direction = new Vector2(randomDirX, randomDirY);
            Direction = Vector2.Normalize(Direction);
            ball.SetValue(Canvas.LeftProperty, (double)Position.X);
            ball.SetValue(Canvas.TopProperty, (double)Position.Y);
            WallbreakerCanvas.Children.Add(ball);
        }
        public void Move()
        {
            if (Position.X < 0 || Position.X > (WallbreakerCanvas.Width - ball.Width)) { Velocity.X = -Velocity.X; }
            if (Position.Y < 0 || Position.Y > (WallbreakerCanvas.Height - ball.Height)) { Velocity.Y = -Velocity.Y; }

            Position += Direction * Velocity;
            ball.SetValue(Canvas.LeftProperty, (double)Position.X);
            ball.SetValue(Canvas.TopProperty, (double)Position.Y);
        }
    }
}
