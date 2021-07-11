using System;
using System.Windows;
using System.Windows.Controls;

namespace Marathon.Toolkit.Controls
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register
        (
            nameof(Text),
            typeof(string),
            typeof(IconButton),
            new PropertyMetadata("Placeholder")
        );

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly DependencyProperty IconTextProperty = DependencyProperty.Register
        (
            nameof(IconText),
            typeof(string),
            typeof(IconButton),
            new PropertyMetadata("block")
        );

        public string IconText
        {
            get => (string)GetValue(IconTextProperty);
            set => SetValue(IconTextProperty, value);
        }

        public static readonly DependencyProperty IconMarginProperty = DependencyProperty.Register
        (
            nameof(IconMargin),
            typeof(Thickness),
            typeof(IconButton),
            new PropertyMetadata(new Thickness(6))
        );
        
        public Thickness IconMargin
        {
            get => (Thickness)GetValue(IconMarginProperty);
            set => SetValue(IconMarginProperty, value);
        }

        public event EventHandler Click;

        public IconButton()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) => Click?.Invoke(sender, e);
    }
}
