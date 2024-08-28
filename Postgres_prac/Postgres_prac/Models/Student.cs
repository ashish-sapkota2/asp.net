using System.ComponentModel.DataAnnotations;

namespace Postgres_prac.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string FirstName {  get; set; }
        public string MiddleName { get; set; } = null;
        public string LastName { get; set; }

        public string FullName => $"{FirstName}{MiddleName}{LastName}";
        public string EmailAddress { get; set; }
        public int PhoneNumber { get; set; }
        public string Gender { get; set; }

    }
}
