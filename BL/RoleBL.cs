using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class RoleBL
    {
        public Role Get(int id)
        {
            return DataLayer.RoleDL.Get(id);
        }

        public Role Get(string name)
        {
            return DataLayer.RoleDL.Get(name);
        }
    }
}