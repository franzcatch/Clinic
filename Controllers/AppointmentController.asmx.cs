using Clinic.BL;
using Clinic.BO;
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
        public object GetAppointmentsForClinic()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<IdContext>(Context);
                var result = BusinessLayer.AppointmentBL.GetAppointmentsForClinic(obj.Id, obj.ServiceDate);
                json = JsonParser.ToJson(result);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAppointmentsForUser()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<IdContext>(Context);
                var result = BusinessLayer.AppointmentBL.GetAppointmentsForUser(obj.Id, obj.ServiceDate);
                json = JsonParser.ToJson(result);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAvailableAppointments()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<GetAvailabilityContext>(Context);
                return BusinessLayer.AppointmentBL.GetAvailableAppointments(obj.ClinicId, obj.ServiceId, obj.ServiceDate);
                //json = JsonParser.ToJson(result);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Create()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<Appointment>(Context);
                BusinessLayer.AppointmentBL.Create(obj);
                json = JsonParser.ToJson(obj);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Delete()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<Appointment>(Context);
                BusinessLayer.AppointmentBL.Delete(obj);
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

        public class IdContext
        {
            public int Id { get; set; }
            public DateTime? ServiceDate { get; set; }
        }
    }
}
