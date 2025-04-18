using System.Collections.ObjectModel;
using System.Windows.Input;
using DialogueEditor.Controllers.Common;
using DialogueEditor.Controllers.Models;

namespace DialogueEditor.Controllers;

class EditorPanelViewModel : ObservableObject
{
    private ObservableCollection<DialogueNode> _nodes;
    public ObservableCollection<DialogueNode> Nodes
    {
        get => _nodes;
        set 
        {
            _nodes = value;
            OnPropertyChanged(nameof(Nodes));
        }
    }

    private bool _isVisibleNodeSettingsPanel;
    public bool IsVisibleNodeSettingsPanel
    {
        get => _isVisibleNodeSettingsPanel;
        set
        {
            _isVisibleNodeSettingsPanel = value;
            OnPropertyChanged(nameof(IsVisibleNodeSettingsPanel));
        }
    }

    public ICommand toggleHideNodeSettingsCommand => new RelayCommand<object>(() => IsVisibleNodeSettingsPanel = !IsVisibleNodeSettingsPanel);

    public EditorPanelViewModel()
    {
        Nodes = new ObservableCollection<DialogueNode>()
        {
            new DialogueNode()
            {
                IsRoot = true,
                DisplayName = "ROOT",
                Children = new ObservableCollection<DialogueNode>
                {
                    new DialogueNode() { 
                        DisplayName = "Npc Node", 
                        Children = new ObservableCollection<DialogueNode>() { 
                            new DialogueNode() { IsPlayer = true, DisplayName = "PlayerNode" },
                            new DialogueNode() { IsPlayer = true, IsLink = true, DisplayName = "Link node" } } },
                    new DialogueNode() { DisplayName = "Npc Node2" },
                }
            }
        };
    }

}
