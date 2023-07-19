using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM.Commands
{
    using System;
    using System.Windows.Input;

    namespace MVVM.Utility
    {
        public class RelayCommand : ICommand
        {
            private readonly Action execute;
            private readonly Func<bool> canExecute;

            public RelayCommand(Action execute)
            {
                this.execute = execute;
                this.canExecute = () => true;
            }

            public RelayCommand(Action execute, Func<bool> canExecute)
            {
                this.execute = execute;
                this.canExecute = canExecute;
            }

            public bool CanExecute(object parameter)
            {
                return canExecute();
            }

            public void Execute(object parameter)
            {
                execute();
            }

            public event EventHandler CanExecuteChanged;
        }
    }
}

