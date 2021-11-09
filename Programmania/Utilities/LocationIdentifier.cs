using Newtonsoft.Json.Linq;
using System.Net;

namespace Programmania.Utilities
{
    public static class LocationIdentifier
    {
        public static string GetLocationByIP(string ip)
        {
            string clientInfo = new WebClient().DownloadString("http://ipinfo.io/" + ip + "?token=cef89ac7fb5690");
            var obj = JObject.Parse(clientInfo);

            string region = (string)obj["country"] + ";" + (string)obj["city"];

            return region;
        }

    }
}
