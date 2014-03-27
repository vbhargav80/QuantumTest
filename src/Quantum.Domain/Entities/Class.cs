using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Quantum.Domain.Entities
{
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string TeacherName { get; set; }

        public List<Student> Students { get; private set; }

        #region Constructor

        public Class()
        {
            Students = new List<Student>();
        }

        #endregion

        public void AddStudent(Student newStudent, List<Student> allExistingStudents)
        {
            if (Students == null)
                Students = new List<Student>();

            if (allExistingStudents.Any(student => student.LastName.ToLower() == newStudent.LastName.ToLower()))
            {
                throw new Exception(string.Format("A Student with the surname {0} already exists.", newStudent.LastName));
            }

            Students.Add(newStudent);
        }

    }
}
