using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class AppointmentBL
    {
        public List<Appointment> GetAppointmentsForClinic(int clinicId, DateTime? date)
        {
            return DataLayer.AppointmentDL.GetAppointmentsForClinic(clinicId, date);
        }

        public List<Appointment> GetAppointmentsForUser(int userId, DateTime? date)
        {
            return DataLayer.AppointmentDL.GetAppointmentsForUser(userId, date);
        }

        public void Create(Appointment appointment)
        {
            DataLayer.AppointmentDL.Create(appointment);
        }

        public void Delete(Appointment appointment)
        {
            DataLayer.AppointmentDL.Delete(appointment);
        }

        public List<AppointmentService> GetAvailableAppointments(int clinicId, int serviceId, DateTime date)
        {
            var desiredService = DataLayer.ServiceDL.Get(serviceId);

            var consumedTimes = new List<DateTime>();
            var allStartTimes = new List<DateTime>();
            var minBetween = 30;
            var openTime = new DateTime(date.Year, date.Month, date.Day).AddHours(8); // 1st appointment @ 8am
            var curTime = openTime; // this is not a reference assignment - .NET creates a copy
            var closeTime = openTime.AddHours(8 + 10); // Last appointment ends by 6pm

            // making a list of half-hour blocked datetimes
            while (curTime != closeTime)
            {
                allStartTimes.Add(curTime);
                curTime = curTime.AddMinutes(minBetween);
            }

            var existingAptSvcs = DataLayer.AppointmentDL.GetExistingAppointmentServices(clinicId, date);
            var takenAptSvcs = new List<AppointmentService>();
            var allProviders = DataLayer.ProviderDL.GetProvidersByClinicId(clinicId);
            var allRooms = DataLayer.RoomDL.GetRoomsByClinicId(clinicId);

            // create a massive list of every possible combination of appointment time, provider, room, service in a given day
            var availableAppointments = new List<AppointmentService>();

            allStartTimes.ForEach(time =>
                allProviders.ForEach(provider =>
                    allRooms.ForEach(room =>
                        availableAppointments.Add(new AppointmentService
                        {
                            Cost = desiredService.Cost,
                            Provider = provider,
                            Room = room,
                            Service = desiredService,
                            StartTime = time
                        })
                    )
                )
            );

            // making takenAptSvcs a list of appointment times consumed for every half hour each existing appointment occupies a room
            foreach (var time in allStartTimes)
            {
                var aptSvcsAtCurTime = existingAptSvcs.Where(x => x.StartTime == curTime).ToList();
                foreach (var aptSvc in aptSvcsAtCurTime)
                {
                    for (var curMinutes = 0; curMinutes < aptSvc.Service.Minutes; curMinutes += minBetween)
                    {
                        takenAptSvcs.Add(new AppointmentService
                        {
                            Id = aptSvc.Id,
                            Cost = aptSvc.Cost,
                            Provider = aptSvc.Provider,
                            Room = aptSvc.Room,
                            Service = aptSvc.Service,
                            StartTime = aptSvc.StartTime.AddMinutes(curMinutes)
                        });
                    }
                }
            }

            // add times that will end after close of business to the taken list
            availableAppointments.RemoveAll(avail =>
                avail.StartTime.AddMinutes(avail.Service.Minutes) > closeTime
            );

            // remove "available" appointments that start less than "desiredService.Minutes" prior to an existing appointment
            foreach (var takenAptSvc in takenAptSvcs)
            {
                availableAppointments.RemoveAll(avail =>
                    avail.StartTime.AddMinutes(avail.Service.Minutes) > avail.StartTime
                );
            }

            // remove "available" appointments that are already taken
            foreach (var takenAptSvc in takenAptSvcs)
            {
                availableAppointments.RemoveAll(avail =>
                    avail.StartTime == avail.StartTime &&
                    avail.Provider.Id == avail.Provider.Id &&
                    avail.Service.Id == avail.Service.Id &&
                    avail.Room.Id == avail.Room.Id);
            }

            return availableAppointments
                .OrderBy(x => x.StartTime)
                .ThenBy(x => x.IsQualified)
                .ThenBy(x => x.Provider.FirstName)
                .ThenBy(x => x.Provider.LastName)
                .ThenBy(x => x.Room.Name)
                .ToList();
        }
    }
}