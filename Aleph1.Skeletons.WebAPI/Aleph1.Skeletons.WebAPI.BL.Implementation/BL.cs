using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;

using System;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
{
    internal class BL : IBL
    {
        private readonly IDAL DAL;

        public BL(IDAL DAL)
        {
            this.DAL = DAL;
        }

        public void Dispose()
        {
            DAL.Dispose();
        }

        [Logged]
        public IQueryable<Person> GetPersons()
        {
            return DAL.GetPersons();
        }

        [Logged]
        public int GetPersonsCount()
        {
            return DAL.GetPersons().Count();
        }

        [Logged]
        public Person GetPersonByID(int ID)
        {
            return DAL.GetPersonByID(ID);
        }

        [Logged]
        public IQueryable<Person> SearchByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentNullException("searchTerm");
            };

            string searchTermSanitized = searchTerm.ToLower();
            return DAL.GetPersons()
                .Where(p => p.FirstName.Contains(searchTermSanitized) || p.LastName.Contains(searchTermSanitized));
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            Person added = DAL.InsertPerson(person);
            DAL.SaveChanges();
            return added;
        }

        [Logged]
        public Person UpdatePerson(int ID, Person personToUpdate)
        {
            Person target = DAL.GetPersonByID(ID);
            if (target != default)
            {
                target.FirstName = personToUpdate.FirstName;
                target.LastName = personToUpdate.LastName;

                DAL.SaveChanges();
            }

            return target;
        }

        [Logged]
        public Person DeletePerson(int ID)
        {
            Person deleted = DAL.DeletePerson(ID);
            DAL.SaveChanges();
            return deleted;
        }
    }
}
