using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<IEnumerable<Word>> SearchWord(string word, bool english = false)
        {
            if (english && !word.Contains('"'))
            {
                word = string.Format("\"{0}\"", word);
            }
            var resp = await client.GetAsync(string.Format(BASE_URL, word));
            var wordObj = JsonConvert.DeserializeObject<JObject>(await resp.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<IEnumerable<Word>>(wordObj["data"].ToString());
        }
        static void Main(string[] args)
        {
            var client = new JishoClient();
            var searchTask = client.SearchWord("vinland");
            searchTask.Wait();
            Console.WriteLine(searchTask.Result);
        }
    }
}
