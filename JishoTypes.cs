using System.Collections.Generic;

namespace jisharp
{
    public struct Word
    {
        public List<EnglishDefinition> EnglishDefinitions { get; set; }
        public bool? IsCommon { get; set; }
        public List<string> Tags { get; set; }
        public List<JapaneseWord> JapaneseWords { get; set; }
    }

    public struct JapaneseWord
    {
        public string Word { get; set; }
        public string Reading { get; set; }
    }

    public struct EnglishDefinition
    {
        public List<string> Definitions { get; set; }
        public List<string> PartsOfSpeech { get; set; }
        public List<string> Tags { get; set; }
    }
}