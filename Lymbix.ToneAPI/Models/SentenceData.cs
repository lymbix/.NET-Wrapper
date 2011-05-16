using System;
using Newtonsoft.Json;

namespace Lymbix.ToneAPI.Models
{
    [Serializable]
    [JsonObject]
    public class SentenceData
    {
        [JsonProperty(PropertyName = "sentence")]
        public string Sentence;
        [JsonProperty(PropertyName = "dominant_emotion")]
        public string DominantEmotion;

        [JsonProperty(PropertyName = "affection_friendliness")]
        public double? AffectionFriendliness;
        [JsonProperty(PropertyName = "enjoyment_elation")]
        public double? EnjoymentElation;
        [JsonProperty(PropertyName = "amusement_excitement")]
        public double? AmusementExcitement;
        [JsonProperty(PropertyName = "contentment_gratitude")]
        public double? ContentmentGratitude;
        [JsonProperty(PropertyName = "sadness_grief")]
        public double? SadnessGrief;
        [JsonProperty(PropertyName = "anger_loathing")]
        public double? AngerLoathing;
        [JsonProperty(PropertyName = "fear_uneasiness")]
        public double? FearUneasiness;
        [JsonProperty(PropertyName = "humiliation_shame")]
        public double? HumiliationShame;

        [JsonProperty(PropertyName = "sentence_sentiment")]
        public Sentiment SentenceSentiment;
        [JsonProperty(PropertyName = "ignored_terms")]
        public string[] IgnoredTerms;
        [JsonProperty(PropertyName = "coverage")]
        public int? Coverage;
        [JsonProperty(PropertyName = "clarity")]
        public double? Clarity;
    }
}
