using Clinic.BO;
using Clinic.DL;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class ReportDL : DlBase
    {
        /// <summary>
        /// List all households with the household ID, name, address, and home phone along with the
        /// patient ID, name and relationship for each patient.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GenerateAllHouseholdReport()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  h.HOUSEHOLD_ID as ACCOUNT_NUMBER,
                  r.NAME as RELATIONSHIP,
                  hp.HOUSEHOLD_PERSON_ID as PATIENT_ID,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as NAME,
                  e.ADDRESS1,
                  e.ADDRESS2,
                  e.CITY,
                  e.STATE,
                  e.ZIP,
                  e.PHONE1,
                  e.PHONE2,
                  e.PHONE3
                FROM HOUSEHOLD h
                JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                JOIN RELATIONSHIP r ON hp.RELATIONSHIP_ID = r.RELATIONSHIP_ID
                ORDER BY h.HOUSEHOLD_ID            
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Account_Number = reader["account_number"].ToString();
                newObj.Relationship = reader["relationship"].ToString();
                newObj.Patient_ID = reader["patient_id"].ToString();
                newObj.Name = reader["name"].ToString();
                newObj.Address = reader["address1"].ToString();
                newObj.City = reader["city"].ToString();
                newObj.State = reader["state"].ToString();
                newObj.Zip = reader["zip"].ToString();
                newObj.Phone = reader["phone1"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List the insurance coverage for all households by household ID, household name, insurance
        /// company ID and company name.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetHouseholdAndInsurance()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT
                    h.HOUSEHOLD_ID as ACCOUNT_NUMBER,
                    e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as NAME,
                    h.INSURANCE_NAME
                FROM HOUSEHOLD h
                JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                WHERE hp.IS_PAYER = 'Y'
                ORDER BY e.NAME1, e.NAME2, e.NAME3                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Account_Number = reader["account_number"].ToString();
                newObj.Name = reader["name"].ToString();
                newObj.Insurance_Name = reader["insurance_name"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List all patients in alphabetical order by patient ID, name, and date of birth along with the
        /// name of the insurance company and policy number.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllPatientsAndInsurance()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  hp.HOUSEHOLD_PERSON_ID as PATIENT_ID,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as NAME,
                  hp.DOB,
                  h.INSURANCE_NAME,
                  h.POLICY_NUMBER,
                  h.GROUP_NUMBER
                FROM HOUSEHOLD h
                JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                JOIN RELATIONSHIP r ON hp.RELATIONSHIP_ID = r.RELATIONSHIP_ID
                ORDER BY h.HOUSEHOLD_ID                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Patient_ID = reader["patient_id"].ToString();
                newObj.Name = reader["name"].ToString();
                newObj.Date_Of_Birth = reader["dob"].ToString();
                newObj.Insurance_Name = reader["insurance_name"].ToString();
                newObj.Policy_Number = reader["policy_number"].ToString();
                newObj.Group_Number = reader["group_number"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// Show itemized billings for all households with the household ID, household name, patient ID,
        /// patient name, service received, and the cost of the service.Show the output in alphabetical
        /// order by household name, patient name and billing date.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllBilling()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  h.HOUSEHOLD_ID as ACCOUNT_NUMBER,
                  payer.NAME as HOUSEHOLD_NAME,
                  hp.HOUSEHOLD_PERSON_ID as PATIENT_ID,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as PATIENT_NAME,
                  s.NAME as SERVICE,
                  asp.COST,
                  asp.TIME as SERVICE_DATE
                FROM HOUSEHOLD h
                JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                JOIN (
                  SELECT
                    h.HOUSEHOLD_ID,
                    e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as NAME
                  FROM HOUSEHOLD h
                  JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                  JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                  WHERE hp.IS_PAYER = 'Y'
                ) payer ON h.HOUSEHOLD_ID = payer.HOUSEHOLD_ID
                JOIN APPOINTMENT a ON hp.HOUSEHOLD_PERSON_ID = a.HOUSEHOLD_PERSON_ID
                JOIN APPOINTMENT_SERVICE asp ON a.APPOINTMENT_ID = asp.APPOINTMENT_ID
                JOIN SERVICE s ON asp.SERVICE_ID = s.SERVICE_ID
                ORDER BY payer.NAME, e.NAME1, e.NAME2, e.NAME3, asp.TIME                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Account_Number = reader["account_number"].ToString();
                newObj.Household_Name = reader["household_name"].ToString();
                newObj.Patient_ID = reader["patient_id"].ToString();
                newObj.Patient_Name = reader["patient_name"].ToString();
                newObj.Service = reader["service"].ToString();
                newObj.Cost = "$" + reader["cost"].ToString();
                newObj.Start_Time = reader["service_date"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List the total cost of all services received for each household.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetHouseholdTotalCosts()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  payer.NAME as HOUSEHOLD_NAME,
                  SUM(asp.COST) as TOTAL
                FROM HOUSEHOLD h
                JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                JOIN APPOINTMENT a ON hp.HOUSEHOLD_PERSON_ID = a.HOUSEHOLD_PERSON_ID
                JOIN APPOINTMENT_SERVICE asp ON a.APPOINTMENT_ID = asp.APPOINTMENT_ID
                JOIN (
                  SELECT 
                    hp.HOUSEHOLD_ID, 
                    SUM(aps.COST) as TOTAL
                  FROM HOUSEHOLD_PERSON hp
                  JOIN APPOINTMENT a ON hp.HOUSEHOLD_PERSON_ID = a.HOUSEHOLD_PERSON_ID
                  JOIN APPOINTMENT_SERVICE aps ON a.APPOINTMENT_ID = aps.APPOINTMENT_ID
                  GROUP BY hp.HOUSEHOLD_ID
                ) totals ON h.HOUSEHOLD_ID = totals.HOUSEHOLD_ID
                JOIN (
                  SELECT
                    h.HOUSEHOLD_ID,
                    e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as NAME
                  FROM HOUSEHOLD h
                  JOIN HOUSEHOLD_PERSON hp ON h.HOUSEHOLD_ID = hp.HOUSEHOLD_ID
                  JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                  WHERE hp.IS_PAYER = 'Y'
                ) payer ON h.HOUSEHOLD_ID = payer.HOUSEHOLD_ID
                GROUP BY payer.NAME
                ORDER BY payer.NAME                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Household_Name = reader["household_name"].ToString();
                newObj.Total = "$" + reader["total"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List each provider with all services he or she is qualified to render.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetProvidersAndServices()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as PROVIDER,
                  s.NAME as QUALIFIED_SERVICE
                FROM PROVIDER p
                JOIN PROVIDER_QUALIFICATION pq ON p.PROVIDER_ID = pq.PROVIDER_ID
                JOIN SERVICE s ON pq.SERVICE_ID = s.SERVICE_ID
                JOIN ENTITY e ON p.ENTITY_ID = e.ENTITY_ID
                ORDER BY e.NAME1, e.NAME2, e.NAME3, s.NAME                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Provider = reader["provider"].ToString();
                newObj.Qualified_Service = reader["qualified_service"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List each service available with all providers who are qualified to offer this service.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetServicesAndProviders()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  s.NAME as SERVICE,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as QUALIFIED_PROVIDER
                FROM SERVICE s
                JOIN PROVIDER_QUALIFICATION pq ON s.SERVICE_ID = pq.SERVICE_ID
                JOIN PROVIDER p ON pq.PROVIDER_ID = p.PROVIDER_ID
                JOIN ENTITY e ON p.ENTITY_ID = e.ENTITY_ID
                ORDER BY s.NAME, e.NAME1, e.NAME2, e.NAME3                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Service = reader["service"].ToString();
                newObj.Qualified_Provider = reader["qualified_provider"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// List all future appointments by name of patient, appointment date and time, estimated length
        /// of service, and contact home phone number.Dates and times should be in calendar order
        /// for each patient.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetFutureAppointmentsByPatient()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as PATIENT,
                  asp.TIME as APPOINTMENT_DATE,
                  s.MINUTES,
                  e.PHONE1 as PHONE
                FROM APPOINTMENT_SERVICE asp
                JOIN APPOINTMENT a ON asp.APPOINTMENT_ID = a.APPOINTMENT_ID
                JOIN HOUSEHOLD_PERSON hp ON a.HOUSEHOLD_PERSON_ID = hp.HOUSEHOLD_PERSON_ID
                JOIN ENTITY e ON hp.ENTITY_ID = e.ENTITY_ID
                JOIN SERVICE s ON asp.SERVICE_ID = s.SERVICE_ID
                WHERE asp.TIME > SYSDATE
                ORDER BY e.NAME1, e.NAME2, e.NAME3, asp.TIME                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Patient = reader["patient"].ToString();
                newObj.Appointment_Date = reader["appointment_date"].ToString();
                newObj.Minutes = reader["minutes"].ToString();
                newObj.Phone = reader["phone"].ToString();
                list.Add(newObj);
            }

            CloseConnection(cmd);
            return list;
        }

        /// <summary>
        /// For a given date, list all services provided by each provider in alphabetical order by name of
        /// the provider.Show the service ID, service description and cost of service.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllServicesProvided()
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  asp.TIME as SERVICE_DATE,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as PROVIDER,
                  s.SERVICE_ID,
                  s.NAME as SERVICE_NAME,
                  s.COST
                FROM PROVIDER p
                JOIN APPOINTMENT_SERVICE asp ON p.PROVIDER_ID = asp.PROVIDER_ID
                JOIN SERVICE s ON asp.SERVICE_ID = s.SERVICE_ID
                JOIN ENTITY e ON p.ENTITY_ID = e.ENTITY_ID
                ORDER BY asp.TIME, e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Service_Date = reader["service_date"].ToString();
                newObj.Provider = reader["provider"].ToString();
                newObj.Service_ID = reader["service_id"].ToString();
                newObj.Service_Name = reader["service_name"].ToString();
                newObj.Cost = "$" + reader["Cost"].ToString();
                list.Add(newObj);
            }
            CloseConnection(cmd);

            return list;
        }

        /// <summary>
        /// For a given date, list the total amount of services each provider rendered.Show in
        /// alphabetical order by the provider’s name.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetTotalServicesForProviders(DateTime serviceDate)
        {
            var list = new List<System.Dynamic.ExpandoObject>();

            string sql = string.Format(@"
                SELECT 
                  asp.TIME as SERVICE_DATE,
                  e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3 as PROVIDER,
                  SUM(s.COST) as TOTAL
                FROM PROVIDER p
                JOIN APPOINTMENT_SERVICE asp ON p.PROVIDER_ID = asp.PROVIDER_ID
                JOIN SERVICE s ON asp.SERVICE_ID = s.SERVICE_ID
                JOIN ENTITY e ON p.ENTITY_ID = e.ENTITY_ID
                GROUP BY asp.TIME, e.NAME1 || ' ' || e.NAME2 || ' ' || e.NAME3
                ORDER BY SERVICE_DATE, PROVIDER                         
                         ");

            var cmd = GetCommand(sql);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                dynamic newObj = new ExpandoObject();
                newObj.Service_Date = reader["service_date"].ToString();
                newObj.Provider = reader["provider"].ToString();
                newObj.Total = "$" + reader["total"].ToString();
                list.Add(newObj);
            }

            CloseConnection(cmd);
            return list;
        }
    }
}