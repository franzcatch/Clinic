using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class HouseholdDL : DlBase
    {
        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Household)obj;
            target.Id = Convert.ToInt32(reader["household_id"]);
            target.GroupNumber = reader["group_number"].ToString();
            target.InsuranceName = reader["insurance_name"].ToString();
            target.PolicyNumber = reader["policy_number"].ToString();
            target.People = DataLayer.PersonDL.GetByHouseholdId(target.Id.Value);
        }

        public Household Get(int id)
        {
            var obj = new Household();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON 
                         WHERE HOUSEHOLD_PERSON_ID = {0}
                         ", id);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public Household GetByUserId(int userId)
        {
            var obj = new Household();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON
                         ", userId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(Household household)
        {
            //TODO InjectionValidator(household.);
            //TODO InjectionValidator(lastName);

            //string sql;
            //int id = GetNextVal(Sequences.Household);

            //sql = string.Format(@"
            //      INSERT INTO HOUSEHOLD_PERSON
            //      (HOUSEHOLD_PERSON_ID, RELATIONSHIP_ID, HOUSEHOLD_ID, ENTITY_ID, IS_PAYER, DOB)
            //      VALUES 
            //      ({0},{1},{2},{3},'{4}','{5}')
            //      ",
            //      id,
            //      household.Relationship.Id,
            //      household.HouseholdId,
            //      household.EntityId,
            //      household.IsPayer,
            //      household.DateOfBirth);

            //ExecuteQuery(sql);

            //household.Id = id;
        }

        public void Update(Household household)
        {
            //TODO InjectionValidator(firstName);
            //TODO InjectionValidator(lastName);

            //string sql = string.Format(@"
            //             UPDATE HOUSEHOLD_PERSON
            //             SET RELATIONSHIP_ID = {1}, 
            //                 HOUSEHOLD_ID = {2}, 
            //                 ENTITY_ID = {3}, 
            //                 IS_PAYER = '{4}', 
            //                 DOB = '{5}'
            //             WHERE HOUSEHOLD_PERSON_ID = {0}
            //             ",
            //             household.Id,
            //             household.Relationship.Id,
            //             household.HouseholdId,
            //             household.EntityId,
            //             household.IsPayer,
            //             household.DateOfBirth);

            //ExecuteQuery(sql);
        }
    }
}