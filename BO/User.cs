using Clinic.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class User : Entity
    {
        public string Username { get; set; }

        public string Password { get; set; }
        public Role Role { get; set; }
    }
}