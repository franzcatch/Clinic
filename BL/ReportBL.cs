
using Clinic.BO;
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
        public ExpandoObject GenerateAllHouseholdReport()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.Households = BusinessLayer.HouseholdBL.GetAll();

            return obj;
        }

        /// <summary>
        /// List the insurance coverage for all households by household ID, household name, insurance
        /// company ID and company name.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetHouseholdAndInsurance()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            obj.Households = BusinessLayer.HouseholdBL.GetAll();

            return obj;
        }

        /// <summary>
        /// List all patients in alphabetical order by patient ID, name, and date of birth along with the
        /// name of the insurance company and policy number.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetAllPatientsAndInsurance()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();
            

            return obj;
        }

        /// <summary>
        /// Show itemized billings for all households with the household ID, household name, patient ID,
        /// patient name, service received, and the cost of the service.Show the output in alphabetical
        /// order by household name, patient name and billing date.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetAllBilling()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// List the total cost of all services received for each household.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetHouseholdTotalCosts()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// List each provider with all services he or she is qualified to render.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetProvidersAndServices()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// List each service available with all providers who are qualified to offer this service.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetServicesAndProviders()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// List all future appointments by name of patient, appointment date and time, estimated length
        /// of service, and contact home phone number.Dates and times should be in calendar order
        /// for each patient.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetFutureAppointmentsByPatient()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// For a given date, list all services provided by each provider in alphabetical order by name of
        /// the provider.Show the service ID, service description and cost of service.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetAllServicesProvided()
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }

        /// <summary>
        /// For a given date, list the total amount of services each provider rendered.Show in
        /// alphabetical order by the provider’s name.
        /// </summary>
        /// <returns></returns>
        public ExpandoObject GetTotalServicesForProviders(DateTime serviceDate)
        {
            dynamic obj = new System.Dynamic.ExpandoObject();


            return obj;
        }
    }
}