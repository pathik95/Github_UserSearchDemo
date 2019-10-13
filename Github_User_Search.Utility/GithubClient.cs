using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Github_User_Search.Utility
{
    public class GitHubAPIClient : IGithubClient
    {
        private HttpClient client;

        /// <summary>
        /// Public Constructor
        /// </summary>
        /// <param name="baseURL"></param>
        public GitHubAPIClient(string baseURL)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseURL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.V3_JSON));
        }


        /// <summary>
        /// Method to get connetion URL
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiEndPoint">API End Point</param>
        /// <param name="accessToken">Access token if required optional</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string apiEndPoint, string accessToken = null) where T : class
        {

            client.DefaultRequestHeaders.Accept.Clear();

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }

            var response = await client.GetAsync(apiEndPoint);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return null;

        }

    }
}
