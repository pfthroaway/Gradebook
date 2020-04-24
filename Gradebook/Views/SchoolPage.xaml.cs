using Gradebook.Models;
using Gradebook.Views.CourseViews;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views
{
    /// <summary>Interaction logic for SchoolPage.xaml</summary>
    public partial class SchoolPage : Page
    {
        public SchoolPage() => InitializeComponent();

        private void BtnViewCourses_Click(object sender, RoutedEventArgs e) => School.Navigate(new CoursesView());
    }
}