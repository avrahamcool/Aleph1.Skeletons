using Aleph1.Skeletons.Proxy.Models;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aleph1.Skeletons.Proxy.Proxy.Moq
{
    internal class ProxyMoq : IProxy
    {
        private readonly List<Person> persons = new List<Person>();

        public ProxyMoq()
        {
            persons.Add(new Person() { ID = 1, FirstName = "אברהם", LastName = "אסודרי" });
            persons.Add(new Person() { ID = 2, FirstName = "Avraham", LastName = "Essoudry" });
        }

        public Task<List<Person>> GetPersons()
        {
            return Task.FromResult(persons);
        }

        public Task<Person> InsertPerson(Person person)
        {
            persons.Add(person);
            return Task.FromResult(person);
        }
    }
}
