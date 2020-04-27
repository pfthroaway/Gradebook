using Extensions;
using Extensions.ListViewHelp;
using Gradebook.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.CourseViews
{
    /// <summary>Interaction logic for CoursesView.xaml</summary>
    public partial class CoursesView : Page
    {
        private Course _selectedCourse;
        private List<Course> _allCourses;
        private ListViewSort _sort = new ListViewSort();

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource()
        {
            _allCourses = School.AllCourses;
            LVCourses.ItemsSource = _allCourses;
            LVCourses.Items.Refresh();
        }

        #region Click

        private void BtnNewCourse_Click(object sender, RoutedEventArgs e) => School.Navigate(new NewCourseView());

        private void BtnDeleteCourse_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedCourse != null && School.YesNoNotification($"Are you sure that you want to delete this course? It will affect {School.AllClasses.Where(cls => cls.Course == _selectedCourse).ToList().Count} courses. This action cannot be undone.", "Gradebook"))
                School.DeleteCourse(_selectedCourse);
        }

        private void BtnModifyCourse_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void LVCourses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = LVCourses.SelectedIndex >= 0;
            BtnDeleteCourse.IsEnabled = selected;
            BtnModifyCourse.IsEnabled = selected;
            if (selected)
                _selectedCourse = (Course)LVCourses.SelectedValue;
        }

        private void LVCoursesColumnHeader_Click(object sender, RoutedEventArgs e) => _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVCourses, "#CCCCCC");

        #endregion Click

        public CoursesView() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshItemsSource();
    }
}