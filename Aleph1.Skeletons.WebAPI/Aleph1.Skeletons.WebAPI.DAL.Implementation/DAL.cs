using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using System.Collections.Generic;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
    internal class DAL : IDAL
    {
        private int uniqueID = 0;
        private readonly List<Person> persons = new List<Person>();
        public DAL()
        {
            InsertPerson(new Person() { FirstName = "אברהם", LastName = "אסודרי" });
            InsertPerson(new Person() { FirstName = "Avraham", LastName = "Essoudry" });
        }

        public void SaveChanges() { }
        public void Dispose() { }

        [Logged]
        public IQueryable<Person> GetPersons()
        {
            return persons.AsQueryable();
        }

        [Logged]
        public Person GetPersonByID(int ID)
        {
            return persons.Find(p => p.ID == ID);
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            person.ID = ++uniqueID;
            persons.Add(person);
            return person;
        }

        [Logged]
        public Person DeletePerson(int ID)
        {
            Person toDelete = GetPersonByID(ID);
            persons.Remove(toDelete);
            return toDelete;
        }
    }
}
