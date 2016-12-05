using Clinic.BL;
using Clinic.BO;
using Clinic.DL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.Utilities
{
    [JsonObject(MemberSerialization.OptIn)]
    public static class GlobalSettings
    {
        private static Nullable<bool> _adminExists;
        
        [JsonProperty]
        public static bool AdminExists
        {
            get
            {
                if (!_adminExists.HasValue)
                {
                    CheckForAdmin();
                }

                return _adminExists.Value;
            }
        }

        [JsonProperty]
        public static User User
        {
            get
            {
                return CurSession.User;
            }
        }

        public static void CheckForAdmin() {
            var admins = BusinessLayer.UserBL.GetAdmins();
            _adminExists = admins.Count > 0;
        }

        public static string GetJson() {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.AdminExists = AdminExists;
            obj.User = User;

            return JsonConvert.SerializeObject(obj);
        }
    }
}