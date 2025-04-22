using System.Windows.Controls;
using DialogueEditor.ViewModels;

namespace DialogueEditor.Views;

/// <summary>
/// Interaction logic for DialogueFilesPanel.xaml
/// </summary>
public partial class DialogueFilesPanel : UserControl
{
	public DialogueFilesPanel()
	{
		DataContext = App.GetService<DialogueFilesViewModel>();
		InitializeComponent();
	}
}
