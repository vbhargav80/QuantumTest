using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPoco;
using Quantum.Domain.Abstractions;
using Quantum.Domain.Entities;

namespace Quantum.Repository
{
    public class Repository : IRepository
    {
        #region Fields

        private readonly string _connectionName;

        #endregion

        #region Constructor

        public Repository()
        {
            _connectionName = "Quantum";
        }

        #endregion

        #region Public Methods

        public List<Class> GetAllClasses()
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Class");
                return db.Query<Class>(sql).ToList();
            }
        }

        public List<Student> GetStudentsByLastname(string lastname)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Student").Where("lastname = @lstname", new { lstName = lastname});
                return db.Query<Student>(sql).ToList();
            }
        }

        public List<Student> GetAllStudents()
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Student");
                return db.Query<Student>(sql).ToList();
            }
        }
        public Class GetClass(int classId)
        {
            Class returnVal;
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Class").Where("id = @clsId", new { clsId = classId });
                returnVal = db.Query<Class>(sql).FirstOrDefault();

                if (returnVal != null)
                {
                    var students = GetStudents(classId);
                    returnVal.Students.AddRange(students);
                }
            }

            return returnVal;
        }

        public void CreateClass(Class newClass)
        {
            using (var db = new Database(_connectionName))
            {
                db.Insert("Class", "ID", new { Name = newClass.Name, Location = newClass.Location, TeacherName = newClass.TeacherName });
            }
        }

        public void UpdateClass(Class updatedClass)
        {
            using (var db = new Database(_connectionName))
            {
                db.Update("Class", "ID", new { Name = updatedClass.Name, Location = updatedClass.Location, TeacherName = updatedClass.TeacherName }, updatedClass.Id);
            }
        }

        public void SaveClass(Class updatedClass)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Append("DELETE FROM dbo.Student WHERE ClassId = @0", updatedClass.Id);
                db.Execute(sql);

                foreach (var student in updatedClass.Students)
                {
                    CreateStudent(student);
                }

                db.Update("Class", "ID", new { Name = updatedClass.Name, Location = updatedClass.Location, TeacherName = updatedClass.TeacherName }, updatedClass.Id);
            }
        }

        public void DeleteClass(int classId)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Append("DELETE FROM dbo.Student WHERE ClassId = @0", classId);
                db.Execute(sql);

                sql = Sql.Builder.Append("DELETE FROM dbo.Class WHERE Id = @0", classId);
                db.Execute(sql);
            }
        }

        public Student GetStudent(int studentId)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Student").Where("id = @stdId", new { stdId = studentId });
                return db.Query<Student>(sql).FirstOrDefault();
            }
        }

        public void CreateStudent(Student student)
        {
            using (var db = new Database(_connectionName))
            {
                db.Insert("Student", "ID", new { FirstName = student.FirstName, LastName = student.LastName, Age = student.Age, GPA = student.GPA, ClassId = student.ClassId });
            }
        }

        public void DeleteStudent(int studentId)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Append("DELETE FROM dbo.Student WHERE Id = @0", studentId);
                db.Execute(sql);
            }
        }

        public void UpdateStudent(Student updatedStudent)
        {
            using (var db = new Database(_connectionName))
            {
                db.Update("Student", "ID", new { FirstName = updatedStudent.FirstName, LastName = updatedStudent.LastName, Age = updatedStudent.Age, GPA = updatedStudent.GPA }, updatedStudent.Id);
            }
        }

        #endregion

        #region Private Methods

        private List<Student> GetStudents(int classId)
        {
            using (var db = new Database(_connectionName))
            {
                var sql = Sql.Builder.Select("*").From("dbo.Student").Where("classId = @clsId", new { clsId = classId });
                return db.Query<Student>(sql).ToList();
            }
        }

        #endregion
    }
}
