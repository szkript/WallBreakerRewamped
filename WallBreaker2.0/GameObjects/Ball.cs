using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WallBreaker2.GameData;

namespace WallBreaker2.GameObjects
{
    public class Ball : GameObject
    {
        private Vector2 Velocity;
        private int BallBaseSpeed = 6;

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
            GameStatusEffect.NitroSpeed += BallBaseSpeed;
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
        internal void SlowMotion()
        {
            if (GameStatusEffect.SlowMotionIsReady)
            {
                GameTimeManager.SlowMotionTimeStart(SlowMotionTimer_Tick);
                GameTimeManager.SlowMotionCooldownStart(SlowMotionCooldown_tick);
                GameStatusEffect.SlowMotionIsReady = false;
                SetSpeed(GameStatusEffect.SlowMotionSpeed);
            }
        }
        void SlowMotionCooldown_tick(object sender, EventArgs e)
        {
            GameTimeManager.SlowMotionCoolDownStop();
        }
        void SlowMotionTimer_Tick(object sender, EventArgs e)
        {
            GameTimeManager.SlowMotionTimeStop();
            SetSpeed(BallBaseSpeed);
        }
        internal void Nitro()
        {
            if (GameStatusEffect.NitroIsOn) { return; }

            SetSpeed(GameStatusEffect.NitroSpeed);
            GameStatusEffect.NitroIsOn = true;
        }
        internal void NitroOff()
        {
            SetSpeed(BallBaseSpeed);
            GameStatusEffect.NitroIsOn = false;
        }
        public void SetSpeed(int speed)
        {
            Velocity.X = Velocity.X > 0 ? speed : -speed;
            Velocity.Y = Velocity.Y > 0 ? speed : -speed;
        }
        public void InverseDirection(Axis axis)
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

        //TODO: Simulate full move with velocity of 1
        public void SimulateMove()
        {
            int distance = Convert.ToInt32(Vector2.Distance(Position, PeekingMove()));
            
            Console.WriteLine($"starting position: {Position}");
            for (int i = 0; i < distance; i++)
            {
                float x = Velocity.X > 0 ? i : -i;
                float y = Velocity.Y > 0 ? i : -i;
                Vector2 fakeVelocity = new Vector2(x, y);
                Vector2 fakePos = Position + (Direction * fakeVelocity);
                Console.WriteLine($"inner position: {fakePos}");
            }
            Console.WriteLine($"final position: {PeekingMove()}");
        }

        public Vector2 PeekingMove()
        {
            Vector2 fake = new Vector2(Velocity.X, Velocity.Y);

            if (Position.X <= 0 || Position.X >= (WallbreakerCanvas.Width - (Width + 5))) { fake.X = -Velocity.X; }
            if (Position.Y <= 0 || Position.Y >= (WallbreakerCanvas.Height - (Height + 5))) { fake.Y = -Velocity.Y; }

            return Position + (Direction * fake);
        }

        internal void PreciseMove()
        {
            int distance = Convert.ToInt32(Vector2.Distance(Position, PeekingMove()));

            Console.WriteLine($"starting position: {Position}");
            //Console.WriteLine($"one step move pos: {fakePos}");
            for (int i = 0; i < distance; i++)
            {
                float x = Velocity.X > 0 ? 1 : -1;
                float y = Velocity.Y > 0 ? 1 : -1;
                Vector2 fakeVelocity = new Vector2(x, y);
                Position += (Direction * fakeVelocity);
                Console.WriteLine($"inner position: {Position}");
            }
            Console.WriteLine($"final position: {PeekingMove()}");
        }
    }
}
