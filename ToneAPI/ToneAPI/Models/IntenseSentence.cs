using System;
using Newtonsoft.Json;

namespace Lymbix.ToneAPI.Models
{
    [Serializable]
    [JsonObject]
    public class IntenseSentence
    {
        [JsonProperty(PropertyName = "sentence")]
        public string Sentence;
        [JsonProperty(PropertyName = "dominant_emotion")]
        public string Dominant_emotion;
        [JsonProperty(PropertyName = "intensity")]
        public double? Intensity;
    }
}
