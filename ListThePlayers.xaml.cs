using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace TheHangedMan
{
    /// <summary>
    /// Interaction logic for ListThePlayers.xaml
    /// 
    /// Class for displaying saved players in the file
    /// </summary>
    public partial class ListThePlayers : Window
    {
        // list for saving players
        public ObservableCollection<Player> PlayersList { get; set; }
        
        public ListThePlayers()
        {
            InitializeComponent();

            try
            {
                ManagmentPlayers managment = new ManagmentPlayers();
                managment.LoadThePlayers();

                PlayersList = managment.PlayerList;

                DataContext = PlayersList.OrderBy(item => item.Score).OrderBy(item => item.NumberOfErrors).ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
