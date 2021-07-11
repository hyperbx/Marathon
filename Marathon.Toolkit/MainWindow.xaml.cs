using System;
using System.Windows;
using System.Windows.Controls;
using Marathon.Toolkit.Controls;

namespace Marathon.Toolkit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void IconButton_New_Click(object sender, EventArgs e)
        {
            TabItem startPage = new() { Header = "Start Page", Content = new StartPage() };

            TabControl_Workspace.Items.Add(startPage);
            TabControl_Workspace.SelectedItem = startPage;
        }
    }
}
