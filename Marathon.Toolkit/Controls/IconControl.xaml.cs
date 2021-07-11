using System.Windows;
using System.Windows.Controls;

namespace Marathon.Toolkit.Controls
{
    /// <summary>
    /// Interaction logic for IconControl.xaml
    /// </summary>
    public partial class IconControl : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            nameof(Text),
            typeof(string),
            typeof(IconControl),
            new PropertyMetadata("block")
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public IconControl()
        {
            InitializeComponent();
        }
    }
}
