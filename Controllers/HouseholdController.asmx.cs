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
        public object GetByUserId()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<UserContext>(Context);
                var household = BusinessLayer.HouseholdBL.GetByUserId(obj.UserId);
                json = JsonParser.ToJson(household);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Get()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<HouseholdContext>(Context);
                var household = BusinessLayer.HouseholdBL.Get(obj.Id);
                json = JsonParser.ToJson(household);
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
                var obj = JsonParser.FromJson<Household>(Context);
                BusinessLayer.HouseholdBL.Update(obj);
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
        public object GetRelationships()
        {
            string json = string.Empty;

            try
            {
                var obj = BusinessLayer.RelationshipBL.GetRelationships();
                json = JsonParser.ToJson(obj);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        public class UserContext
        {
            public int UserId { get; set; }
        }

        public class HouseholdContext
        {
            public int Id { get; set; }
        }
    }
}