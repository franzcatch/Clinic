using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class EntityDL : DlBase
    {
        public void Populate(Object obj, OracleDataReader reader)
        {
            obj = new Entity()
            {
                EntityId = Convert.ToInt32(reader["entity_id"]),
                Name1 = reader["name1"].ToString(),
                Name2 = reader["name2"].ToString(),
                Name3 = reader["name3"].ToString(),
                Address1 = reader["address1"].ToString(),
                Address2 = reader["address2"].ToString(),
                City = reader["city"].ToString(),
                State = reader["state"].ToString(),
                Zip = reader["zip"].ToString(),
                Phone1 = reader["phone1"].ToString(),
                Phone2 = reader["phone2"].ToString(),
                Phone3 = reader["phone3"].ToString()
            };
        }

        public Entity Get(int id)
        {
            var obj = new User();

            string sql = string.Format(@"
                         SELECT * FROM ENTITY 
                         WHERE ENTITY_ID = {0}
                         ", id);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(Entity entity)
        {
            int id = GetNextVal(Sequences.Entity);

            string sql = string.Format(@"
                         INSERT INTO ENTITY
                         (ENTITY_ID, NAME1, NAME2, NAME3, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, PHONE1, PHONE2, PHONE3)
                         VALUES 
                         ({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')
                         ", 
                         id, 
                         entity.Name1, 
                         entity.Name2,
                         entity.Name3,
                         entity.Address1,
                         entity.Address2,
                         entity.City,
                         entity.State,
                         entity.Zip,
                         entity.Phone1,
                         entity.Phone2,
                         entity.Phone3);

            ExecuteQuery(sql);

            entity.Id = id;
        }

        public void Update(Entity entity)
        {
            string sql = string.Format(@"
                         UPDATE ENTITY
                         SET NAME1 = {1}, 
                             NAME2 = {2}, 
                             NAME3 = {3}, 
                             ADDRESS1 = {4}, 
                             ADDRESS2 = {5}, 
                             CITY = {6}, 
                             STATE = {7}, 
                             ZIP = {8}, 
                             PHONE1 = {9}, 
                             PHONE2 = {10}, 
                             PHONE3 = {11}
                         ({0},{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')
                         ",
                         entity.Id,
                         entity.Name1,
                         entity.Name2,
                         entity.Name3,
                         entity.Address1,
                         entity.Address2,
                         entity.City,
                         entity.State,
                         entity.Zip,
                         entity.Phone1,
                         entity.Phone2,
                         entity.Phone3);

            ExecuteQuery(sql);
        }
    }
}