using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public class Brick : GameObject
    {
        public Brick(double width, double height, Vector2 position, Canvas wallbreakerCanvas) : base(width, height, wallbreakerCanvas)
        {
            Position = position;
            Rectangle = CreateRectangle(Brushes.Yellow, Brushes.Red);
            AddToCanvas(Rectangle);
        }
    }
}
