using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab2.Models
{
    class Teacher : IReadableFromString
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [InverseProperty("Teacher")]
        public virtual ICollection<Exam> Exams { get; set; }

        public void ReadFromStringArray(string[] values)
        {
            LastName = values[1];
            FirstName = values[2];
            MiddleName = values[3];
        }
    }
}
