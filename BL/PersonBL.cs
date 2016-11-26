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

        public void Update(Person person)
        {
            DataLayer.PersonDL.Update(person);
            BusinessLayer.EntityBL.Update(person);
        }
    }
}