using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;
using DialogueEditor.Controllers.Common;

namespace DialogueEditor.Controllers.Models;

public class DialogueNode : ObservableObject
{
    //public NodeSettings ContentSettings { get; set; }
    public string DisplayName { get; set; } = "node";
    public bool IsRoot { get; set; } = false;
    public bool IsPlayer { get; set; } = false;
    public bool IsLink { get; set; } = false;

    public DialogueNode? Parent { get; set; }
    public DialogueNode? LinkChild { get; set; }

    public ObservableCollection<DialogueNode> Children { get; set; }

    public DialogueNode()
    {
        //_childrenLoaded = false;
        Children = new ObservableCollection<DialogueNode>();
    }

    //public void LoadChildNodes()
    //{
    //    if (_childrenLoaded) return;

    //    _childrenLoaded = true;
    //}
}

public class NodeSettings
{
    public string Text { get; set; } = string.Empty;
    public string Speaker { get; set; } = string.Empty;
}