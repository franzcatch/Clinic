using Clinic.BL;
using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class ClinicDL : DlBase
    {
        public void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Clinic.BO.Clinic)obj;

            int entityId = Int32.Parse(reader["entity_id"].ToString());
            DataLayer.EntityDL.Get(entityId).CopyTo(target);

            target.Id = Int32.Parse(reader["clinic_id"].ToString());
            target.Providers = DataLayer.ProviderDL.GetProvidersByClinicId(target.Id.Value);
            target.Rooms = DataLayer.RoomDL.GetRoomsByClinicId(target.Id.Value);
            target.Services = DataLayer.ServiceDL.GetServicesByClinicId(target.Id.Value);
        }

        public Clinic.BO.Clinic Get(int id)
        {
            var obj = new Clinic.BO.Clinic();

            string sql = string.Format(@"
                         SELECT * FROM CLINIC 
                         WHERE CLINIC_ID = {0}
                         ", id);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Clinic.BO.Clinic> GetClinics()
        {
            var obj = new List<Clinic.BO.Clinic>();

            string sql = string.Format(@"
                         SELECT * FROM CLINIC 
                         ");

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(Clinic.BO.Clinic clinic)
        {
            int id = GetNextVal(Sequences.Clinic);

            string sql = string.Format(@"
                         INSERT INTO CLINIC
                         (CLINIC_ID, ENTITY_ID)
                         VALUES 
                         ({0},{1})
                         ",
                         id,
                         clinic.EntityId);

            ExecuteQuery(sql);

            clinic.Id = id;
        }

        public void Update(Clinic.BO.Clinic clinic)
        {
            // DO Nothing
            // Name (on entity) and all others should be update in their respective layers
        }

        public void Delete(int clinicId)
        {
            string sql = string.Format(@"
                         DELETE CLINIC
                         WHERE CLINIC_ID = {0}
                         ", clinicId);

            ExecuteQuery(sql);
        }
    }
}