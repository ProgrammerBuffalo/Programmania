using System;
using System.Collections.Generic;
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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ICollection<User> Users { get; set; }

        public Discipline Discipline { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public Lesson()
        {
            Users = new List<User>();
        }
    }
}
