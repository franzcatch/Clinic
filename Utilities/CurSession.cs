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
                var user = HttpContext.Current.Session["LoggedInUser"];
                return user != null ? (BO.User)user : new BO.User();
            }
            set
            {
                HttpContext.Current.Session["LoggedInUser"] = value;
            }
        }
    }
}