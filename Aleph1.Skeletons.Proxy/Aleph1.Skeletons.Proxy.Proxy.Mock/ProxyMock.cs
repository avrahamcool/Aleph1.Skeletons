using Aleph1.Skeletons.Proxy.Models;
using Aleph1.Skeletons.Proxy.Proxy.Contracts;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aleph1.Skeletons.Proxy.Proxy.Mock
{
    internal class ProxyMock : IProxy
    {
        private int uniqueID = 0;
        private readonly List<Person> persons = new List<Person>();

        public ProxyMock()
        {
            InsertPerson(new Person() { FirstName = "אברהם", LastName = "אסודרי" }).Wait();
            InsertPerson(new Person() { FirstName = "Avraham", LastName = "Essoudry" }).Wait();
        }

        public Task<List<Person>> GetPersons()
        {
            return Task.FromResult(persons);
        }

        public Task<Person> InsertPerson(Person person)
        {
            person.ID = ++uniqueID;
            persons.Add(person);
            return Task.FromResult(person);
        }
    }
}
