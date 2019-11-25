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
        public async Task<Dictionary<string, dynamic>> SearchWord(string word, bool english = false)
        {
            if (english) {
                word = string.Format("\"{0}\"", word);
            }
            var resp = await client.GetAsync(string.Format(BASE_URL, word));
            var wordDict = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(await resp.Content.ReadAsStringAsync());
            return wordDict;
        }
        static void Main(string[] args)
        {
            var client = new JishoClient();
            var searchTask = client.SearchWord("no", true);
            searchTask.Wait();
            foreach (var pair in searchTask.Result)
            {
                Console.WriteLine("Key = {0}, Value = {1}", pair.Key, pair.Value);
            }
        }
    }
}
