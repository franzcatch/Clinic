using Clinic.BO;
using Clinic.DL;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class UserDL : DlBase
    {
        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (User)obj;
            if (!string.IsNullOrEmpty(reader["entity_id"].ToString()))
            {
                target = (User)DataLayer.EntityDL.Get(Convert.ToInt32(reader["entity_id"].ToString()));
            }
            
            target.Id = Convert.ToInt32(reader["user_id"]);
            target.Username = reader["username"].ToString();
            target.Role = DataLayer.RoleDL.Get(Convert.ToInt32(reader["role_id"]));

        }

        public List<User> GetAdmins()
        {
            var obj = new List<User>();

            string sql = string.Format(@"
                         SELECT *
                         FROM USERS
                         WHERE ROLE_ID = 1 -- Admin
                         ");

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public User Get(string userName, string password)
        {
            var hashedPass = password.GetHashCode();

            var obj = new User();

            string sql = string.Format(@"
                         SELECT *
                         FROM USERS
                         WHERE USERNAME = '{0}' AND PASSWORD = '{1}'
                         ", userName, hashedPass.ToString());

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(User user)
        {
            var id = GetNextVal(Sequences.User);
            var hashedPass = user.Password.GetHashCode();

            string sql = string.Empty;

            if (user.EntityId.HasValue)
            {
                sql = string.Format(@"
                        INSERT INTO USERS 
                        (USER_ID, USERNAME, PASSWORD, ROLE_ID, ENTITY_ID) 
                        VALUES 
                        ({0},'{1}','{2}',{3},{4})
                        ", id, user.Username, hashedPass, user.Role.Id, user.EntityId);
            }
            else
            {
                sql = string.Format(@"
                        INSERT INTO USERS 
                        (USER_ID, USERNAME, PASSWORD, ROLE_ID) 
                        VALUES 
                        ({0},'{1}','{2}',{3})
                        ", id, user.Username, hashedPass.ToString(), user.Role.Id);
            }

            ExecuteQuery(sql);
        }

        public void Update(User user)
        {
            var hashedPass = user.Password.GetHashCode();

            string sql = string.Format(@"
                         UPDATE USERS 
                         SET USERNAME = '{1}', 
                             PASSWORD = '{2}', 
                             ROLE_ID = {3}, 
                             ENTITY_ID = {4}
                         WHERE USER_ID = {0}
                         ", 
                         user.Id, 
                         user.Username,
                         hashedPass,
                         user.Role.Id,
                         user.EntityId);

            ExecuteQuery(sql);
        }
    }
}