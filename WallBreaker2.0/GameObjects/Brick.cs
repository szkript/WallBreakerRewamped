using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Brick : GameObject
    {
        public Rectangle brick;

        public Brick(double width, double height, Vector2 position) : base(width, height)
        {
            this.Position = position;
            brick = CreateRectangle(Brushes.Yellow, Brushes.Red);
        }
    }
}
