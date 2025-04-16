using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DialogueEditor.ViewModels.Common
{
	public class ObservableObject : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		protected bool SetProperty<T>(ref T oldValue, T newValue, string propertyName)
		{
			var valueChanged = false;

			if (!EqualityComparer<T>.Default.Equals(oldValue, newValue))
			{
				oldValue = newValue;
				OnPropertyChanged(propertyName);
				valueChanged = true;
			}

			return valueChanged;
		}

		protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
