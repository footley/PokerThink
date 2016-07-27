using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PokerThinkUI
{
    public class RelayCommand : ICommand
    {
        #region Fields

        readonly Action _execute;
        readonly Func<bool> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute();
        }

        #endregion // ICommand Members
    }

    public class RelayCommand<ParameterType> : ICommand
    {
        #region Fields

        readonly Action<ParameterType> _execute;
        readonly Func<ParameterType, bool> _canExecute;

        #endregion // Fields

        #region Constructors

        public RelayCommand(Action<ParameterType> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<ParameterType> execute, Func<ParameterType, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((ParameterType)parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute((ParameterType)parameter);
        }

        #endregion // ICommand Members
    }
}
