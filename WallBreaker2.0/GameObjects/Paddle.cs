using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace WallBreaker2.GameObjects
{
    class Paddle : GameObject
    {
        private Rectangle paddle;
        private int Speed;
        internal bool MoveLeft { set; get; } = false;
        internal bool MoveRight { set; get; } = false;

        public Paddle(double Width, double Height) : base(Width,Height)
        {
            Console.WriteLine($"{Name} w: {Width}");
            paddle = CreateRectangle();
        }
        public Rectangle GetPaddle() {
            return paddle;
        }
    }
}
