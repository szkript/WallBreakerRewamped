using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Ball : GameObject
    {
        private Vector2 Velocity;
        private int BallBaseSpeed = 5;

        public Vector2 Direction;

        public Ball(double width, double height, Canvas wallbreakerCanvas, double offset) : base(width, height, wallbreakerCanvas)
        {
            Rectangle = CreateRectangle(Brushes.Black, Brushes.Black);
            Position = new Vector2((float)WallbreakerCanvas.Width / 2 - (float)Rectangle.Width / 2, (float)offset);
            Velocity = new Vector2(BallBaseSpeed, BallBaseSpeed);
            Random random = new Random();
            int randomDirX = -100 + random.Next(0, 201);
            int randomDirY = 0 + random.Next(0, 201);
            Direction = new Vector2(randomDirX, randomDirY);
            Direction = Vector2.Normalize(Direction);
            AddToCanvas(Rectangle);
        }
        public void Move()
        {
            if (Position.X < 0 || Position.X > (WallbreakerCanvas.Width - Rectangle.Width)) { Velocity.X = -Velocity.X; }
            if (Position.Y < 0 || Position.Y > (WallbreakerCanvas.Height - Rectangle.Height)) { Velocity.Y = -Velocity.Y; }

            Position += Direction * Velocity;
            Rectangle.SetValue(Canvas.LeftProperty, (double)Position.X);
            Rectangle.SetValue(Canvas.TopProperty, (double)Position.Y);
        }

        internal void InverseDirection(Paddle paddle)
        {
            double paddleMiddle = paddle.Position.X + paddle.Width / 2;
            double ballMiddle = Position.X + Width / 2;
            double positionDifference = ballMiddle - paddleMiddle;

            Velocity.Y = Math.Abs(Velocity.Y);
            Velocity.X = Math.Abs(Velocity.X);
            Direction = new Vector2(1 * (float)positionDifference, (float)-Math.Abs(1000 / positionDifference));
            Direction = Vector2.Normalize(Direction);
            Position += Direction * Velocity;
            Rectangle.SetValue(Canvas.TopProperty, (double)Position.Y);
        }

        public void InverseDirection(Brick brick)
        {
            List<int> ballRightSide = Enumerable.Range((int)(Position.X + Width), (int)Height).ToList();
            List<int> brickLeftSide = Enumerable.Range((int)brick.Position.X, (int)brick.Height).ToList();

            List<int> ballLeftSide = Enumerable.Range((int)Position.X, (int)Height).ToList();
            List<int> brickRightSide = Enumerable.Range((int)(brick.Position.X + brick.Width), (int)brick.Height).ToList();

            List<int> ballBottomSide = Enumerable.Range((int)(Position.Y + Height), (int)Width).ToList();
            List<int> brickTopSide = Enumerable.Range((int)brick.Position.Y, (int)brick.Width).ToList();

            List<int> brickBottomSide = Enumerable.Range((int)brick.Position.X, (int)brick.Width).ToList();
            List<int> ballTopSide = Enumerable.Range((int)Position.X, (int)Width).ToList();


            if (brickLeftSide.Any(x => ballRightSide.Contains(x)) || brickRightSide.Any(x => ballLeftSide.Contains(x)))
            {
                InverseDirection(Axis.X);
            }
            else if (brickTopSide.Any(x => ballBottomSide.Contains(x)) || brickBottomSide.Any(x => ballTopSide.Contains(x)))
            {
                InverseDirection(Axis.Y);
            }
        }
        public Vector2 PeekingMove()
        {
            Vector2 fake = new Vector2(Velocity.X, Velocity.Y);

            if (Position.X <= 0 || Position.X >= (WallbreakerCanvas.Width - (Width + 5))) { fake.X = -Velocity.X; }
            if (Position.Y <= 0 || Position.Y >= (WallbreakerCanvas.Height - (Height + 5))) { fake.Y = -Velocity.Y; }

            return Position + (Direction * fake);
        }
        private void InverseDirection(Axis axis)
        {
            switch (axis)
            {
                case Axis.X:
                    Direction.X = Direction.X > Direction.X + 1 ? Direction.X : -Direction.X;
                    break;
                case Axis.Y:
                    Direction.Y = Direction.Y > Direction.Y + 1 ? Direction.Y : -Direction.Y;
                    break;
            }
        }
    }
}
