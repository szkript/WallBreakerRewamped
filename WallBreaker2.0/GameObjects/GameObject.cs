using System.Numerics;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public abstract class GameObject
    {
        internal Vector2 Position;
        internal double Width;
        internal double Height;
        internal string Name;

        public GameObject(double width, double height)
        {
            this.Width = width;
            this.Height = height;
            this.Name = this.GetType().Name;
        }
        internal Rectangle CreateRectangle()
        {
            Rectangle rectangle = new Rectangle
            {
                Name = this.Name,
                Stroke = Brushes.Black,
                Fill = Brushes.Green,
                Width = this.Width,
                Height = this.Height
            };
            return rectangle;
        }
    }
}
