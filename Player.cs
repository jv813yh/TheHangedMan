/* 
 * 
 * Author Jozef Vendel
 * Date 19.07.2023, version 1.0
 * Simple hanged man game where you guess a word 
 * one letter at a time and if you don't guess, you hang the boy
 * 
 */

namespace TheHangedMan
{
    /// <summary>
    /// A class representing the player with his nickname and score.
    /// </summary>
    public class Player
    {
        // name of the player
        public string Name { get; set; }  

        // score of the player = length of guessed word
        public int Score { get; set; }

        // number of mistakes
        public int NumberOfErrors { get; set; }

        public Player(string name, int score, int numberOfErrors)
        {
            Name = name;
            Score = score;
            NumberOfErrors = numberOfErrors;
        }

        // due to data serialization
        public Player()
        {

        }

        /// <summary>
        /// format which is written to the file
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "Name: " + Name + ", bad tips: " +
                NumberOfErrors + ", word length: " + Score;
        }
    }
}
