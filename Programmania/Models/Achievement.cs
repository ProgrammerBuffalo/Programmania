using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programmania.Models
{
    [Table("Achievements")]
    public class Achievement
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Description")]
        public string Desc { get; set; }

        [Column("Points")]
        public int Points { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ICollection<User> Users { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public Achievement()
        {
            Users = new List<User>();
        }
    }
}
