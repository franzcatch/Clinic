using Clinic.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    public static class CurSession
    {
        public static User User
        {
            get
            {
                var user = HttpContext.Current.Session["User"];
                return user != null ? (BO.User)user : null;
            }
            set
            {
                HttpContext.Current.Session["User"] = value;
            }
        }
    }
}