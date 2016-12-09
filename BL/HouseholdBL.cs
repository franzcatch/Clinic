using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class HouseholdBL
    {
        public Household Get(int id)
        {
            return DataLayer.HouseholdDL.Get(id);
        }

        public List<Household> GetAll() {
            return DataLayer.HouseholdDL.GetAll();
        }

        public Household GetByUserId(int userId)
        {
            return DataLayer.HouseholdDL.GetByUserId(userId);
        }

        public void Create(Household household)
        {
            DataLayer.HouseholdDL.Create(household);
        }
        
        public void Update(Household household)
        {
            if (household.Id.HasValue)
            {
                DataLayer.HouseholdDL.Update(household);
            }
            else
            {
                DataLayer.HouseholdDL.Create(household);
            }

            household.People.ForEach(person =>
                BusinessLayer.PersonBL.Update(household.Id.Value, person)
            );
        }
    }
}