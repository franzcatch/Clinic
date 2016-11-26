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
        public string FirstName
        {
            get
            {
                return this.Name1;
            }
            set
            {
                this.Name1 = value;
            }
        }
        public string MiddleName
        {
            get
            {
                return this.Name2;
            }
            set
            {
                this.Name2 = value;
            }
        }
        public string LastName
        {
            get
            {
                return this.Name3;
            }
            set
            {
                this.Name3 = value;
            }
        }
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