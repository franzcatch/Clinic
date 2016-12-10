using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Appointment : BusinessBase
    {
        public Person Person { get; set; }
        public Clinic Clinic { get; set; }
        public List<AppointmentService> AppointmentServices { get; set; }
    }
}