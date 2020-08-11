using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using System.Collections.Generic;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Mock
{
    internal class DALMock : IDAL
    {
        private int uniqueID;
        private readonly List<Person> persons = new List<Person>();
        public DALMock()
        {
            InsertPerson(new Person() { FirstName = "אברהם", LastName = "אסודרי" });
            InsertPerson(new Person() { FirstName = "Avraham", LastName = "Essoudry" });
        }

        public void SaveChanges(string username) { }
        public void Dispose() { }


        public IQueryable<Person> GetPersons()
        {
            return persons.AsQueryable();
        }

        public Person GetPersonByID(int ID)
        {
            return persons.Find(p => p.ID == ID);
        }

        public Person InsertPerson(Person person)
        {
            person.ID = ++uniqueID;
            persons.Add(person);
            return person;
        }

        public Person DeletePerson(int ID)
        {
            Person toDelete = GetPersonByID(ID);
            persons.Remove(toDelete);
            return toDelete;
        }
    }
}
