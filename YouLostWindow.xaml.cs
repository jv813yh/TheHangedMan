using System.Windows;


namespace TheHangedMan
{
    /// <summary>
    /// Interaction logic for YouLostWindow.xaml
    /// Displaying the loss window
    /// </summary>
    public partial class YouLostWindow : Window
    {
        public string GuessWord { get; set; }
        public YouLostWindow(string guessWord)
        {
            InitializeComponent();
            GuessWord = guessWord;  
            DataContext = GuessWord;
        }
    }
}
