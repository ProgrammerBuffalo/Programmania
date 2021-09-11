using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    public class Reward
    {
        public Reward()
        {

        }

        public Reward(string type, int expierence, DateTime date)
        {
            Type = type;
            Expierence = expierence;
            Date = date;
        }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "exp")]
        public int Expierence { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }
    }
}
