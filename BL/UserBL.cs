using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class UserBL
    {
        public User Get(string userId, string password)
        {
            return DataLayer.UserDL.Get(userId, password);
        }

        public void Create(User user)
        {
            DataLayer.UserDL.Create(user);
        }

        public List<User> GetAdmins()
        {
            return DataLayer.UserDL.GetAdmins();
        }

        public List<User> GetClients()
        {
            return DataLayer.UserDL.GetClients();
        }

        public List<User> GetStaff()
        {
            return DataLayer.UserDL.GetStaff();
        }

        /// <summary>
        /// Household should already exist if creating user this way
        /// </summary>
        /// <param name="householdId"></param>
        /// <param name="user"></param>
        public void Create(int householdId, User user)
        {
            var person = DataLayer.PersonDL.GetPayerByHouseholdId(householdId);

            if (!person.IsEmpty())
            {
                var newUser = (User)((Entity)person).Copy();
                newUser.Username = user.Username;
                newUser.Password = user.Password;

                DataLayer.UserDL.Create(user);
            }
        }

        public void Update(User user)
        {
            if (user.EntityId == null)
            {
                BusinessLayer.EntityBL.Create(user);
            }
            else
            {
                BusinessLayer.EntityBL.Update(user);
            }

            DataLayer.UserDL.Update(user);
        }

        public void Delete(User user)
        {
            DataLayer.UserDL.Delete(user);
        }
    }
}