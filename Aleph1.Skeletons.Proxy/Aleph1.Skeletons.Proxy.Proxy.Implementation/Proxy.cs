using Aleph1.Logging;
using Aleph1.Skeletons.Proxy.Models;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Aleph1.Skeletons.Proxy.Proxy.Implementation
{
    internal class Proxy : IProxy
    {
        private readonly HttpClient httpClient;

        public Proxy()
        {
            //for Windows Authentication use:
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
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            return await response.Content.ReadAsAsync<List<Person>>();
        }

        [Logged]
        public async Task<Person> InsertPerson(Person person)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/Person", person);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync();
                throw new Exception(error);
            }
            return await response.Content.ReadAsAsync<Person>();
        }
    }
}
