using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Programmania.Models
{
    [Table("Users")]
    public class User
    {
        [Column("Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; }

        [Column("Nickname")]
        [Required]
        [MaxLength(15), MinLength(5)]
        public string Name { get; set; }

        [Column("Login")]
        [Required]
        [MaxLength(20), MinLength(8)]
        public string Login { get; set; }

        [Column("Password")]
        [Required]
        [MaxLength(20), MinLength(5)]
        public string Password { get; set; }

        [Column("Experience")]
        public int Exp { get; set; }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ICollection<Achievement> Achievements { get; set; }

        public ICollection<UserDiscipline> UserDisciplines { get; set; }

        public ICollection<UserChallenge> UserChallenges { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public ICollection<Reward> Rewards { get; set; }

        public ChallengeStatistics ChallengeStatistics { get; set; }
        
        [Column("img_stream_id")]
        public Guid ImageId { get; set; }

        public User()
        {
            Achievements = new List<Achievement>();
            RefreshTokens = new List<RefreshToken>();
            Rewards = new List<Reward>();
        }
    }
}