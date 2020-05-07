using Gradebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gradebook.Views.ClassViews
{
    /// <summary>Interaction logic for ClassView.xaml</summary>
    public partial class ClassView : Page
    {
        public ClassView()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Title = School.CurrentClass.Id;
            TxtClassName.Text = School.CurrentClass.Id;
        }

        private void BtnManageAssignments_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageStudents_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnManageClass_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}