using System.Windows.Input;

namespace Raspisanie
{
    public class CommandGetVisibility : ICommand
    {
        private readonly Action<object> execute;
        public event EventHandler CanExecuteChanged;

        public CommandGetVisibility(Action<object> execute) => this.execute = execute;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter) => execute(parameter);
    }
}
