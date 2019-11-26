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
            foreach (var pair in wordDict)
            {
                Console.WriteLine("Key = {0}, Value = {1}", pair.Key, pair.Value);
            }
            return CreateWords(wordDict);
        }
        List<Word> CreateWords(Dictionary<string, dynamic> wordDict)
        {
            var wordList = new List<Word>();
            return wordList;
        }
        static void Main(string[] args)
        {
            var client = new JishoClient();
            var searchTask = client.SearchWord("iie");
            searchTask.Wait();
        }
    }
}
