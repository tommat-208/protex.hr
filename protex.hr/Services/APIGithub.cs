using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace protex.hr.Services
{
    public static class APIGithub
    {

        private static string url = $"https://api.github.com/repos/tommat-208/protex.hr/commits";
        private static string token = "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"; // Validita' 60 giorni; scade il 21 feb 2025
        private static HttpClient _httpClient = GetHttpClient();

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

            return httpClient;
        }

        public static async Task<List<string>> GetChangelog()
        {
            // Imposta gli header
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Aggiungi l'header User-Agent (obbligatorio per le API GitHub)
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClient");

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Verifica il risultato
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Risultato:"+result);
                    var l = GetMessages(result);
                    Console.WriteLine(l.Count);

                    return l;
                }
                else
                {
                    Console.WriteLine($"Errore: {response.StatusCode}");
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eccezione: {ex.Message}");
                return null;
            }
        }


        private static List<string> GetMessages(string json)
        {
            using JsonDocument document = JsonDocument.Parse(json);
            var messages = new List<string>();

            foreach (JsonElement element in document.RootElement.EnumerateArray())
            {
                // Naviga al campo "commit" -> "message"
                if (element.TryGetProperty("commit", out JsonElement commit) &&
                    commit.TryGetProperty("message", out JsonElement message) &&
                    commit.TryGetProperty("author", out JsonElement author) &&
                    author.TryGetProperty("date", out JsonElement date))
                {
                    messages.Add($"{date.GetString()}\n-{message.GetString()}");
                }
            }

            return messages;
        }
    }
}
