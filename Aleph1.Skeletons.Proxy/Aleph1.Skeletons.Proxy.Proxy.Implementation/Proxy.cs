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

        public void Dispose()
        {
            httpClient.Dispose();
        }

        [Logged]
        public async Task<List<Person>> GetPersons()
        {
            HttpResponseMessage response = await httpClient.GetAsync(new Uri("api/Person", UriKind.Relative)).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception(error);
            }
            return await response.Content.ReadAsAsync<List<Person>>().ConfigureAwait(false);
        }

        [Logged]
        public async Task<Person> InsertPerson(Person person)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(new Uri("api/Person", UriKind.Relative), person).ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                string error = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new Exception(error);
            }
            return await response.Content.ReadAsAsync<Person>().ConfigureAwait(false);
        }
    }
}
