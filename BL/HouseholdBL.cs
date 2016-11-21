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
        public User Get(string userId, string password)
        {
            return DataLayer.UserDL.Get(userId, password);
        }

        public void Create(Household household)
        {
            DataLayer.HouseholdDL.Create(household);
        }
        
        public void Update(Household household)
        {
            DataLayer.HouseholdDL.Update(household);
        }
    }
}