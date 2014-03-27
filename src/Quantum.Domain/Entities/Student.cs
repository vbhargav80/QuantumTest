using System.ComponentModel.DataAnnotations;
using System.Net.Configuration;

namespace Quantum.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int ClassId { get; set; }
        [Required]
        public int Age { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        [Required]
        public decimal GPA { get; set; }
        [Required]
        public string LastName { get; set; }

        public bool IsBright { get { return (GPA >= 3.2m); }}
    }
}