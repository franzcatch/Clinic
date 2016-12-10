using Clinic.BL;
using Clinic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Clinic.Controllers
{
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AppointmentController : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAvailableTimesAndProvidersForService()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<GetAvailabilityContext>(Context);
                var result = BusinessLayer.AppointmentBL.GetAvailableTimesAndProvidersForService(obj.ClinicId, obj.ServiceId, obj.ServiceDate);
                json = JsonParser.ToJson(result);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        public class GetAvailabilityContext {
            public int ClinicId { get; set; }
            public int ServiceId { get; set; }
            public DateTime ServiceDate { get; set; }
        }
    }
}
