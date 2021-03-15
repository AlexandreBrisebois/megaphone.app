using Megaphone.Standard.Representations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megaphone.App.Data.Representations
{
    public class FeedListRepresentation : Representation
    {

        [JsonPropertyName("feeds")]
        public List<FeedRepresentation> Feeds { get; set; } = new List<FeedRepresentation>();

        [JsonPropertyName("updated")]
        public DateTimeOffset Updated { get; set; }
    }
}
