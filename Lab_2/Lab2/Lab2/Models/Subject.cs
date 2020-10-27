using Lab2.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab2.Models
{
    class Subject : IReadableFromString
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public DateTime Date { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Exam> Exams { get; set; }

        public void ReadFromStringArray(string[] values)
        {
            Name = values[1];
            Date = DateTime.Parse(values[2]);
        }

        public override string ToString()
        {
            return $"Subject(Name: {Name}, Date: {Date.ToShortDateString()})";
        }
    }
}
