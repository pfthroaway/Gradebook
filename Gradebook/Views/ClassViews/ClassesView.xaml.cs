using Extensions;
using Extensions.ListViewHelp;
using Gradebook.Models;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Gradebook.Views.ClassViews
{
    /// <summary>Interaction logic for ClassesView.xaml</summary>
    public partial class ClassesView : Page
    {
        private List<SchoolClass> _allClasses;
        private ListViewSort _sort = new ListViewSort();

        /// <summary>Refreshes the ListView's ItemsSource.</summary>
        internal void RefreshItemsSource()
        {
            _allClasses = new List<SchoolClass>(School.AllClasses);
            LVClasses.ItemsSource = _allClasses;
            LVClasses.Items.Refresh();
        }

        #region Click

        private void BtnNewClass_Click(object sender, RoutedEventArgs e) => School.Navigate(new NewClassView());

        private void BtnDeleteClass_Click(object sender, RoutedEventArgs e)
        {
            if (School.CurrentClass != null && School.YesNoNotification($"Are you sure that you want to delete this class? This action cannot be undone.", "Gradebook"))
            {
                School.DeleteClass(School.CurrentClass);
                RefreshItemsSource();
            }
        }

        private void BtnViewClass_Click(object sender, RoutedEventArgs e) => School.Navigate(new ClassView());

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void LVClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = LVClasses.SelectedIndex >= 0;
            BtnDeleteClass.IsEnabled = selected;
            BtnViewClass.IsEnabled = selected;
            if (selected)
                School.CurrentClass = (SchoolClass)LVClasses.SelectedValue;
        }

        private void LVClassesColumnHeader_Click(object sender, RoutedEventArgs e) => _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVClasses, "#CCCCCC");

        #endregion Click

        public ClassesView() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshItemsSource();
    }
}