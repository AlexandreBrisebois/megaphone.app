using Megaphone.Standard.Representations;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Megaphone.App.Data.Representations
{
    public class ResourceListRepresentation : Representation
    {
        [JsonPropertyName("resources")]
        public List<ResourceRepresentation> Resources { get; set; } = new List<ResourceRepresentation>();

        [JsonPropertyName("updated")]
        public DateTimeOffset Updated { get; set; }

        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }
    }
}
