using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class ClinicBL
    {
        public Clinic.BO.Clinic Get(int id)
        {
            return DataLayer.ClinicDL.Get(id);
        }

        public List<Clinic.BO.Clinic> GetClinics()
        {
            return DataLayer.ClinicDL.GetClinics();
        }

        public void Delete(int id)
        {
            DataLayer.ClinicDL.Delete(id);
        }

        public List<User> GetEligibleProviders(int clinicId) {
            var allStaff = BusinessLayer.UserBL.GetStaff();

            if (clinicId > 0)
            {
                var eligibleUsers = new List<User>();

                var clinic = Get(clinicId);
                foreach (var potential in allStaff)
                {
                    if (!clinic.Providers.Any(existing => existing.EntityId == potential.EntityId))
                    {
                        eligibleUsers.Add(potential);
                    }
                }

                return eligibleUsers;
            }
            else
            {
                return allStaff;
            }            
        }

        public void Update(Clinic.BO.Clinic clinic)
        {
            if (clinic.EntityId == null)
            {
                BusinessLayer.EntityBL.Create(clinic);
            }
            else
            {
                BusinessLayer.EntityBL.Update(clinic);
            }

            if (clinic.Id.HasValue)
            {
                DataLayer.ClinicDL.Update(clinic);
            }
            else
            {
                DataLayer.ClinicDL.Create(clinic);
            }

            var existingClinic = BusinessLayer.ClinicBL.Get(clinic.Id.Value);

            var newProviders = clinic.Providers.Where(x => !existingClinic.Providers.Any(existing => existing.Id == x.Id)).ToList();
            var removedProviders = existingClinic.Providers.Where(existing => !clinic.Providers.Any(cur => cur.Id.Value == existing.Id.Value)).ToList();

            var newServices = clinic.Services.Where(x => !x.Id.HasValue).ToList();
            var updatedServices = clinic.Services.Where(x => x.Id.HasValue).ToList();
            var removedServices = existingClinic.Services.Where(existing => !updatedServices.Any(cur => cur.Id.Value == existing.Id.Value)).ToList();

            var newRooms = clinic.Rooms.Where(x => !x.Id.HasValue).ToList();
            var updatedRooms = clinic.Rooms.Where(x => x.Id.HasValue).ToList();
            var removedRooms = existingClinic.Rooms.Where(existing => !updatedRooms.Any(cur => cur.Id.Value == existing.Id.Value)).ToList();

            newProviders.ForEach(x => DataLayer.ProviderDL.Create(clinic.Id.Value, x));
            removedProviders.ForEach(x => DataLayer.ProviderDL.Delete(clinic.Id.Value, x));

            newServices.ForEach(x => DataLayer.ServiceDL.AddToClinic(x, clinic));
            updatedServices.ForEach(x => DataLayer.ServiceDL.Update(x));
            removedServices.ForEach(x => DataLayer.ServiceDL.DeleteFromClinic(x, clinic));

            newRooms.ForEach(x => DataLayer.RoomDL.Create(clinic.Id.Value, x));
            updatedRooms.ForEach(x => DataLayer.RoomDL.Update(x));
            removedRooms.ForEach(x => DataLayer.RoomDL.Delete(x));
        }
    }
}