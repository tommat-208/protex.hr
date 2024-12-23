using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace protex.hr.Services
{
    public static class APIGithub
    {

        private static string url = $"https://api.github.com/repos/tommat-208/protex.ht/events";
        private static HttpClient _httpClient = GetHttpClient();


        public static async Task<string> GetChangelog()
        {


            _httpClient.DefaultRequestHeaders.UserAgent.TryParseAdd("request"); // Necessario per l'API GitHub
            string json;

            var res = await _httpClient.GetAsync(url);

            if (res.IsSuccessStatusCode)
                json = await res.Content.ReadAsStringAsync();
            else
                json = null;

            return json;
        }


        private static HttpClient GetHttpClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(url)
            };

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            return httpClient;
        }
    }
}
