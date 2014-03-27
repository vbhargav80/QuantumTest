using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Quantum.Domain.Entities;

namespace Quantum.Tests
{
    [TestFixture]
    public class ClassTestSuite
    {
        [Test]
        [ExpectedException(typeof(Exception), ExpectedMessage = "A Student with the surname Bhargava already exists.")]
        public void CannotAddStudentWithDuplicateSurname()
        {
            var newStudent = new Student() {Age = 20, ClassId = 1, FirstName = "Varun", LastName = "Bhargava", GPA = 3.2m};
            var existingStudents = new List<Student>();
            existingStudents.Add(new Student() { Age = 20, ClassId = 1, FirstName = "Varun 1", LastName = "Bhargava 1", GPA = 3.2m });
            existingStudents.Add(new Student() { Age = 30, ClassId = 3, FirstName = "Varun 2", LastName = "Bhargava 3", GPA = 3.2m });
            existingStudents.Add(new Student() { Age = 40, ClassId = 4, FirstName = "Varun 3", LastName = "Bhargava", GPA = 3.2m });

            var myClass = new Class() {Id = 1, Location = "Somewhere", Name = "Someclass"};
            myClass.AddStudent(newStudent, existingStudents);
        }
    }
}
