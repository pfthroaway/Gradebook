using System.Collections.Generic;

namespace Gradebook.Models
{
    /// <summary>Static class representing the <see cref="School"/> and all its necessary components.</summary>
    public static class School
    {
        public static List<SchoolClass> AllClasses = new List<SchoolClass>();
        public static List<Teacher> AllTeachers = new List<Teacher>();
        public static List<Student> AllStudents = new List<Student>();
        public static List<Course> AllCourses = new List<Course>();
    }
}