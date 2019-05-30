using Aleph1.Logging;
using Aleph1.Skeletons.Proxy.Models;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Aleph1.Skeletons.Proxy.Proxy.Implementation
{
    internal class Proxy : IProxy
    {
        private readonly HttpClient httpClient;

        [Logged(LogParameters = false)]
        public Proxy()
        {
            //for Windows Auth use:
            //httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true })

            httpClient = new HttpClient()
            {
                BaseAddress = SettingsManager.ServiceBaseUrl
            };
        }

        [Logged]
        public async Task<List<Person>> GetPersons()
        {
            HttpResponseMessage response = await httpClient.GetAsync("api/Person");

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<List<Person>>();
        }

        [Logged]
        public async Task<Person> InsertPerson(Person person)
        {
            StringContent dataAsJSON = new StringContent(JsonConvert.SerializeObject(person), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync("api/Person", dataAsJSON);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<Person>();
        }
    }
}
