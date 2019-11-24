using System;
using System.Net.Http;

namespace jisharp
{
    public class JishoClient
    {
        const string BASE_URL = "https://jisho.org/api/v1/search/";
        HttpClient client;

        public JishoClient()
        {
            this.client = new HttpClient();
        }
    }
}
