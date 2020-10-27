using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lab2.Models
{
    class Exam
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }

        public Guid SubjectId { get; set; }

        [ForeignKey("SubjectId")]
        public Subject Subject { get; set; }

        public Guid TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        public int Mark { get; set; }

        public override string ToString()
        {
            return $"Exam({Subject}, {Teacher})";
        }
    }
}
