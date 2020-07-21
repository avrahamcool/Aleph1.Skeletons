using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using System.Linq;

namespace Aleph1.Skeletons.WebAPI.DAL.Implementation
{
    internal class DAL : IDAL
    {
        private readonly PersonContext Context;
        public DAL(PersonContext context)
        {
            Context = context;
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }

        [Logged]
        public IQueryable<Person> GetPersons()
        {
            return Context.Persons.AsNoTracking();
        }

        [Logged]
        public Person GetPersonByID(int ID)
        {
            return Context.Persons.Find(ID);
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            return Context.Persons.Add(person);
        }

        [Logged]
        public Person DeletePerson(int ID)
        {
            Person toDelete = Context.Persons.Find(ID);
            if (toDelete != default)
            {
                return Context.Persons.Remove(toDelete);
            }
            return toDelete;
        }
    }
}
