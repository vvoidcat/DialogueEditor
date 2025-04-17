using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DialogueEditor.Views.Common
{
    /// <summary>
    /// Interaction logic for ShowHideButtonControl.xaml
    /// </summary>
    public partial class ShowHideButtonControl : UserControl
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register
            (
                "Command",
                typeof(ICommand),
                typeof(ShowHideButtonControl)
            );

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ShowHideButtonControl()
        {
            InitializeComponent();
        }
    }
}
