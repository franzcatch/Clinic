using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class AppointmentService : BusinessBase
    {
        public Provider Provider { get; set; }
        public Service Service { get; set; }
        public Room Room { get; set; }
        public bool IsQualified
        {
            get
            {
                return Provider.Services.Any(x => x.Id == Service.Id);
            }
        }
        public decimal Cost { get; set; }
        public DateTime StartTime { get; set; }
        public string StartTimeString
        {
            get
            {
                return StartTime.ToString("dd-MMM-yyyy HH:mm").ToUpper();
            }
        }
    }
}