using System.Collections.Generic;
using Quantum.Domain.Entities;

namespace Quantum.Domain.Abstractions
{
    public interface IRepository
    {
        List<Class> GetAllClasses();
        Class GetClass(int classId);

        void SaveClass(Class updatedClass);
        void CreateClass(Class newClass);
        void UpdateClass(Class updatedClass);
        void DeleteClass(int classId);

        List<Student> GetStudentsByLastname(string lastname);
        List<Student> GetAllStudents();
        Student GetStudent(int studentId);
        void UpdateStudent(Student updatedStudent);
        void CreateStudent(Student student);
        void DeleteStudent(int studentId);
    }
}