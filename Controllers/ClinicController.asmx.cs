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
    public class ClinicController : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAll()
        {
            string json = string.Empty;

            try
            {
                var clinics = BusinessLayer.ClinicBL.GetClinics();
                json = JsonParser.ToJson(clinics);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetEligibleProviders()
        {
            string json = string.Empty;

            try
            {
                var clinicId = JsonParser.FromJson<IdContext>(Context).Id;
                var users = BusinessLayer.ClinicBL.GetEligibleProviders(clinicId.HasValue ? clinicId.Value : -1);
                json = JsonParser.ToJson(users);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAllServices()
        {
            string json = string.Empty;

            try
            {
                var services = BusinessLayer.ServiceBL.Get();
                json = JsonParser.ToJson(services);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetRooms()
        {
            string json = string.Empty;

            try
            {
                var clinicId = JsonParser.FromJson<IdContext>(Context).Id;
                var roles = BusinessLayer.RoomBL.GetRoomsByClinicId(clinicId.Value);
                json = JsonParser.ToJson(roles);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Update()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<Clinic.BO.Clinic>(Context);
                BusinessLayer.ClinicBL.Update(obj);
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
                var obj = JsonParser.FromJson<IdContext>(Context);
                BusinessLayer.ClinicBL.Delete(obj.Id.Value);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }
    }

    public class IdContext
    {
        public int? Id { get; set; }
    }
}