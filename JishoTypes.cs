using System.Collections.Generic;

namespace jisharp
{
    public struct Word
    {
        public List<EnglishDefinition> EnglishDefinitions { get; }
        public bool IsCommon { get; }
        public List<string> Tags { get; }
        public List<JapaneseWord> JapaneseWords { get; }
    }

    public struct JapaneseWord
    {
        public string Word { get; }
        public string Reading { get; }
    }

    public struct EnglishDefinition
    {
        public List<string> Definitions { get; }
        public List<string> PartsOfSpeech { get; }
        public List<string> Tags { get; }
    }
}