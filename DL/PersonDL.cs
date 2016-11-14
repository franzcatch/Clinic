using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class PersonDL : DlBase
    {
        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Person)obj;
            target.Id = Convert.ToInt32(reader["person_id"]);
            DataLayer.EntityDL.Populate(obj, reader);
            target.Appointments = new List<Appointment>();
            target.IsPayer = true;
            target.Relationship = new Relationship();
        }

        public Person Get(int id)
        {
            var obj = new Person();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON 
                         WHERE HOUSEHOLD_PERSON_ID = {0}
                         ", id);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public Person GetByUserId(int userId)
        {
            var obj = new Person();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON
                         ", userId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }
        
        public void Create(Person person)
        {
            //TODO InjectionValidator(person.);
            //TODO InjectionValidator(lastName);

            string sql;
            int id = GetNextVal(Sequences.Person);

            sql = string.Format(@"
                  INSERT INTO HOUSEHOLD_PERSON
                  (HOUSEHOLD_PERSON_ID, RELATIONSHIP_ID, HOUSEHOLD_ID, ENTITY_ID, IS_PAYER, DOB)
                  VALUES 
                  ({0},{1},{2},{3},'{4}','{5}')
                  ", 
                  id, 
                  person.Relationship.Id,
                  person.HouseholdId,
                  person.EntityId,
                  person.IsPayer,
                  person.DateOfBirth);
            
            ExecuteQuery(sql);

            person.Id = id;
        }

        public void Update(Person person)
        {
            //TODO InjectionValidator(firstName);
            //TODO InjectionValidator(lastName);

            string sql = string.Format(@"
                         UPDATE HOUSEHOLD_PERSON
                         SET RELATIONSHIP_ID = {1}, 
                             HOUSEHOLD_ID = {2}, 
                             ENTITY_ID = {3}, 
                             IS_PAYER = '{4}', 
                             DOB = '{5}'
                         WHERE HOUSEHOLD_PERSON_ID = {0}
                         ",
                         person.Id,
                         person.Relationship.Id,
                         person.HouseholdId,
                         person.EntityId,
                         person.IsPayer,
                         person.DateOfBirth);

            ExecuteQuery(sql);
        }
    }
}