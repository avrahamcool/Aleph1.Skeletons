using Aleph1.Logging;
using Aleph1.Skeletons.WebAPI.BL.Contracts;
using Aleph1.Skeletons.WebAPI.DAL.Contracts;
using Aleph1.Skeletons.WebAPI.Models;
using Aleph1.Skeletons.WebAPI.Models.Security;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Aleph1.Skeletons.WebAPI.BL.Implementation
{
    internal class BL : IBL
    {
        private readonly IDAL DAL;
        private readonly AuthenticationInfo CurrentUser;

        public BL(IDAL DAL, AuthenticationInfo currentUser)
        {
            this.DAL = DAL;
            CurrentUser = currentUser;
        }

        public void Dispose()
        {
            DAL.Dispose();
        }

        [Logged]
        public IEnumerable<Person> GetPersons()
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
        public IEnumerable<Person> SearchByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentNullException(nameof(searchTerm));
            };

            string searchTermSanitized = searchTerm.ToUpperInvariant();
            return DAL.GetPersons()
                .Where(p => p.FirstName.Contains(searchTermSanitized) || p.LastName.Contains(searchTermSanitized));
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            Person added = DAL.InsertPerson(person);
            DAL.SaveChanges(CurrentUser?.Username);
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
                target.BirthDate = personToUpdate.BirthDate;

                DAL.SaveChanges(CurrentUser?.Username);
            }

            return target;
        }

        [Logged]
        public Person DeletePerson(int ID)
        {
            Person deleted = DAL.DeletePerson(ID);
            DAL.SaveChanges(CurrentUser?.Username);
            return deleted;
        }
    }
}
