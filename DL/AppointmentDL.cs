using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class AppointmentDL : DlBase
    {
        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Appointment)obj;
            target.Id = Convert.ToInt32(reader["appointment_id"]);
            target.Clinic = DataLayer.ClinicDL.Get(Convert.ToInt32(reader["clinic_id"]));
            target.Person = DataLayer.PersonDL.Get(Convert.ToInt32(reader["household_person_id"]));
            target.AppointmentServices = GetAppointmentServices(target.Id.Value);
        }

        private void PopulateService(Object obj, OracleDataReader reader)
        {
            var target = (AppointmentService)obj;
            target.Id = Convert.ToInt32(reader["appointment_service_id"]);
            target.Provider = DataLayer.ProviderDL.Get(Convert.ToInt32(reader["provider_id"]));
            target.Service = DataLayer.ServiceDL.Get(Convert.ToInt32(reader["service_id"]));
            target.Room = DataLayer.RoomDL.Get(Convert.ToInt32(reader["service_id"]));
            target.StartTime = DateTime.Parse(reader["time"].ToString());
        }

        public void Create(Appointment appointment)
        {
            int id = GetNextVal(Sequences.Appointment);

            string sql = string.Format(@"
                  INSERT INTO APPOINTMENT
                  (APPOINTMENT_ID, CLINIC_ID, HOUSEHOLD_PERSON_ID)
                  VALUES 
                  ({0},{1},{2})
                  ",
                  id,
                  appointment.Clinic.Id,
                  appointment.Person.Id);

            ExecuteQuery(sql);

            appointment.Id = id;

            foreach(var appointmentService in appointment.AppointmentServices)
            {
                AddService(appointment, appointmentService);
            }
        }

        public void Delete(Appointment appointment)
        {
            appointment.AppointmentServices.ForEach(x => DeleteService(x));

            string sql = string.Format(@"
                              DELETE FROM APPOINTMENT
                              WHERE APPOINTMENT_ID = {0} 
                              ",
                              appointment.Id);

            ExecuteQuery(sql);
        }

        public void AddService(Appointment appointment, AppointmentService appointmentService)
        {
            int id = GetNextVal(Sequences.AppointmentService);

            string sql = string.Format(@"
                  INSERT INTO APPOINTMENT_SERVICE
                  (APPOINTMENT_SERVICE_ID, 
                   APPOINTMENT_ID,
                   PROVIDER_ID,
                   SERVICE_ID,
                   ROOM_ID,
                   COST,
                   TIME)
                  VALUES 
                  ({0},{1},{2},{3},{4},{5},TO_DATE('{6}', 'dd/mon/yyyy HH24:MI'))
                  ",
                  id,
                  appointment.Id,
                  appointmentService.Provider.Id,
                  appointmentService.Service.Id,
                  appointmentService.Room.Id,
                  appointmentService.Cost,
                  appointmentService.StartTimeString);

            ExecuteQuery(sql);

            appointmentService.Id = id;
        }
        
        public void DeleteService(AppointmentService appointmentService)
        {
            string sql = string.Format(@"
                              DELETE FROM APPOINTMENT_SERVICE
                              WHERE APPOINTMENT_SERVICE_ID = {0} 
                              ",
                              appointmentService.Id);

            ExecuteQuery(sql);
        }

        public List<Appointment> GetAppointmentsForUser(int userId, DateTime? date)
        {
            var obj = new List<Appointment>();

            var dateFilter = !date.HasValue ? ""
                             : "AND TRUNC(aps.TIME) = TO_DATE('" + date.Value.ToString("dd-MMM-yyyy").ToUpper() + "')";

            string sql = string.Format(@"
                         SELECT aps.*
                         FROM APPOINTMENT_SERVICE aps
                         JOIN APPOINTMENT a ON aps.APPOINTMENT_ID = a.APPOINTMENT_ID
                         JOIN HOUSEHOLD_PERSON_ID hp ON a.HOUSEHOLD_PERSON_ID = hp.HOUSEHOLD_PERSON_ID
                         JOIN USERS u ON hp.ENTITY_ID = u.ENTITY_ID
                         WHERE u.USER_ID = {0}
                               {1}
                         ",
                         userId,
                         dateFilter);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<Appointment> GetAppointmentsForClinic(int clinicId, DateTime? date)
        {
            var obj = new List<Appointment>();

            var dateFilter = !date.HasValue ? ""
                             : "AND TRUNC(aps.TIME) = TO_DATE('" + date.Value.ToString("dd-MMM-yyyy").ToUpper() + "')";

            string sql = string.Format(@"
                         SELECT aps.*
                         FROM APPOINTMENT_SERVICE aps
                         JOIN APPOINTMENT a ON aps.APPOINTMENT_ID = a.APPOINTMENT_ID
                         WHERE a.CLINIC_ID = {0}
                               {1}
                         ",
                         clinicId,
                         dateFilter);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public List<AppointmentService> GetExistingAppointmentServices(int clinicId, DateTime serviceDate)
        {
            var obj = new List<AppointmentService>();

            string sql = string.Format(@"
                         SELECT aps.*
                         FROM APPOINTMENT_SERVICE aps
                         JOIN APPOINTMENT a ON aps.APPOINTMENT_ID = a.APPOINTMENT_ID
                         WHERE a.CLINIC_ID = {0}
                           AND TRUNC(aps.TIME) = TO_DATE('{1}')
                         ",
                         clinicId,
                         serviceDate.ToString("dd-MMM-yyyy").ToUpper());

            ExecuteReader(sql, obj, PopulateService);

            return obj;
        }

        public List<AppointmentService> GetAppointmentServices(int appointmentId)
        {
            var obj = new List<AppointmentService>();

            string sql = string.Format(@"
                         SELECT *
                         FROM APPOINTMENT_SERVICE
                         WHERE APPOINTMENT_ID = {0}
                         ",
                         appointmentId);

            ExecuteReader(sql, obj, PopulateService);

            return obj;
        }
    }
}