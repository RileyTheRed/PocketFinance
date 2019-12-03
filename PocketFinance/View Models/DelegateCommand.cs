using System;
using System.Windows.Input;

namespace PocketFinance.ViewModels
{
    public class DelegateCommand : ICommand
    {
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public DelegateCommand(Action<object> execute) : this(execute, null) { }

        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null) { throw new ArgumentNullException(nameof(execute)); }

            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter) { _execute(parameter); }

        public bool CanExecute(object parameter) { return (_canExecute == null) ? true : _canExecute(parameter); }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        public event EventHandler CanExecuteChanged;
    }
}
