using System.Collections.Generic;
using Newtonsoft.Json;

namespace jisharp
{
    public struct Word
    {
        [JsonProperty("senses")]
        public List<EnglishDefinition> EnglishDefinitions { get; set; }
        public bool? IsCommon { get; set; }
        public List<string> Tags { get; set; }
        [JsonProperty("japanese")]
        public List<JapaneseWord> JapaneseWords { get; set; }
    }

    public struct JapaneseWord
    {
        public string Word { get; set; }
        public string Reading { get; set; }
    }

    public struct EnglishDefinition
    {
        [JsonProperty("english_definitions")]
        public List<string> Definitions { get; set; }
        [JsonProperty("parts_of_speech")]
        public List<string> PartsOfSpeech { get; set; }
        public List<string> Tags { get; set; }
    }
}