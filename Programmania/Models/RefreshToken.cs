using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Programmania.Models
{
    [Table("Tokens")]
    public class RefreshToken
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string Token { get; set; }

        public string IpCreator { get; set; }

        public string LocationCreator { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        [JsonIgnore]
        public string IpRevoker { get; set; }

        public string LocationRevorker { get; set; }

        [JsonIgnore]
        public DateTime? Revoked { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsAlive => DateTime.UtcNow <= Expires && Revoked == null;

        [JsonIgnore]
        public User User { get; set; }

    }
}
