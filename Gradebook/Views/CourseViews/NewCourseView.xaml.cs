using Extensions;
using Extensions.DataTypeHelpers;
using Extensions.Enums;
using Gradebook.Models;
using Gradebook.Models.IO;
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

namespace Gradebook.Views.CourseViews
{
    /// <summary>Interaction logic for NewCourseView.xaml</summary>
    public partial class NewCourseView : Page
    {
        /// <summary>Checks whether the Save buttons should be enabled.</summary>
        private void CheckButtons()
        {
            bool enabled = TxtNumber.Text.Length >= 0 && TxtName.Text.Length >= 0 && TxtHours.Text.Length >= 0;
            BtnSaveAndDone.IsEnabled = enabled;
            BtnSaveAndNew.IsEnabled = enabled;
        }

        /// <summary>Clears all TextBoxes.</summary>
        private void Clear()
        {
            TxtNumber.Clear();
            TxtName.Clear();
            TxtHours.Clear();
        }

        /// <summary>Saves a new <see cref="Course"/>.</summary>
        private void Save() => School.SaveCourse(new Course(TxtNumber.Text, TxtName.Text, DecimalHelper.Parse(TxtHours)));

        #region Click

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void BtnClear_Click(object sender, RoutedEventArgs e) => Clear();

        private void BtnSaveAndDone_Click(object sender, RoutedEventArgs e)
        {
            Save();
            School.GoBack();
        }

        private void BtnSaveAndNew_Click(object sender, RoutedEventArgs e)
        {
            Save();
            Clear();
        }

        #endregion Click

        #region Page-Manipulation Methods

        public NewCourseView() => InitializeComponent();

        private void Txt_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        private void TxtHours_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Decimals);

        private void TxtNameNumber_TextChanged(object sender, TextChangedEventArgs e) => CheckButtons();

        private void TxtHours_TextChanged(object sender, TextChangedEventArgs e)
        {
            Functions.TextBoxTextChanged(sender, KeyType.Decimals);
            CheckButtons();
        }

        #endregion Page-Manipulation Methods
    }
}