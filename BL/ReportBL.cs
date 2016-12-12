
using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class ReportBL
    {
        /// <summary>
        /// List all households with the household ID, name, address, and home phone along with the
        /// patient ID, name and relationship for each patient.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GenerateAllHouseholdReport()
        {
            return DataLayer.ReportDL.GenerateAllHouseholdReport();
        }

        /// <summary>
        /// List the insurance coverage for all households by household ID, household name, insurance
        /// company ID and company name.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetHouseholdAndInsurance()
        {
            return DataLayer.ReportDL.GetHouseholdAndInsurance();
        }

        /// <summary>
        /// List all patients in alphabetical order by patient ID, name, and date of birth along with the
        /// name of the insurance company and policy number.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllPatientsAndInsurance()
        {
            return DataLayer.ReportDL.GetAllPatientsAndInsurance();
        }

        /// <summary>
        /// Show itemized billings for all households with the household ID, household name, patient ID,
        /// patient name, service received, and the cost of the service.Show the output in alphabetical
        /// order by household name, patient name and billing date.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllBilling()
        {
            return DataLayer.ReportDL.GetAllBilling();
        }

        /// <summary>
        /// List the total cost of all services received for each household.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetHouseholdTotalCosts()
        {
            return DataLayer.ReportDL.GetHouseholdTotalCosts();
        }

        /// <summary>
        /// List each provider with all services he or she is qualified to render.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetProvidersAndServices()
        {
            return DataLayer.ReportDL.GetProvidersAndServices();
        }

        /// <summary>
        /// List each service available with all providers who are qualified to offer this service.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetServicesAndProviders()
        {
            return DataLayer.ReportDL.GetServicesAndProviders();
        }

        /// <summary>
        /// List all future appointments by name of patient, appointment date and time, estimated length
        /// of service, and contact home phone number.Dates and times should be in calendar order
        /// for each patient.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetFutureAppointmentsByPatient()
        {
            return DataLayer.ReportDL.GetFutureAppointmentsByPatient();
        }

        /// <summary>
        /// For a given date, list all services provided by each provider in alphabetical order by name of
        /// the provider.Show the service ID, service description and cost of service.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetAllServicesProvided()
        {
            return DataLayer.ReportDL.GetAllServicesProvided();
        }

        /// <summary>
        /// For a given date, list the total amount of services each provider rendered.Show in
        /// alphabetical order by the provider’s name.
        /// </summary>
        /// <returns></returns>
        public List<ExpandoObject> GetTotalServicesForProviders(DateTime serviceDate)
        {
            return DataLayer.ReportDL.GetTotalServicesForProviders(serviceDate);
        }
    }
}