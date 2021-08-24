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

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ICollection<Lesson> Lessons { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        [Column("stream_id")]
        public Guid StreamId { get; set; }

        public User()
        {
            Achievements = new List<Achievement>();
            Lessons = new List<Lesson>();
            RefreshTokens = new List<RefreshToken>();
        }
    }
}