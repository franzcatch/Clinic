using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class PersonBL
    {
        public Person GetByUserId(int userId)
        {
            return DataLayer.PersonDL.GetByUserId(userId);
        }

        public Person GetPayerByHouseholdId(int householdId)
        {
            return DataLayer.PersonDL.GetPayerByHouseholdId(householdId);
        }

        public void Update(int householdId, Person person)
        {
            if (person.EntityId == null)
            {
                BusinessLayer.EntityBL.Create(person);
            }
            else
            {
                BusinessLayer.EntityBL.Update(person);
            }

            if (!person.Id.HasValue)
            {
                DataLayer.PersonDL.Create(householdId, person);
            }
            else
            {
                DataLayer.PersonDL.Update(householdId, person);
            }
        }
    }
}