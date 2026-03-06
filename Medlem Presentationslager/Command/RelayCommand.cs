using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Medlem_Presentationslager.Command
{
    public class RelayCommand :ICommand //Den ska heta Relay för att inte blandas ihop med namespacet=
    {
        private readonly Action<object> _execute; // Ändrat till Action<object>
        private readonly Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        public void Execute(object parameter) => _execute(parameter);

        public event EventHandler CanExecuteChanged;

        //public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        //public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}



