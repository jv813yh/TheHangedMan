using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

/*
 * Class for working with files, 
 * loading players, saving players 
 * to a file, saving a player to a list 
 * 
 */

namespace TheHangedMan
{
    public class ManagmentPlayers
    {
        // the name of the file where the players are stored
        private string fileName = "theBestPlayers.xaml";

        // observableCollection for saving the players
        public ObservableCollection<Player> PlayerList { get; set; }

        public ManagmentPlayers()
        {
            PlayerList = new ObservableCollection<Player>();

            LoadThePlayers();
        }


        /// <summary>
        /// function for saving the player to .xml file
        /// </summary>
        public void SaveTheNewPlayer()
        {
            // Is used to serialize objects to XML format.
            // In the constructor of this class, the GetType method
            // is used to get the type of the list of players (PlayerList).
            XmlSerializer serializer = new XmlSerializer(PlayerList.GetType());

            // StreamWritter for opening the file
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                // writes the contents of the player list to a file using a StreamWriter object
                serializer.Serialize(sw, PlayerList);
            }
        }

        /// <summary>
        /// function for loading the player to .xml file
        /// </summary>
        public void LoadThePlayers()
        {
            // Is used to serialize objects to XML format.
            // In the constructor of this class, the GetType method
            // is used to get the type of the list of players (PlayerList).
            XmlSerializer xmlSerializer = new XmlSerializer(PlayerList.GetType());

            // if file exists
            if(File.Exists(fileName))
            {
                // streamReader is used to read text data from a file
                using (StreamReader sr = new StreamReader(fileName))
                {
                    // using the XmlSerializer, the data from the StreamReader
                    // is read and stored in the players variable
                    // of type ObservableCollection<Player>
                    var players = (ObservableCollection<Player>)xmlSerializer.Deserialize(sr);

                    // cleaning PlayerList
                    PlayerList.Clear();

                    // each loaded player from the list of players is sequentially
                    // passed and added to the PlayerList
                    foreach (var player in players)
                    {
                       PlayerList.Add(player);  
                    }
                }
            }
            else
            {
                // if no players have been saved yet
                PlayerList = new ObservableCollection<Player>();
                MessageBox.Show("No player has been saved yet ", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// creating a new player and saving it to a file
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="score">Score </param>
        /// <param name="numberOfMiss"></param>
        public void CreateNewPlayer(string name, int score, int numberOfMiss)
        {
            try
            {
                // creates a new player, adds it to the list of players
                // and then saves it to the file
                PlayerList.Add(new Player(name, score, numberOfMiss));

                SaveTheNewPlayer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
