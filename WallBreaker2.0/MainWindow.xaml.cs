using System;
using System.Configuration;
using System.Windows;

namespace WallBreaker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double WindowWidth = Convert.ToDouble(ConfigurationManager.AppSettings.Get("MainWindowWidth"));
        private double WindowHeight = Convert.ToDouble(ConfigurationManager.AppSettings.Get("MainWindowHeight"));

        public MainWindow()
        {
            InitializeComponent();
        }
        private void PongCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowSize();
            CenterWindowOnScreen();
        }

        private void SetWindowSize()
        {
            Application.Current.MainWindow.Width = WindowWidth;
            Application.Current.MainWindow.Height = WindowHeight;
            PongCanvas.Width = WindowWidth;
            PongCanvas.Height = WindowHeight;
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
