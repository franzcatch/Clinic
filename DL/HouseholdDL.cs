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

        public List<Household> GetAll()
        {
            var obj = new List<Household>();

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD_PERSON 
                         ");

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public Household GetByUserId(int userId)
        {
            var obj = new Household();

            string sql = string.Format(@"
                         SELECT *
                         FROM USERS u
                         JOIN HOUSEHOLD_PERSON hp ON u.entity_id = hp.entity_id
                         JOIN HOUSEHOLD h ON hp.household_id = h.household_id
                         WHERE u.user_id = {0}
                         ", userId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Household> GetByPayerName(string firstName, string middleName, string lastName)
        {
            var obj = new List<Household>();

            var where = string.Empty;

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(middleName) && !string.IsNullOrEmpty(lastName))
            {
                where = string.Format(@"
                    UPPER(e.NAME1) LIKE UPPER('{0}%') AND
                    UPPER(e.NAME2) LIKE UPPER('{1}%') AND
                    UPPER(e.NAME3) LIKE UPPER('{2}%')
                    ", firstName, middleName, lastName);
            }
            else if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
            {
                where = string.Format(@"
                    UPPER(e.NAME1) LIKE UPPER('{0}%') AND
                    UPPER(e.NAME3) LIKE UPPER('{1}%')
                    ", firstName, lastName);
            }
            else if (!string.IsNullOrEmpty(lastName))
            {
                where = string.Format(@"
                    UPPER(e.NAME3) LIKE UPPER('{0}%')
                    ", lastName);
            }
            else
            {
                return obj;
            }

            string sql = string.Format(@"
                         SELECT *
                         FROM HOUSEHOLD h
                         JOIN HOUSEHOLD_PERSON hp ON h.household_id = hp.household_id
                         JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                         WHERE {0}"
                         , where);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(Household household)
        {
            //TODO InjectionValidator(household.);
            //TODO InjectionValidator(lastName);

            string sql;
            int id = GetNextVal(Sequences.Household);

            sql = string.Format(@"
                  INSERT INTO HOUSEHOLD
                  (HOUSEHOLD_ID, INSURANCE_NAME, POLICY_NUMBER, GROUP_NUMBER)
                  VALUES 
                  ({0},'{1}','{2}','{3}')
                  ",
                  id,
                  household.InsuranceName,
                  household.PolicyNumber,
                  household.GroupNumber);

            ExecuteQuery(sql);

            household.Id = id;
        }

        public void Update(Household household)
        {
            //TODO InjectionValidator(firstName);
            //TODO InjectionValidator(lastName);

            string sql = string.Format(@"
                         UPDATE HOUSEHOLD
                         SET INSURANCE_NAME = '{1}', 
                             POLICY_NUMBER = '{2}', 
                             GROUP_NUMBER = '{3}'
                         WHERE HOUSEHOLD_ID = {0}
                         ",
                         household.Id,
                         household.InsuranceName,
                         household.PolicyNumber,
                         household.GroupNumber);

            ExecuteQuery(sql);
        }
    }
}