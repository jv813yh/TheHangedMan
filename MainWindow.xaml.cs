﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TheHangedMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// It contains the opening screen of the 
    /// game with the name, picture and rules. 
    /// It also includes a button to start the game.
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickBtn(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.ShowDialog();
        }
    }
}