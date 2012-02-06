using System;
using Newtonsoft.Json;

namespace Lymbix.ToneAPI.Models
{
    [Serializable]
    [JsonObject]
    public class Sentiment
    {
        [JsonProperty(PropertyName = "sentiment")]
        public string SentimentType;
        [JsonProperty(PropertyName = "score")]
        public double? Score;
    }
}
