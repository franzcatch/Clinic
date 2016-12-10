using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class ServiceDL : DlBase
    {
        public void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Service)obj;

            target.Id = Int32.Parse(reader["service_id"].ToString());
            target.Name = reader["name"].ToString();
            target.Cost = Decimal.Parse(reader["cost"].ToString());
            target.Minutes = Int32.Parse(reader["minutes"].ToString());
        }

        public Service Get(string name)
        {
            var obj = new Service();

            string sql = string.Format(@"
                         SELECT * 
                         FROM SERVICE
                         WHERE NAME = '{0}'
                         ", name);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public Service Get(int id)
        {
            var obj = new Service();

            string sql = string.Format(@"
                         SELECT * 
                         FROM SERVICE
                         WHERE SERVICE_ID = {0}
                         ", id);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Service> Get()
        {
            var obj = new List<Service>();

            string sql = string.Format(@"
                         SELECT * 
                         FROM SERVICE
                         ");

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Service> GetServicesByClinicId(int clinicId)
        {
            var obj = new List<Service>();

            string sql = string.Format(@"
                         SELECT s.* 
                         FROM SERVICE s
                         JOIN SERVICE_CLINIC sc ON s.SERVICE_ID = sc.SERVICE_ID
                         WHERE sc.CLINIC_ID = {0}
                         ", clinicId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Service> GetServicesByProviderId(int providerId)
        {
            var obj = new List<Service>();

            string sql = string.Format(@"
                         SELECT s.* 
                         FROM SERVICE s
                         JOIN PROVIDER_QUALIFICATION pq ON s.SERVICE_ID = pq.SERVICE_ID
                         WHERE pq.PROVIDER_ID = {0}
                         ", providerId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Service> GetEligibleProviderServicesForUserId(int userId)
        {
            var services = new List<Service>();
            var clinics = new List<Clinic.BO.Clinic>();

            string sql = string.Format(@"
                         SELECT c.*
                         FROM USERS u
                         JOIN ENTITY e ON u.ENTITY_ID = e.ENTITY_ID
                         JOIN PROVIDER p ON e.ENTITY_ID = p.ENTITY_ID
                         JOIN CLINIC c ON p.CLINIC_ID = p.CLINIC_ID
                         WHERE u.USER_ID = {0}
                         ", userId);

            ExecuteReader(sql, clinics, DataLayer.ClinicDL.Populate);

            foreach(var clinic in clinics)
            {
                foreach(var service in clinic.Services)
                {
                    if (!services.Any(x => x.Id == service.Id))
                    {
                        services.Add(service);
                    }
                }
            }

            return services;
        }

        public List<Service> GetProviderServicesForUserId(int userId)
        {
            var obj = new List<Service>();

            string sql = string.Format(@"
                         SELECT s.*
                         FROM SERVICE s
                         JOIN PROVIDER_QUALIFICATION pq ON s.SERVICE_ID = pq.SERVICE_ID
                         JOIN PROVIDER p ON pq.PROVIDER_ID = p.PROVIDER_ID
                         JOIN ENTITY e ON p.ENTITY_ID = e.ENTITY_ID
                         JOIN USERS u ON e.ENTITY_ID = u.ENTITY_ID
                         WHERE u.USER_ID = {0}
                         ", userId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(Service service)
        {
            string sql;
            int id = GetNextVal(Sequences.Service);

            sql = string.Format(@"
                  INSERT INTO SERVICE
                  (SERVICE_ID, NAME, COST, MINUTES)
                  VALUES 
                  ({0},'{1}',{2},{3})
                  ",
                  id,
                  service.Name,
                  service.Cost,
                  service.Minutes);

            ExecuteQuery(sql);

            service.Id = id;
        }

        public void AddToProvider(Service service, Provider provider)
        {
            string sql;
            int id = GetNextVal(Sequences.ProviderQualification);

            sql = string.Format(@"
                  INSERT INTO PROVIDER_QUALIFICATION
                  (PROVIDER_QUALIFICATION_ID, PROVIDER_ID, SERVICE_ID)
                  VALUES 
                  ({0},{1},{2})
                  ",
                  id,
                  provider.Id,
                  service.Id);

            ExecuteQuery(sql);
        }

        public void AddToClinic(Service service, Clinic.BO.Clinic clinic)
        {
            var existingService = Get(service.Name);

            if (existingService.Id == null)
            {
                Create(service);
            } else
            {
                service = existingService;
            }

            string sql;
            int id = GetNextVal(Sequences.ServiceClinic);

            sql = string.Format(@"
                  INSERT INTO SERVICE_CLINIC
                  (SERVICE_CLINIC_ID, CLINIC_ID, SERVICE_ID)
                  VALUES 
                  ({0},{1},{2})
                  ",
                  id,
                  clinic.Id,
                  service.Id);

            ExecuteQuery(sql);
        }

        public void DeleteFromProvider(Service service, Provider provider)
        {
            string sql = string.Format(@"
                              DELETE FROM PROVIDER_QUALIFICATION
                              WHERE SERVICE_ID = {0} 
                                AND PROVIDER_ID = {1}
                              ",
                              service.Id,
                              provider.Id);

            ExecuteQuery(sql);
        }

        public void DeleteFromClinic(Service service, Clinic.BO.Clinic clinic)
        {
            string sql = string.Format(@"
                              DELETE FROM SERVICE_CLINIC
                              WHERE SERVICE_ID = {0} 
                                AND CLINIC_ID = {1}
                              ",
                              service.Id,
                              clinic.Id);

            ExecuteQuery(sql);
        }

        /// <summary>
        /// Deletes the service for all clinics and providers
        /// </summary>
        /// <param name="service"></param>
        public void Delete(Service service)
        {
            var clinics = GetClinicsUsingService(service.Name);
            var providers = GetProvidersUsingService(service.Name);

            clinics.ForEach(x => DeleteFromClinic(service, x));
            providers.ForEach(x => DeleteFromProvider(service, x));

            string sql = string.Format(@"
                              DELETE FROM SERVICE
                              WHERE SERVICE_ID = {0}",
                              service.Id);

            ExecuteQuery(sql);
        }

        /// <summary>
        /// Modifies the service for all clinics and providers
        /// </summary>
        /// <param name="service"></param>
        public void Update(Service service)
        {
            string sql = string.Format(@"
                         UPDATE SERVICE
                         SET NAME = '{1}', 
                             COST = '{2}',
                             MINUTES = {3}
                         WHERE SERVICE_ID = {0}
                         ",
                         service.Id,
                         service.Name,
                         service.Cost,
                         service.Minutes);

            ExecuteQuery(sql);
        }

        private List<Clinic.BO.Clinic> GetClinicsUsingService(string serviceName)
        {
            var obj = new List<Clinic.BO.Clinic>();

            string sql = string.Format(@"
                         SELECT c.*
                         FROM CLINIC c
                         JOIN SERVICE_CLINIC sc ON c.CLINIC_ID = sc.CLINIC_ID
                         JOIN SERVICE s ON sc.SERVICE_ID = s.SERVICE_ID
                         WHERE s.NAME = '{0}'
                         ", serviceName);

            ExecuteReader(sql, obj, DataLayer.ClinicDL.Populate);

            return obj;
        }

        private List<Provider> GetProvidersUsingService(string serviceName)
        {
            var obj = new List<Provider>();

            string sql = string.Format(@"
                         SELECT p.*
                         FROM PROVIDER p
                         JOIN PROVIDER_QUALIFICATION pq ON p.PROVIDER_ID = pq.PROVIDER_ID
                         JOIN SERVICE s ON pq.SERVICE_ID = s.SERVICE_ID
                         WHERE s.NAME = '{0}'
                         ", serviceName);

            ExecuteReader(sql, obj, DataLayer.ProviderDL.Populate);

            return obj;
        }
    }
}