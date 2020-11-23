using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab3.Models
{
    public class Teacher
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [InverseProperty("Teacher")]
        public virtual ICollection<Exam> Exams { get; set; }

        public override string ToString()
        {
            return $"Teacher({LastName} {FirstName} {MiddleName})";
        }
    }
}
