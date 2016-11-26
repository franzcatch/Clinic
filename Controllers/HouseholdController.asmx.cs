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
    public class HouseholdController : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public Household GetByUserId()
        {
            var obj = JsonParser.GetParams<UserContext>(Context);
            var household = BusinessLayer.HouseholdBL.GetByUserId(obj.UserId);
            return household;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public Household Update()
        {
            var obj = JsonParser.GetParams<Household>(Context);
            BusinessLayer.HouseholdBL.Update(obj);
            return obj;
        }

        public class UserContext
        {
            public int UserId { get; set; }
        }
    }
}