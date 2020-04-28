using Extensions;
using Extensions.ListViewHelp;
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
    /// <summary>Interaction logic for ClassesView.xaml</summary>
    public partial class ClassesView : Page
    {
        private SchoolClass _selectedClass;
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
            if (_selectedClass != null && School.YesNoNotification($"Are you sure that you want to delete this class? This action cannot be undone.", "Gradebook"))
            {
                School.DeleteClass(_selectedClass);
                RefreshItemsSource();
            }
        }

        private void BtnModifyClass_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void LVClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool selected = LVClasses.SelectedIndex >= 0;
            BtnDeleteClass.IsEnabled = selected;
            BtnModifyClass.IsEnabled = selected;
            if (selected)
                _selectedClass = (SchoolClass)LVClasses.SelectedValue;
        }

        private void LVClassesColumnHeader_Click(object sender, RoutedEventArgs e) => _sort = Functions.ListViewColumnHeaderClick(sender, _sort, LVClasses, "#CCCCCC");

        #endregion Click

        public ClassesView() => InitializeComponent();

        private void Page_Loaded(object sender, RoutedEventArgs e) => RefreshItemsSource();
    }
}