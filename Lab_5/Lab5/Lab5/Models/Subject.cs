using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab5.Models
{
    public class Subject
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }

        public virtual ICollection<Exam> Exams { get; set; }

        public override string ToString()
        {
            return $"Subject(Name: {Name}, Date: {Date.ToShortDateString()})";
        }
    }
}
