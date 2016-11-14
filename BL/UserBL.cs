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

        public void Update(User user)
        {
            DataLayer.UserDL.Update(user);
        }
    }
}