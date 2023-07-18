using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheHangedMan
{
    /// <summary>
    /// A class representing the player with his nickname and score.
    /// </summary>
    class Player
    {
        public string Name { get; set; }  
        public int Score { get; set; }
        public int NumberOfErrors { get; set; }

        public Player(string name, int score, int numberOfErrors)
        {
            Name = name;
            Score = score;
            NumberOfErrors = numberOfErrors;
        }

        public override string ToString()
        {
            return Name + " - number of bad tips " + NumberOfErrors + " - number of guessed words " + Score;
        }
    }
}
