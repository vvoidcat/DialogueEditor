using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DialogueEditor.Controllers.Common;

namespace DialogueEditor.Controllers
{
    class MainAreaViewModel : ObservableObject
    {
        public ObservableCollection<DialogueNode> nodes;

        public MainAreaViewModel()
        {
            nodes = new ObservableCollection<DialogueNode>() { new DialogueNode() { Children = new ObservableCollection<DialogueNode> { new DialogueNode() } } };
        }

    }

    public class DialogueNode : ObservableObject
    {
        private bool _childrenLoaded = false;

        //public NodeSettings ContentSettings { get; set; }
        public string DisplayName { get; set; } = "node";
        public bool IsRoot { get; set; }
        public DialogueNode? Parent { get; set; }

        public ObservableCollection<DialogueNode> Children { get; set; }

        public DialogueNode()
        {
            _childrenLoaded = false;
            Children = new ObservableCollection<DialogueNode>();
        }

        public void LoadChildNodes()
        {
            if (_childrenLoaded) return;

            var newChildren = new ObservableCollection<DialogueNode>() { new DialogueNode() { Children = new ObservableCollection<DialogueNode> { new DialogueNode() } } };

            Children.Clear();
            Children = newChildren;

            _childrenLoaded = true;
        }
    }

    public class NodeSettings
    {
        public string Text { get; set; } = string.Empty;
        public string Speaker { get; set; } = string.Empty;
    }
}
