using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class ProviderDL : DlBase
    {
        public void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Provider)obj;

            int entityId = Int32.Parse(reader["entity_id"].ToString());
            DataLayer.EntityDL.Get(entityId).CopyTo(target);

            target.Id = Int32.Parse(reader["clinic_id"].ToString());
            target.Services = DataLayer.ServiceDL.GetServicesByProviderId(target.Id.Value);
        }

        private bool GetIsProvider(int providerId, int clinicId, int entityId)
        {
            string sql = string.Format(@"
                         SELECT ENTITY_ID 
                         FROM PROVIDER 
                         WHERE ENTITY_ID = {0}
                           AND CLINIC_ID = {1}
                           AND PROVIDER_ID = {2}
                         ", entityId, clinicId, providerId);

            var result = ExecuteScalar(sql);

            return string.IsNullOrEmpty(result.ToString());
        }

        public List<Provider> GetProvidersByClinicId(int clinicId)
        {
            var obj = new List<Provider>();

            string sql = string.Format(@"
                         SELECT * FROM PROVIDER 
                         WHERE CLINIC_ID = {0}
                         ", clinicId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(int clinicId, Provider provider)
        {
            string sql;
            int id = GetNextVal(Sequences.Provider);

            sql = string.Format(@"
                  INSERT INTO PROVIDER
                  (PROVIDER_ID, CLINIC_ID, ENTITY_ID)
                  VALUES 
                  ({0},{1},{2})
                  ",
                  id,
                  clinicId,
                  provider.EntityId);

            ExecuteQuery(sql);

            provider.Id = id;
        }

        public void Delete(int clinicId, Provider provider)
        {
            provider.Services.ForEach(x => DeleteProviderQualification(provider, x));

            string sql = string.Format(@"
                              DELETE FROM PROVIDER
                              WHERE PROVIDER_ID = {0} 
                                AND CLINIC_ID = {1}
                                AND ENTITY_ID = {2}
                              ",
                              provider.Id,
                              clinicId,
                              provider.EntityId);

            ExecuteQuery(sql);
        }

        public void Update(int clinicId, Provider provider)
        {
            var existingServices = DataLayer.ServiceDL.GetServicesByProviderId(provider.Id.Value);

            var newServices = provider.Services.Where(x => !x.Id.HasValue).ToList();
            var updatedServices = provider.Services.Where(x => x.Id.HasValue).ToList();
            var removedServices = existingServices.Where(existing => !updatedServices.Any(cur => cur.Id.Value == existing.Id.Value)).ToList();

            newServices.ForEach(x => InsertProviderQualification(provider, x));
            removedServices.ForEach(x => DeleteProviderQualification(provider, x));
        }

        private void InsertProviderQualification(Provider provider, Service qualification)
        {
            var obj = new Service();
            int id = GetNextVal(Sequences.Provider);

            string sql = string.Format(@"
                              INSERT INTO PROVIDER_QUALIFICATION
                              (PROVIDER_QUALIFICATION_ID, PROVIDER_ID, SERVICE_ID)
                              VALUES 
                              ({0},{1},{2})
                              ",
                              id,
                              provider.Id,
                              qualification.Id);

            ExecuteReader(sql, obj, Populate);

            obj.Id = id;

            provider.Services.Add(obj);
        }

        private void DeleteProviderQualification(Provider provider, Service qualification)
        {
            string sql = string.Format(@"
                              DELETE FROM PROVIDER_QUALIFICATION
                              WHERE PROVIDER_ID = {0} 
                                AND SERVICE_ID = {1}
                              ",
                              provider.Id,
                              qualification.Id);

            ExecuteQuery(sql);

            provider.Services.RemoveAll(x => x.Id == qualification.Id);
        }
    }
}