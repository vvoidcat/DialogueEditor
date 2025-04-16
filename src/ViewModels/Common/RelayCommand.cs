using System.Windows.Input;

namespace DialogueEditor.ViewModels.Common
{

	public class RelayCommand<T> : ICommand
	{
		private readonly Action? _exec;
		private readonly Action<T>? _execWithParam;

		public event EventHandler? CanExecuteChanged;

		public RelayCommand(Action<T> action)
		{
			_execWithParam = action;
		}

		public RelayCommand(Action action)
		{
			_exec = action;
		}

		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{
			if (_execWithParam is not null && parameter is not null)
			{
				_execWithParam.Invoke((T)parameter);
			}

			if (_exec is not null)
			{
				_exec.Invoke();
			}
		}

		private void OnCanExecuteChanged()
		{
			CanExecuteChanged?.Invoke(this, new EventArgs());
		}
	}
}