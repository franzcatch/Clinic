using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class User : Entity
    {
        private Role _role;

        public string Username { get; set; }
        public string Password { get; set; }
        public Role Role
        {
            get
            {
                if (_role == null)
                {
                    _role = DataLayer.RoleDL.Get(Constants.User);
                }

                return _role;
            }
            set
            {
                _role = value;
            }
        }
    }
}