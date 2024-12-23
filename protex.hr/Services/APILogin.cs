using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using protexhr.repository;
using Repository;

namespace protex.hr.Services
{
    public static class APILogin
    {
        private static string url = "https://10.99.99.46:7062";
        private static HttpClient _httpClient { get { return GetHttpClient(); } }


        public static async Task<bool> VerificaPassword(string username, string password) 
        {
            var up = await GetUserPerson(username);

            if (up != null)
            {
                return Hasher.VerifyPassword(password, up.PasswordHash);
            }

            return false;
        }


        public static async Task<bool> CambiaPassword(string username, string oldPw, string newPw)
        {
            if (await VerificaPassword(username, oldPw))
            {
                Console.WriteLine("APILogin.CambiaPassword : verifica password passata");

                var up = await GetUserPerson(username);

                up.PasswordHash = Hasher.HashPassword(newPw);

                return await UpdateUserPerson(up);
            }

            return false;
        }


        private static async Task<bool> UpdateUserPerson(UserPerson up)
        {
            var v = new ModelValidator();
            v.Validate(up);


            var json = JsonConvert.SerializeObject(up);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PutAsync(url + "/Auth", content);

            return res.IsSuccessStatusCode;
        }


        public static async Task<bool> DeleteUserPerson(string username)
        {
            var res = await _httpClient.DeleteAsync($"{url}/Auth?username={username}");

            return res.IsSuccessStatusCode;
        }


        public static async Task<UserPerson> GetUserPerson(string username)
        {
            var res = await _httpClient.GetAsync(url + $"/Auth?username={username}");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;
                var up = JsonConvert.DeserializeObject<UserPerson>(json);
                return up;
            }

            return null;
        }



        public static async Task<bool> InsertUserPerson(UserPerson up)
        {
            var v = new ModelValidator();
            v.Validate(up);

            up.PasswordHash = Hasher.HashPassword(up.PasswordHash);

            var json = JsonConvert.SerializeObject(up);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await _httpClient.PostAsync(url+"/Auth", content);

            return res.IsSuccessStatusCode;
        }


        public static async Task<List<UserType>> GetAllUserType()
        {
            var res = await _httpClient.GetAsync(url + "/Auth/usertypes");

            if (res.IsSuccessStatusCode)
            {
                string json = res.Content.ReadAsStringAsync().Result;

                var l = JsonConvert.DeserializeObject<List<UserType>>(json);
                return l;
            }

            return null;
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

            return httpClient;
        }

    }
}
