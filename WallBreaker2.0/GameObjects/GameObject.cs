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
        internal Rectangle CreateRectangle(Brush stroke,Brush fill)
        {
            Rectangle rectangle = new Rectangle
            {
                Name = this.Name,
                Stroke = stroke,
                Fill = fill,
                Width = this.Width,
                Height = this.Height
            };
            return rectangle;
        }
    }

    public enum Axis
    {
        X,
        Y
    }
    public enum Side
    {
        Top,
        Right,
        Bottom,
        Left
    }
}
