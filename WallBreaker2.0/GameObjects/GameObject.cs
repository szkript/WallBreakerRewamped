using System.Numerics;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    public abstract class GameObject
    {
        internal Canvas WallbreakerCanvas;
        internal Vector2 Position;
        internal Rectangle Rectangle;
        internal double Width;
        internal double Height;
        internal string Name;

        public GameObject(double width, double height, Canvas wallbreakerCanvas)
        {
            WallbreakerCanvas = wallbreakerCanvas;
            Width = width;
            Height = height;
            Name = GetType().Name;
        }
        internal Rectangle CreateRectangle(Brush stroke, Brush fill)
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
        internal virtual void AddToCanvas(Rectangle rectangle)
        {
            rectangle.SetValue(Canvas.LeftProperty, (double)Position.X);
            rectangle.SetValue(Canvas.TopProperty, (double)Position.Y);
            WallbreakerCanvas.Children.Add(rectangle);
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
