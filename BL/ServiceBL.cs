using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class ServiceBL
    {
        public List<Service> Get()
        {
            return DataLayer.ServiceDL.Get();
        }

        public List<Service> GetEligibleProviderServicesForUserId(int userId)
        {
            return DataLayer.ServiceDL.GetEligibleProviderServicesForUserId(userId);
        }

        public List<Service> GetProviderServicesForUserId(int userId)
        {
            return DataLayer.ServiceDL.GetProviderServicesForUserId(userId);
        }

        public void UpdateProviderServicesForUserId(int userId, List<Service> services)
        {
            var existingServices = GetProviderServicesForUserId(userId);

            var newServices = services.Where(x => !existingServices.Any(existing => existing.Id == x.Id)).ToList();
            var deletedServices = existingServices.Where(existing => !services.Any(x => x.Id == existing.Id)).ToList();

            var provider = DataLayer.ProviderDL.GetProviderByUserId(userId);

            newServices.ForEach(x => DataLayer.ServiceDL.AddToProvider(x, provider));
            deletedServices.ForEach(x => DataLayer.ServiceDL.DeleteFromProvider(x, provider));
        }
    }
}