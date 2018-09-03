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

        [Logged(LogParameters = false)]
        public BL(IDAL DAL)
        {
            this.DAL = DAL;
        }

        [Logged]
        public IQueryable<Person> GetPersons()
        {
            return DAL.GetPersons();
        }

        [Logged]
        public Person GetPersonByID(int ID)
        {
            return DAL.GetPersonByID(ID);
        }

        [Logged]
        public Person GetPersonByName(string firstName)
        {
            return DAL.GetPersons().FirstOrDefault(p => String.Equals(p.FirstName, firstName, StringComparison.CurrentCultureIgnoreCase));
        }

        [Logged]
        public Person InsertPerson(Person person)
        {
            return DAL.InsertPerson(person);
        }
    }
}

