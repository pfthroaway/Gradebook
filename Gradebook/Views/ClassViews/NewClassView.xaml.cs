using Extensions;
using Extensions.DataTypeHelpers;
using Extensions.Enums;
using Gradebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gradebook.Views.ClassViews
{
    /// <summary>Interaction logic for NewClassView.xaml</summary>
    public partial class NewClassView : Page
    {
        private Teacher _selectedTeacher;
        private Course _selectedCourse;

        /// <summary>Clears all TextBoxes.</summary>
        private void Clear()
        {
            TxtID.Clear();
            CmbCourse.SelectedIndex = -1;
            CmbTeacher.SelectedIndex = -1;
            TxtDaily.Clear();
            TxtHomework.Clear();
            TxtProject.Clear();
            TxtQuiz.Clear();
            TxtReport.Clear();
            TxtTest.Clear();
            TxtStartTime.Text = "9:00 AM";
            TxtEndTime.Text = "9:50 AM";
            TxtID.Focus();
            TxtError.Visibility = Visibility.Collapsed;
        }

        private bool CheckValidCheckBox(CheckBox checkBox) => (checkBox.IsChecked ?? false) && checkBox.IsChecked.Value;

        private bool CheckValidClass() => TxtID.Text.Trim().Length > 0 && !School.AllClasses.Any(cls => cls.Id.Equals(TxtID.Text.Trim())) && CmbCourse.SelectedIndex >= 0 && CmbTeacher.SelectedIndex >= 0 && TxtDaily.Text.Trim().Length > 0 && TxtHomework.Text.Trim().Length > 0 && TxtProject.Text.Trim().Length > 0 && TxtQuiz.Text.Trim().Length > 0 && TxtReport.Text.Trim().Length > 0 && TxtTest.Text.Trim().Length > 0 && CheckValidTime(TxtStartTime.Text) && CheckValidTime(TxtEndTime.Text) && (CheckValidCheckBox(ChkSunday) || CheckValidCheckBox(ChkMonday) || CheckValidCheckBox(ChkTuesday) || CheckValidCheckBox(ChkWednesday) || CheckValidCheckBox(ChkThursday) || CheckValidCheckBox(ChkFriday) || CheckValidCheckBox(ChkSaturday));

        private bool CheckValidTime(string time)
        {
            DateTime checkTime = DateTimeHelper.Parse(time);
            return checkTime.Hour != 0 || checkTime.Minute != 0;
        }

        /// <summary>Saves a new <see cref="Class"/>.</summary>
        private bool Save()
        {
            if (CheckValidClass())
            {
                List<DayOfWeek> days = new List<DayOfWeek>();
                if (CheckValidCheckBox(ChkSunday))
                    days.Add(DayOfWeek.Sunday);
                if (CheckValidCheckBox(ChkMonday))
                    days.Add(DayOfWeek.Monday);
                if (CheckValidCheckBox(ChkTuesday))
                    days.Add(DayOfWeek.Tuesday);
                if (CheckValidCheckBox(ChkWednesday))
                    days.Add(DayOfWeek.Wednesday);
                if (CheckValidCheckBox(ChkThursday))
                    days.Add(DayOfWeek.Thursday);
                if (CheckValidCheckBox(ChkFriday))
                    days.Add(DayOfWeek.Friday);
                if (CheckValidCheckBox(ChkSaturday))
                    days.Add(DayOfWeek.Saturday);
                School.NewClass(new SchoolClass(TxtID.Text.Trim().Trim(), _selectedTeacher.Id, new List<string>(), _selectedCourse, new List<decimal> { DecimalHelper.Parse(TxtDaily.Text.Trim()), DecimalHelper.Parse(TxtHomework.Text.Trim()), DecimalHelper.Parse(TxtProject.Text.Trim()), DecimalHelper.Parse(TxtQuiz.Text.Trim()), DecimalHelper.Parse(TxtReport.Text.Trim()), DecimalHelper.Parse(TxtTest.Text.Trim()) }, new List<Assignment>(), days, DateTimeHelper.Parse(TxtStartTime.Text), DateTimeHelper.Parse(TxtEndTime.Text)));
                return true;
            }
            TxtError.Visibility = Visibility.Visible;
            return false;
        }

        #region Click

        private void BtnBack_Click(object sender, RoutedEventArgs e) => School.GoBack();

        private void BtnClear_Click(object sender, RoutedEventArgs e) => Clear();

        private void BtnSaveAndDone_Click(object sender, RoutedEventArgs e)
        {
            if (Save())
                School.GoBack();
        }

        private void BtnSaveAndNew_Click(object sender, RoutedEventArgs e)
        {
            if (Save())
                Clear();
        }

        #endregion Click

        #region Page-Manipulation Methods

        public NewClassView()
        {
            InitializeComponent();
            TxtID.Focus();
            CmbCourse.ItemsSource = School.AllCourses;
            CmbTeacher.ItemsSource = School.AllTeachers;
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e) => Functions.TextBoxGotFocus(sender);

        #endregion Page-Manipulation Methods

        private void TxtIntegers_TextChanged(object sender, TextChangedEventArgs e) => Functions.TextBoxTextChanged(sender, KeyType.Integers);

        private void TxtIntegers_PreviewKeyDown(object sender, KeyEventArgs e) => Functions.PreviewKeyDown(e, KeyType.Integers);

        private void CmbCourse_SelectionChanged(object sender, SelectionChangedEventArgs e) => _selectedCourse = CmbCourse.SelectedIndex >= 0 ? (Course)CmbCourse.SelectedValue : null;

        private void CmbTeacher_SelectionChanged(object sender, SelectionChangedEventArgs e) => _selectedTeacher = CmbTeacher.SelectedIndex >= 0 ? (Teacher)CmbTeacher.SelectedValue : null;
    }
}