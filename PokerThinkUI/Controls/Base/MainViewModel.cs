using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Drawing;

namespace PokerThinkUI.Controls
{
    public class MainViewModel : BaseViewModel
    {
        public int TotalChips
        {
            get { return Players.Sum(x => x.Chips); }
        }

        private int _pot = 0;
        public int Pot
        {
            get { return _pot; }
            set
            {
                _pot = value;
                RaisePropertyChanged("Pot");
            }
        }

        public string CardImage
        {
            get { return "Resources/As.png"; }
        }

        public DeeperObservableCollection<Player.ViewModel> Players { get; set; }

        public MainViewModel()
        {
            Players = new DeeperObservableCollection<Player.ViewModel>();
            Players.Add(new Player.ViewModel() { Name = "Player1", Chips = 3000 });
            Players.Add(new Player.ViewModel() { Name = "Player2", Chips = 3000 });
            Players.Add(new Player.ViewModel() { Name = "Player3", Chips = 3000 });
            Players.Add(new Player.ViewModel() { Name = "Player4", Chips = 3000 });
            Players.Add(new Player.ViewModel() { Name = "Player5", Chips = 3000 });
            Players.Add(new Player.ViewModel() { Name = "Player6", Chips = 3000 });
            Players.CollectionChanged += Players_CollectionChanged;
        }

        private void Players_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("TotalChips");
        }
    }
}
