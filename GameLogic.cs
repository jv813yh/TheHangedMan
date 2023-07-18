using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;

namespace TheHangedMan
{
    /// <summary>
    /// A class that contains the game logic and its processing. 
    /// Here is the generation of the guessed word, the checking 
    /// of the guessed letters, the drawing of the picture of the 
    /// hanged man and the processing of the results.
    /// </summary>
    class GameLogic
    {
        /// <summary>
        /// Load the file and return one random word from the file
        /// </summary>
        /// <returns></returns>
        public static string LoadFileReturWord()
        {
            try
            {
                Random random = new Random();

                // loading inputFile.txt
                string[] linesFile = File.ReadAllLines(@"inputFile.txt");

                // random selection one word from the inputFile
                return linesFile[random.Next(linesFile.Length)];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                return string.Empty;
            }
        }

        /// <summary>
        /// check whether the guessed letter is in guessWord
        /// </summary>
        /// <param name="guessWord"></param>
        /// <param name="guessChar"></param>
        /// <returns></returns>
        public static bool CheckChar(string guessWord, char guessChar,
                                     string spacedHiddenWord, out int indexerChar)
        {
            // default value in case the letter is not found
            indexerChar = -1;
            
            // return value 
            bool IsContains = false;

            // check whether the character is in the word
            for(int i = 0; i < guessWord.Length; i++)
            {
                if(guessWord[i] == guessChar
                    && spacedHiddenWord[i * 2] == '_')
                {
                    indexerChar = i;
                    IsContains = true;
                    break;
                }
            }

            return IsContains;
        }
    }
}
