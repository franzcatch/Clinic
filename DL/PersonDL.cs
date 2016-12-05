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
            
            int entityId;
            if (Int32.TryParse(reader["entity_id"].ToString(), out entityId))
            {
                DataLayer.EntityDL.Get(entityId).CopyTo(target);
            }

            target.Id = Convert.ToInt32(reader["household_person_id"]);
            target.Appointments = new List<Appointment>();
            target.IsPayer = reader["is_payer"].ToString() == "Y" ? true : false;
            target.DateOfBirth = DateTime.Parse(reader["dob"].ToString());
            var relationshipId = Convert.ToInt32(reader["relationship_id"]);
            target.Relationship = DataLayer.RelationshipDL.GetRelationships().First(x => x.Id == relationshipId);
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

        public List<Person> GetByHouseholdId(int householdId)
        {
            var obj = new List<Person>();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON 
                         WHERE HOUSEHOLD_ID = {0}
                         ", householdId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public Person GetPayerByHouseholdId(int householdId)
        {
            var obj = new Person();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON 
                         WHERE HOUSEHOLD_ID = {0}
                           AND IS_PAYER = 'Y'
                         ", householdId);

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
        
        public void Create(int householdId, Person person)
        {
            //TODO InjectionValidator(person.);
            //TODO InjectionValidator(lastName);

            string sql;
            int id = GetNextVal(Sequences.Person);

            sql = string.Format(@"
                  INSERT INTO HOUSEHOLD_PERSON
                  (HOUSEHOLD_PERSON_ID, RELATIONSHIP_ID, HOUSEHOLD_ID, ENTITY_ID, IS_PAYER, DOB)
                  VALUES 
                  ({0},{1},{2},{3},'{4}',TO_DATE('{5}'))
                  ", 
                  id, 
                  person.Relationship.Id,
                  householdId,
                  person.EntityId,
                  person.IsPayer ? 'Y' : 'N',
                  person.DateOfBirthString);
            
            ExecuteQuery(sql);

            person.Id = id;
        }

        public void Update(int householdId, Person person)
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
                         householdId,
                         person.EntityId,
                         person.IsPayer ? 'Y' : 'N',
                         person.DateOfBirthString);

            ExecuteQuery(sql);
        }
    }
}