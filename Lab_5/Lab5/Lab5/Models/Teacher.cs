using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Display(Name = "По батькові")]
        public string MiddleName { get; set; }

        [Display(Name = "Прізвище")]
        public string LastName { get; set; }


        public virtual ICollection<Exam> Exams { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
