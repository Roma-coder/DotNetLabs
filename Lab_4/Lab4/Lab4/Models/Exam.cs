using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab4.Models
{
    public class Exam
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }
        [Display(Name = "Студент")]
        public Student Student { get; set; }


        public Guid SubjectId { get; set; }
        [Display(Name = "Предмет")]
        public Subject Subject { get; set; }


        public Guid TeacherId { get; set; }
        [Display(Name = "Вчитель")]
        public Teacher Teacher { get; set; }

        [Display(Name = "Оцінка")]
        public int Mark { get; set; }

        public override string ToString()
        {
            return $"Exam({Subject}, {Teacher})";
        }
    }
}
