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
    public class GlobalSettingsController : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetSettings()
        {
            string json = string.Empty;

            try
            {
                json = GlobalSettings.GetJson();
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex); 
            }

            return json;
        }
    }
}
