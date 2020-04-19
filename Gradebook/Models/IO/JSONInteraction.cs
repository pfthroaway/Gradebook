using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Gradebook.Models.IO
{
    public static class JSONInteraction
    {
        private static readonly string ClassesFolderLocation = Path.Combine(AppData.Location, "Classes");
        private static readonly string CoursesFolderLocation = Path.Combine(AppData.Location, "Courses");
        private static readonly string StudentsFolderLocation = Path.Combine(AppData.Location, "Students");
        private static readonly string TeachersFolderLocation = Path.Combine(AppData.Location, "Teachers");

        #region Load

        /// <summary>Loads all <see cref="Class"/>es from disk.</summary>
        /// <returns>List of <see cref="Class"/>es</returns>
        internal static List<SchoolClass> LoadClasses()
        {
            List<SchoolClass> classes = new List<SchoolClass>();
            if (!Directory.Exists(ClassesFolderLocation))
                Directory.CreateDirectory(ClassesFolderLocation);
            string[] files = Directory.GetFiles(ClassesFolderLocation);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        classes.Add(JsonConvert.DeserializeObject<SchoolClass>(File.ReadAllText(file)));
                }
            }

            return classes;
        }

        /// <summary>Loads all <see cref="Cours"/>es from disk.</summary>
        /// <returns>List of <see cref="Cours"/>es</returns>
        internal static List<Course> LoadCourses()
        {
            List<Course> courses = new List<Course>();
            if (!Directory.Exists(CoursesFolderLocation))
                Directory.CreateDirectory(CoursesFolderLocation);
            string[] files = Directory.GetFiles(CoursesFolderLocation);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        courses.Add(JsonConvert.DeserializeObject<Course>(File.ReadAllText(file)));
                }
            }

            return courses;
        }

        /// <summary>Loads all <see cref="Cours"/>es from disk.</summary>
        /// <returns>List of <see cref="Cours"/>es</returns>
        internal static List<Student> LoadStudents()
        {
            List<Student> students = new List<Student>();
            if (!Directory.Exists(StudentsFolderLocation))
                Directory.CreateDirectory(StudentsFolderLocation);
            string[] files = Directory.GetFiles(StudentsFolderLocation);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        students.Add(JsonConvert.DeserializeObject<Student>(File.ReadAllText(file)));
                }
            }

            return students;
        }

        /// <summary>Loads all <see cref="Cours"/>es from disk.</summary>
        /// <returns>List of <see cref="Cours"/>es</returns>
        internal static List<Teacher> LoadTeachers()
        {
            List<Teacher> teachers = new List<Teacher>();
            if (!Directory.Exists(TeachersFolderLocation))
                Directory.CreateDirectory(TeachersFolderLocation);
            string[] files = Directory.GetFiles(TeachersFolderLocation);
            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    if (file.EndsWith(".json"))
                        teachers.Add(JsonConvert.DeserializeObject<Teacher>(File.ReadAllText(file)));
                }
            }

            return teachers;
        }

        /// <summary>Loads JSON data from a file.</summary>
        /// <param name="path">Path to the file to be loaded</param>
        /// <returns>JSON data from a file</returns>
        private static List<T> LoadJsonFromFile<T>(string path)
        {
            string data = "";
            if (File.Exists(path))
                data = File.ReadAllText(path);
            else
                School.DisplayNotification($"{path} does not exist.", "Sulimn");

            return !string.IsNullOrWhiteSpace(data) ? JsonConvert.DeserializeObject<List<T>>(data) : new List<T>();
        }

        #endregion Load

        #region Save

        /// <summary>Saves a <see cref="SchoolClass"/> to disk.</summary>
        /// <param name="schoolClass"><see cref="SchoolClass"/> to be saved to disk</param>
        public static void SaveClass(SchoolClass schoolClass)
        {
            if (!Directory.Exists(ClassesFolderLocation))
                Directory.CreateDirectory(ClassesFolderLocation);
            File.WriteAllText(Path.Combine(ClassesFolderLocation, $"{schoolClass.Id}.json"), JsonConvert.SerializeObject(schoolClass, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Course"/> to disk.</summary>
        /// <param name="course"><see cref="Course"/> to be saved to disk</param>
        public static void SaveCourse(Course course)
        {
            if (!Directory.Exists(CoursesFolderLocation))
                Directory.CreateDirectory(CoursesFolderLocation);
            File.WriteAllText(Path.Combine(CoursesFolderLocation, $"{course.Number}.json"), JsonConvert.SerializeObject(course, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Student"/> to disk.</summary>
        /// <param name="student"><see cref="Student"/> to be saved to disk</param>
        public static void SaveStudent(Student student)
        {
            if (!Directory.Exists(StudentsFolderLocation))
                Directory.CreateDirectory(StudentsFolderLocation);
            File.WriteAllText(Path.Combine(StudentsFolderLocation, $"{student.Id}.json"), JsonConvert.SerializeObject(student, Formatting.Indented));
        }

        /// <summary>Saves a <see cref="Teacher"/> to disk.</summary>
        /// <param name="teacher"><see cref="Teacher"/> to be saved to disk</param>
        public static void SaveTeacher(Teacher teacher)
        {
            if (!Directory.Exists(TeachersFolderLocation))
                Directory.CreateDirectory(TeachersFolderLocation);
            File.WriteAllText(Path.Combine(TeachersFolderLocation, $"{teacher.Id}.json"), JsonConvert.SerializeObject(teacher, Formatting.Indented));
        }

        #endregion Save
    }
}