using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PokerThinkUI.Controls.Player
{
    public class ViewModel : BaseViewModel
    {
        #region Fields

        private int _chips;
        private int _bet;
        private Visibility _buttonVisibility = Visibility.Hidden;
        private string _name;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public int Chips
        {
            get { return _chips; }
            set
            {
                _chips = value;
                RaisePropertyChanged("Chips");
            }
        }

        public int Bet
        {
            get { return _bet; }
            set
            {
                _bet = value;
                RaisePropertyChanged("Bet");
            }
        }

        public Visibility ButtonVisibility
        {
            get { return _buttonVisibility; }
            set
            {
                _buttonVisibility = value;
                RaisePropertyChanged("ButtonVisibility");
            }
        }

        #endregion

        #region Commands

        public RelayCommand PlaceBetCommand { get; private set; }

        public void PlaceBet()
        {
            Chips -= Bet;
        }

        public bool CanPlaceBet()
        {
            return (Chips >= Bet);
        }

        #endregion

        #region Constructor(s)

        public ViewModel()
        {
            PlaceBetCommand = new RelayCommand(PlaceBet, CanPlaceBet);
        }

        #endregion
    }
}
