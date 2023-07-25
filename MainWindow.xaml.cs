using System.Windows;

/* 
 * Author Jozef Vendel
 * Date 19.07.2023, version 1.0
 * Simple hanged man game where you guess a word 
 * one letter at a time and if you don't guess, you hang the boy
 */

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
            game.Show();
        }
    }
}
 