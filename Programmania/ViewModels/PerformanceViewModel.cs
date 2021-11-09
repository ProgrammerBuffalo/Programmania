using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Programmania.ViewModels
{
    public class PerformanceViewModel
    {
        public PerformanceViewModel()
        {
            JObject pairs = JObject.Parse(System.IO.File.ReadAllText("wwwroot\\res\\calendar.json"));
            JArray array = (JArray)pairs["months"];
            Months = new string[array.Count];
            for (int i = 0; i < array.Count; i++)
                Months[i] = array[i].ToString();

            array = (JArray)pairs["days"];
            Days = new string[array.Count];
            for (int i = 0; i < array.Count; i++)
                Days[i] = array[i].ToString();
        }

        public PerformanceViewModel(IEnumerable<Models.Reward> rewards) : this()
        {
            Rewards = rewards;
        }

        [JsonProperty("months")]
        public string[] Months { get; set; }

        [JsonProperty("days")]
        public string[] Days { get; set; }

        [JsonProperty("rewards")]
        public IEnumerable<Models.Reward> Rewards { get; set; }
    }
}
