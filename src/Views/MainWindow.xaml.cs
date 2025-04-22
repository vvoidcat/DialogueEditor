using System.Windows;
using DialogueEditor.ViewModels;

namespace DialogueEditor.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
	public MainWindow()
	{
		var vm = App.GetService<MainViewModel>();
		DataContext = vm;

		InitializeComponent();

		vm.OnRequestHide += (s, e) => { this.WindowState = WindowState.Minimized; };
		vm.OnRequestResize += (s, e) => { this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized; };
		vm.OnRequestClose += (s, e) => this.Close();
	}

}