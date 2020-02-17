﻿using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Brick : GameObject
    {
        public Rectangle brick;

        public Brick(double width, double height, Vector2 position, Canvas wallbreakerCanvas) : base(width, height, wallbreakerCanvas)
        {
            Position = position;
            brick = CreateRectangle(Brushes.Yellow, Brushes.Red);
            Canvas.SetLeft(brick, Position.X);
            Canvas.SetTop(brick, Position.Y);
            wallbreakerCanvas.Children.Add(brick);

        }
    }
}
