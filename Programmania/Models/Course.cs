using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programmania.Models
{
    [Table("Courses")]
    public class Course
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int LessonCount { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }
    }
}
