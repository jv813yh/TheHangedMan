using System.Windows;

namespace TheHangedMan
{
    /// <summary>
    /// Interaction logic for NewPlayer.xaml
    /// Loading and saving a new player
    /// </summary>
    public partial class NewPlayer : Window
    { 
        private int Score { get; set; }
        private int MissAttempts { get; set; }

        // auxiliary class for work with file and create a new player
        ManagmentPlayers managmentPlayers;

        public NewPlayer(int score, int missAttempts)
        {
            InitializeComponent();

            Score = score;
            MissAttempts = missAttempts;

            managmentPlayers = new ManagmentPlayers();

            EnterYourNameTxtBox.Focus();

        }

        /// <summary>
        /// function for saving the player to the list and file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtnClick(object sender, RoutedEventArgs e)
        {
            // create a new player and save him/his to the file
            managmentPlayers.CreateNewPlayer(EnterYourNameTxtBox.Text, 
                Score, MissAttempts);

            // close the current dialog window
            Window window = GetWindow(this);
            window.Close();
        }
    }
}
