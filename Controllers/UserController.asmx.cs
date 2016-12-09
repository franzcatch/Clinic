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
        public object Login()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<UserDataContext>(Context);
                var user = BusinessLayer.UserBL.Get(obj.username, obj.password);
                CurSession.User = user;
                json = JsonParser.ToJson(user);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Register()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<RegistrationContext>(Context);

                var user = new User() {
                    Username = obj.username,
                    Password = obj.password,
                    Role = obj.role
                };

                if (GlobalSettings.AdminExists)
                {
                    if (obj.householdId.HasValue)
                    {
                        BusinessLayer.UserBL.Create(obj.householdId.Value, user);
                    }
                    else
                    {
                        BusinessLayer.UserBL.Create(user);
                    }
                }
                else
                {
                    user.Role = BusinessLayer.RoleBL.Get(Constants.Admin);
                    BusinessLayer.UserBL.Create(user);
                }

                if (CurSession.User == null)
                {
                    CurSession.User = user;
                }

                GlobalSettings.CheckForAdmin();

                json = JsonParser.ToJson(user);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public void Logout()
        {
            CurSession.User = null;
        }

        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        [WebMethod(EnableSession = true)]
        public object Update()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<User>(Context);
                BusinessLayer.UserBL.Update(obj);
                if (CurSession.User.Id == obj.Id)
                {
                    CurSession.User = obj;
                }
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
        public object Remove()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<User>(Context);

                if (obj.Id == CurSession.User.Id)
                {
                    throw new Exception("You can not delete yourself"); 
                }

                BusinessLayer.UserBL.Update(obj);
                CurSession.User = obj;
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
        public object GetClients()
        {
            string json = string.Empty;

            try
            {
                var obj = BusinessLayer.UserBL.GetClients();
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
        public object GetStaff()
        {
            string json = string.Empty;

            try
            {
                var obj = BusinessLayer.UserBL.GetStaff();
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
        public object GetRoles()
        {
            string json = string.Empty;

            try
            {
                var roles = BusinessLayer.RoleBL.GetRoles();
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
        public object GetEligibleQualifications()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<UserContext>(Context);
                var roles = BusinessLayer.ServiceBL.GetEligibleProviderServicesForUserId(obj.UserId.Value);
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
        public object GetQualifications()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<UserContext>(Context);
                var roles = BusinessLayer.ServiceBL.GetProviderServicesForUserId(obj.UserId.Value);
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
        public object UpdateQualifications()
        {
            string json = string.Empty;

            try
            {
                var obj = JsonParser.FromJson<UserQualificationsContext>(Context);
                BusinessLayer.ServiceBL.UpdateProviderServicesForUserId(obj.UserId, obj.Services);
            }
            catch (Exception ex)
            {
                json = JsonParser.ExceptionToJson(ex);
            }

            return json;
        }

        public class UserContext
        {
            public int? UserId { get; set; }
        }

        public class RegistrationContext : UserDataContext
        {
            public int? householdId { get; set; }
            public Role role { get; set; }
        }

        public class UserDataContext
        {
            public string username { get; set; }
            public string password { get; set; }
        }

        public class UserQualificationsContext
        {
            public int UserId { get; set; }
            public List<Service> Services { get; set; }
        }
    }
}
