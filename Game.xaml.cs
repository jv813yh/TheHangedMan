using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;


/* 
 * 
 * Author Jozef Vendel
 * Date 19.07.2023, version 1.0
 * Simple hanged man game where you guess a word 
 * one letter at a time and if you don't guess, you hang the boy
 * 
 * Class handles the dialog window for the game itself, 
 * accepts characters entered by the user, checks the match 
 * with the word, draws a picture, evaluates whether 
 * the player won or lost
 *
 */

namespace TheHangedMan
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window, INotifyPropertyChanged
    {
        // the number of images in the game
        private int countOfImages = 11;
        // guess word our game
        public string GuessWord { get; set; }

        // player score, we want to have the smallest score for the player
        public int ScorePlayer { get; set; }

        // number of nonguessed attempts
        public int MissAttempts { get; set; }

        // character entered by the user
        public char GuessChar { get; set; }

        private string spacedHiddenWord;

        // edited version of the guessed word with '_'
        // characters that is displayed
        public string SpacedHiddenWord
        {
            get { return spacedHiddenWord; }
            set
            {
                // if there is a change ->
                if (spacedHiddenWord != value)
                {
                    spacedHiddenWord = value;
                    // invoking event
                    OnPropertyChanged();
                }
            }
        }

        // the event allows notifying other objects that the value
        // of property of the object on which this event is
        // invoked has changed -> spacedHiddenWord
        public event PropertyChangedEventHandler PropertyChanged;

        public Game()
        {
            InitializeComponent();

            // retrieving the searched word
            GuessWord = GameLogic.LoadFileReturWord();
            if(!string.IsNullOrEmpty(GuessWord))
            {
                MessageBox.Show(GuessWord, "GuessWord:", MessageBoxButton.OK);

                // setting a hidden word with letters like '_ '
                SpacedHiddenWord = string.Join(" ", GuessWord.Select(c => '_'));

                // context for binding guess word
                DataContext = this;

                // set variables for 0
                // MissAttempts -> number of mistakes
                // ScorePlayer -> how many attempts he needed to guess the word
                ScorePlayer = 0;
                MissAttempts = 0;

                InputTxtBox.Focus();
            }
        }

        /// <summary>
        /// Function for restart current dialog window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRestart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // reference to the current dialog window
                Window dialogWindow = Window.GetWindow(this);

                // close current dialog window
                dialogWindow.Close();

                Game newGame = new Game();
                newGame.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        /// <summary>
        /// function to display the list of players
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTheBestPlayers_Click(object sender, RoutedEventArgs e)
        {
            ListThePlayers listThePlayers = new ListThePlayers();

            listThePlayers.ShowDialog();
        }

        /// <summary>
        /// 1. get the guessed letter from the player,
        /// 2. check whether the guessed letter is in guessWord,
        /// 3. update hidden word to correctly guessed letter,
        /// 4. refresh the display in the TextBlock
        /// 5. rendering of a image, 
        ///    depending on the number of no guessed words
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToGuessButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1.comment
                char guessChar = char.Parse(InputTxtBox.Text);
                GuessChar = char.ToUpper(guessChar);

                // index of char in string
                int indexOfChar;

                // 2.comment  
                bool IsContains = GameLogic.CheckChar(GuessWord, GuessChar,
                                                       SpacedHiddenWord, out indexOfChar);
                if (IsContains && indexOfChar >= 0)
                {
                    ScorePlayer++;

                    // 3. comment
                    // management of the string guessWord according to guessed characters
                    StringBuilder spacedHiddenWord = new StringBuilder(SpacedHiddenWord);
                    spacedHiddenWord[indexOfChar * 2] = GuessChar;

                    SpacedHiddenWord = spacedHiddenWord.ToString();

                    // the content of the InputTxtBox will be empty.
                    InputTxtBox.Text = String.Empty;

                    // you can start typing without having to click again in the InputTxtBox
                    InputTxtBox.Focus();
                }
                else
                {
                    // rendering of the images, depending on the number of nonguessed words
                    UpdateImageVisibility(++MissAttempts);

                    InputTxtBox.Text = String.Empty;
                    InputTxtBox.Focus();

                    // the loss window appears
                    if(MissAttempts == countOfImages)
                    {
                        YouLostWindow youLostWindow = new YouLostWindow(GuessWord);
                        youLostWindow.ShowDialog();

                        // reference to the current dialog window
                        Window dialogWindow = Window.GetWindow(this);

                        // close current dialog window
                        dialogWindow.Close();
                    }
                }

                // you gessed the word
                if(!SpacedHiddenWord.Any(item => item == '_'))
                {
                    MessageBox.Show("You guessed the whole word", "Congratulations ;)", 
                        MessageBoxButton.OK, MessageBoxImage.Information);


                    // create new dialog window
                    NewPlayer newPlayer = new NewPlayer(ScorePlayer, MissAttempts);
                    newPlayer.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                InputTxtBox.Text = string.Empty;
                InputTxtBox.Focus();
            }
        }
        /// <summary>
        /// Is used to call the PropertyChanged event, 
        /// which reports that the value of a class property has changed.
        /// Used in conjunction with data binding
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Updating images according mistakes
        /// </summary>
        /// <param name="numberOfMistakes"></param>
        public void UpdateImageVisibility(int numberOfMistakes)
        {
            List<Image> imagesList = new List<Image>
            {
                image1, image2, image3, image4, image5, image6, image7,
                image8, image9, image10, image11
            };

            for(int i = 0; i < imagesList.Count; i++)
            {
                imagesList[i].Visibility = (i < numberOfMistakes) ? 
                    Visibility.Visible : Visibility.Hidden;
            }
        }
    }
}
