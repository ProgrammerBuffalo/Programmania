using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    [Table("ChallengeStatistics")]
    public class ChallengeStatistics
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public int Wins { get; set; }

        public int Loses { get; set; }

        public int Draws { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }
    }
}
