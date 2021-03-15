using Megaphone.Standard.Representations;
using System.Text.Json.Serialization;

namespace Megaphone.App.Data.Representations
{
    public class FeedRepresentation : Representation
    {
        [JsonPropertyName("display")]
        public string Display { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
