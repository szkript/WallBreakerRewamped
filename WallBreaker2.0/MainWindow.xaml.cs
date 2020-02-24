using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using WallBreaker2.GameData;

namespace WallBreaker2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double WindowWidth = Convert.ToDouble(ConfigurationManager.AppSettings.Get("MainWindowWidth"));
        private double WindowHeight = Convert.ToDouble(ConfigurationManager.AppSettings.Get("MainWindowHeight"));
        private Game game;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void WallbreakerCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            SetWindowSize();
            CenterWindowOnScreen();
            game = new Game(WallbreakerCanvas, MenuCanvas);
        }

        private void SetWindowSize()
        {
            Application.Current.MainWindow.Width = WindowWidth;
            Application.Current.MainWindow.Height = WindowHeight;
            //for some reason the canvas and the window size aren't the same:/
            //Further investigation needs.
            WallbreakerCanvas.Width = Width - 25;
            WallbreakerCanvas.Height = Height - 40;
        }
        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = Width;
            double windowHeight = Height;
            Left = (screenWidth / 2) - (windowWidth / 2);
            Top = (screenHeight / 2) - (windowHeight / 2);
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // in case of pressing arrows in menu
            if (game.ball == null && game.paddle == null){ return; }

            switch (e.Key)
            {
                case Key.Escape:
                    game.TogglePause(GameState.Exit);
                    break;
                case Key.Left:
                    game.paddle.MoveLeft = true;
                    break;
                case Key.Right:
                    game.paddle.MoveRight = true;
                    break;
                case Key.Space:
                    game.TogglePause(GameState.SimplePause);
                    break;
                case Key.F2:
                    game.TogglePause(GameState.Restart);
                    break;
                case Key.Up:
                    game.ball.Nitro();
                    break;
                case Key.Down:
                    game.ball.SlowMotion();
                    break;
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            // in case of pressing arrows in menu
            if (game.ball == null && game.paddle == null) { return; }

            switch (e.Key)
            {
                case Key.Left:
                    game.paddle.MoveLeft = false;
                    break;
                case Key.Right:
                    game.paddle.MoveRight = false;
                    break;
                case Key.Up:
                    game.ball.NitroOff();
                    break;
            }
        }

    }
}
