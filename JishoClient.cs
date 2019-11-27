using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace jisharp
{
    public class JishoClient
    {
        const string BASE_URL = "https://jisho.org/api/v1/search/words?keyword={0}";
        HttpClient client;

        public JishoClient()
        {
            this.client = new HttpClient();
        }
        public async Task<List<Word>> SearchWord(string word, bool english = false)
        {
            if (english && !word.Contains('"'))
            {
                word = string.Format("\"{0}\"", word);
            }
            var resp = await client.GetAsync(string.Format(BASE_URL, word));
            var wordDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await resp.Content.ReadAsStringAsync());
            return CreateWords(wordDict);
        }
        List<Word> CreateWords(Dictionary<string, dynamic> wordsDict)
        {
            var wordList = new List<Word>();
            foreach (var wordDict in wordsDict["data"])
            {
                var wordObj = wordDict as Newtonsoft.Json.Linq.JObject;
                var japaneseWords = CreateJpWords(wordObj);
                var englishDefinitions = CreateEngDefs(wordObj);
                var commonString = wordObj["is_common"]?.ToString();
                bool? isCommon = null;
                if(!(commonString is null)) {
                    isCommon = Boolean.Parse(commonString);
                }
                var tags = new List<string>();
                foreach (var tag in wordObj["tags"]) {
                    tags.Add(tag.ToString());
                }
                var word = new Word {
                    EnglishDefinitions = englishDefinitions,
                    JapaneseWords = japaneseWords,
                    IsCommon = isCommon,
                    Tags = tags
                };
                wordList.Add(word);
            }
            return wordList;
        }
        List<JapaneseWord> CreateJpWords(Newtonsoft.Json.Linq.JObject wordObj)
        {
            var japaneseWords = new List<JapaneseWord>();
            foreach (var japaneseWordObj in wordObj["japanese"])
            {
                var word = japaneseWordObj["word"]?.ToString() ?? "";
                var reading = japaneseWordObj["reading"]?.ToString() ?? "";
                var japaneseWord = new JapaneseWord
                {
                    Reading = reading,
                    Word = word
                };
                japaneseWords.Add(japaneseWord);
            }
            return japaneseWords;
        }
        List<EnglishDefinition> CreateEngDefs(Newtonsoft.Json.Linq.JObject wordObj)
        {
            var englishDefinitions = new List<EnglishDefinition>();
            foreach (var englishDefObj in wordObj["senses"])
            {
                var definitions = new List<string>();
                var partsOfSpeech = new List<string>();
                var tags = new List<string>();
                foreach (var definition in englishDefObj["english_definitions"]) {
                    definitions.Add(definition.ToString());
                }
                foreach (var partOfSpeech in englishDefObj["parts_of_speech"]) {
                    partsOfSpeech.Add(partOfSpeech.ToString());
                }
                foreach (var tag in englishDefObj["tags"]) {
                    tags.Add(tag.ToString());
                }
                var englishDefinition = new EnglishDefinition {
                    Definitions = definitions,
                    PartsOfSpeech = partsOfSpeech,
                    Tags = tags
                };
                englishDefinitions.Add(englishDefinition);
            }
            return englishDefinitions;
        }
        static void Main(string[] args)
        {
            var client = new JishoClient();
            var searchTask = client.SearchWord("vinland");
            searchTask.Wait();
            System.Console.WriteLine(searchTask.Result);
        }
    }
}
