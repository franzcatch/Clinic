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
    public class ReportController : System.Web.Services.WebService
    {
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GenerateAllHouseholdReport()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GenerateAllHouseholdReport();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetHouseholdAndInsurance()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetHouseholdAndInsurance();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAllPatientsAndInsurance()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetAllPatientsAndInsurance();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAllBilling()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetAllBilling();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetHouseholdTotalCosts()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetHouseholdTotalCosts();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetProvidersAndServices()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetProvidersAndServices();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetServicesAndProviders()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetServicesAndProviders();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetFutureAppointmentsByPatient()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetFutureAppointmentsByPatient();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetAllServicesProvided()
        {
            string json = string.Empty;

            try
            {
                var response = BusinessLayer.ReportBL.GetAllServicesProvided();
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object GetTotalServicesForProviders()
        {
            string json = string.Empty;

            try
            {
                var serviceDate = JsonParser.FromJson<DateContext>(Context).Date;
                var response = BusinessLayer.ReportBL.GetTotalServicesForProviders(serviceDate);
                json = JsonParser.ToJson(response);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        public class DateContext
        {
            public DateTime Date { get; set; }
        }
    }
}
