using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Moq
{
    internal class DALMoq : IDAL
    {
        private List<Person> persons = new List<Person>();
        
        public DALMoq()
        {
            persons.Add(new Person() { ID = 1, FirstName = "אברהם", LastName = "אסודרי" });
            persons.Add(new Person() { ID = 2, FirstName = "Avraham", LastName = "Essoudry" });
        }

        public IQueryable<Person> GetPersons()
        {
            return persons.AsQueryable();
        }

        public Person GetPersonByID(int ID)
        {
            return persons.FirstOrDefault(p => p.ID == ID);
        }

        public Person InsertPerson(Person person)
        {
            persons.Add(person);
            return person;
        }
    }
}
