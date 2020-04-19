using Gradebook.Models;
using System.Windows;

namespace Gradebook
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            School.MainWindow = this;
            School.LoadAll();
        }
    }
}