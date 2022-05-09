using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ComeBien.ViewModel
{
    public class RelayCommand : ICommand
	{
		private readonly Action<object> _execute;
		private readonly bool _canExecute;

		public RelayCommand(Action<object> execute)
			: this(execute, true)
		{
		}

		public RelayCommand(Action<object> execute, bool canExecute)
		{
			if (execute == null)
				throw new ArgumentNullException("execute");
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter)
		{
			return _canExecute;
		}

		public event EventHandler CanExecuteChanged
		{
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}

	}
}
