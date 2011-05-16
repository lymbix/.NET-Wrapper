using System;
using Newtonsoft.Json;

namespace Lymbix.ToneAPI.Models
{
    [Serializable]
    [JsonObject]
    public class ArticleInfo
    {
        [JsonProperty(PropertyName = "article")]
        public string Article;
        [JsonProperty(PropertyName = "ignored_terms")]
        public string[] IgnoredTerms;

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

        [JsonProperty(PropertyName = "dominant_emotion")]
        public string DominantCategory;
        [JsonProperty(PropertyName = "article_sentiment")]
        public Sentiment ArticleSentiment;
        [JsonProperty(PropertyName = "coverage")]
        public int? Coverage;
        [JsonProperty(PropertyName = "intense_sentence")]
        public IntenseSentence IntenseSentence;
        [JsonProperty(PropertyName = "reference_id")]
        public int? ReferenceId;
        [JsonProperty(PropertyName = "clarity")]
        public double? Clarity;

        [JsonProperty(PropertyName = "sentences_data")]
        public SentenceData[] SentenceData;
    }
}