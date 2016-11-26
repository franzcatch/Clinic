using Clinic.BL;
using Clinic.BO;
using Clinic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace Clinic.Controllers
{
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class UserController1 : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public User Login()
        {
            var obj = JsonParser.GetParams<UserDataContext>(Context);
            var user = BusinessLayer.UserBL.Get(obj.username, obj.password);
            CurSession.User = user;
            return user;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public User Register()
        {
            var obj = JsonParser.GetParams<RegistrationContext>(Context);

            var user = new BO.User() {
                Username = obj.username,
                Password = obj.password
            };

            if (GlobalSettings.AdminExists)
            {
                BusinessLayer.UserBL.Create(obj.householdId, user);
            }
            else
            {
                user.Role = BusinessLayer.RoleBL.Get(Constants.Admin);
                BusinessLayer.UserBL.Create(user);
            }

            if (user != null)
            {
                CurSession.User = user;
            }

            GlobalSettings.CheckForAdmin();

            return user;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void Logout()
        {
            CurSession.User = null;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public User Update()
        {
            var obj = JsonParser.GetParams<User>(Context);
            BusinessLayer.UserBL.Update(obj);
            CurSession.User = obj;
            return obj;
        }

        public class RegistrationContext : UserDataContext
        {
            public int householdId { get; set; }
        }

        public class UserDataContext
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}
