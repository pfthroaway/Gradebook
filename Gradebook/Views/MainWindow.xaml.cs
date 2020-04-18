using Gradebook.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Gradebook
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            School.AllStudents.Add(new Student("S0001", "Harry", "Potter", new List<string> { "C0001", "C0002" }));
            School.AllClasses.Add(new SchoolClass("C0001", "T0001", new List<string> { "S0001" }, new Course("ENGL-1301", "English Composition I", 3m), new List<decimal> { 0.1m, 0.1m, 0.15m, 0.2m, 0.15m, 0.3m }, new List<Assignment> { new Assignment("Worksheet 1", AssignmentType.Daily, new Dictionary<string, decimal>() { { "S0001", 97m } }), new Assignment("Test 1", AssignmentType.Test, new Dictionary<string, decimal>() { { "S0001", 92m } }) }, new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }, new DateTime(2020, 1, 1, 9, 0, 0), new DateTime(2020, 1, 1, 9, 50, 0)));
            School.AllClasses.Add(new SchoolClass("C0002", "T0001", new List<string> { "S0001" }, new Course("ENGL-1302", "English Composition II", 3m), new List<decimal> { 0.1m, 0.1m, 0.15m, 0.2m, 0.15m, 0.3m }, new List<Assignment> { new Assignment("Worksheet 1", AssignmentType.Daily, new Dictionary<string, decimal>() { { "S0001", 97m } }), new Assignment("Quiz 1", AssignmentType.Quiz, new Dictionary<string, decimal>() { { "S0001", 92m } }), new Assignment("Report 1", AssignmentType.Report, new Dictionary<string, decimal>() { { "S0001", 102m } }), }, new List<DayOfWeek> { DayOfWeek.Monday, DayOfWeek.Wednesday, DayOfWeek.Friday }, new DateTime(2020, 1, 1, 9, 0, 0), new DateTime(2020, 1, 1, 9, 50, 0)));
            Txt.Text = (School.GetStudentGrades("S0001"));
        }
    }
}