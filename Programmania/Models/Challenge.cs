using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    [Table("Challenges")]
    public class Challenge
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        public DateTime Created { get; set; }

        public Course Course { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ICollection<Test> Tests { get; set; }

        public ICollection<UserChallenge> ChallengeUsers { get; set; }

        public Challenge()
        {
            Tests = new List<Test>();
        }

    }
}
