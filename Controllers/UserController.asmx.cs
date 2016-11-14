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
            var obj = JsonParser.GetParams<Data>(Context);
            var user = BusinessLayer.UserBL.Get(obj.Username, obj.Password);
            CurSession.User = user;
            return user;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public User Create()
        {
            var obj = JsonParser.GetParams<Data>(Context);

            var user = new BO.User() {
                Username = obj.Username,
                Password = obj.Password
            };

            BusinessLayer.UserBL.Create(user);

            CurSession.User = user;

            return user;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void Logout()
        {
            CurSession.User = null;
        }
    }

    public class Data
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
