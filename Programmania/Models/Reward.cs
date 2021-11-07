using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    [Table("Rewards")]
    public class Reward
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public int Experience { get; set; }

        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
