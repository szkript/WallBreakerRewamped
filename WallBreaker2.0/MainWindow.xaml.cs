﻿using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;

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
            game = new Game(WallbreakerCanvas);
            game.Start();
        }

        private void SetWindowSize()
        {
            Application.Current.MainWindow.Width = WindowWidth;
            Application.Current.MainWindow.Height = WindowHeight;
            //for some reason the canvas and the window size aren't the same:/
            WallbreakerCanvas.Width = Width -25;
            WallbreakerCanvas.Height = Height -40;
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
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    game.paddle.MoveLeft = true;
                    break;
                case Key.Right:
                    game.paddle.MoveRight = true;
                    break;
                case Key.Space:
                    game.TogglePause(GameState.SimplePause);
                    break;
            }
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    game.paddle.MoveLeft = false;
                    break;
                case Key.Right:
                    game.paddle.MoveRight = false;
                    break;
            }
        }
    }
}
