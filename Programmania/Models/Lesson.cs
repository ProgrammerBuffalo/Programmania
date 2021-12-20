using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programmania.Models
{
    public class Lesson
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("Order")]
        public int Order { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public int TestId { get; set; }

        public Test Test { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public Lesson()
        {

        }
    }
}
