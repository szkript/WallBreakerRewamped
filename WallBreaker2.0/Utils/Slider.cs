using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WallBreaker2.Utils
{
    class Slider
    {
        private double sliderCounter;
        private Canvas _Canvas;

        public Slider(Canvas canvas)
        {
            _Canvas = canvas;
        }
        private void ScreenSlideRight(object sender, EventArgs e)
        {
            if (sliderCounter <= 15)
            {
                foreach (Rectangle item in _Canvas.Children)
                {
                    item.SetValue(Canvas.LeftProperty, (double)item.GetValue(Canvas.LeftProperty) + sliderCounter);
                }
                sliderCounter += 0.1;
                Console.WriteLine(sliderCounter);
                return;
            }
            DispatcherTimer senderTimer = (DispatcherTimer)sender;
            senderTimer.Stop();
            Console.WriteLine("Slide Right done");
        }
        private void ScreenSlideLeft(object sender, EventArgs e)
        {
            if (sliderCounter >= 1.7)
            {
                foreach (Rectangle item in _Canvas.Children)
                {
                    item.SetValue(Canvas.LeftProperty, (double)item.GetValue(Canvas.LeftProperty) - sliderCounter);
                }
                sliderCounter -= 0.1;
                Console.WriteLine(sliderCounter);
                return;
            }
            DispatcherTimer senderTimer = (DispatcherTimer)sender;
            senderTimer.Stop();
            Console.WriteLine("Slide Left done");
        }
        private void SlideController(Action<object, EventArgs> action)
        {
            DispatcherTimer slideTimer = new DispatcherTimer();
            slideTimer.Interval = TimeSpan.FromSeconds(0.02);
            slideTimer.Tick += new EventHandler(action);
            slideTimer.Start();
        }
    }
}
