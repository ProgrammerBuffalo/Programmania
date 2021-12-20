using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Programmania.Models
{
    [Table("Disciplines")]
    public class Discipline
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Points")]
        public int Points { get; set; }

        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<UserDiscipline> UserDisciplines { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public Discipline()
        {
            Lessons = new List<Lesson>();
        }
    }
}
