using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class RoleDL : DlBase
    {
        private List<Role> _roles = new List<Role>();

        private List<Role> Roles {
            get
            {
                if(_roles.Count > 0)
                {
                    return _roles;
                }

                string sql = string.Format(@"
                         SELECT *
                         FROM ROLES");

                this.ExecuteReader(sql, _roles, Populate);

                return _roles;
            }
        }

        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Role)obj;
            target.Id = Convert.ToInt32(reader["role_id"]);
            target.Name = reader["role_name"].ToString();
        }

        public Role Get(int id)
        {
            return Roles.First(x => x.Id == id);
        }

        public Role Get(string name)
        {
            return Roles.First(x => x.Name == name);
        }

        public List<Role> GetRoles()
        {
            return Roles;
        }
    }
}