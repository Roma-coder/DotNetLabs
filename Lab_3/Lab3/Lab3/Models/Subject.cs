using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab3.Models
{
    public class Subject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public DateTime Date { get; set; }

        [InverseProperty("Subject")]
        public virtual ICollection<Exam> Exams { get; set; }

        public override string ToString()
        {
            return $"Subject(Name: {Name}, Date: {Date.ToShortDateString()})";
        }
    }
}
