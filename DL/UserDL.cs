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
            DataLayer.EntityDL.Populate(obj, reader);
            target.Id = Convert.ToInt32(reader["user_id"]);
            target.Username = reader["username"].ToString();
            target.Role = DataLayer.RoleDL.GetRole(Convert.ToInt32(reader["role_id"]));
        }

        public User Get(string userName, string password)
        {
            //TODO InjectionValidator(userName);
            //TODO InjectionValidator(password);

            var hashedPass = password.GetHashCode();

            var obj = new User();

            string sql = string.Format(@"
                         SELECT *
                         FROM USERS
                         WHERE USERNAME = '{0}' AND PASSWORD = '{1}'
                         ", userName, hashedPass);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(User user)
        {
            //TODO InjectionValidator(username);
            //TODO InjectionValidator(password);

            var id = GetNextVal(Sequences.User);
            var hashedPass = user.Password.GetHashCode();

            string sql = string.Format(@"
                         INSERT INTO USERS 
                         (USER_ID, USERNAME, PASSWORD, ROLE_ID, ENTITY_ID) 
                         VALUES 
                         ({0},'{1}','{2}')
                         ", id, user.Username, hashedPass, user.Role.Id, user.EntityId);

            ExecuteQuery(sql);

            if (!string.IsNullOrWhiteSpace(user.Name1))
            {
                DataLayer.EntityDL.Create((Entity)user);
            }
        }

        public void Update(User user)
        {
            //TODO InjectionValidator(email);
            //TODO InjectionValidator(password);
            //TODO InjectionValidator(firstName);
            //TODO InjectionValidator(lastName);

            var hashedPass = user.Password.GetHashCode();

            string sql = string.Format(@"
                         UPDATE USERS 
                         SET USER_ID = {0}, 
                             USERNAME = '{0}', 
                             PASSWORD = '{0}', 
                             ROLE_ID = {0}, 
                             ENTITY_ID = {0}
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