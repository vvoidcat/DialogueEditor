using DialogueEditor.ViewModels;
using DialogueEditor.Views.CommonControls;

namespace DialogueEditor.Views;

/// <summary>
/// Interaction logic for EditorPanel.xaml
/// </summary>
public partial class EditorPanel : DisposableUserControl
{
	public EditorPanel()
	{
		DataContext = App.GetService<EditorViewModel>();
		InitializeComponent();
	}
}
