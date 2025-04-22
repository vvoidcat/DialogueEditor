using System.Windows.Controls;
using DialogueEditor.ViewModels;

namespace DialogueEditor.Views;

/// <summary>
/// Interaction logic for ConfigurationsPanel.xaml
/// </summary>
public partial class ConfigurationsPanel : UserControl
{
	public ConfigurationsPanel()
	{
		DataContext = App.GetService<ConfigurationsViewModel>();
		InitializeComponent();
	}
}
