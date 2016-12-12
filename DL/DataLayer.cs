using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public static class DataLayer
    {
        public static UserDL UserDL = new UserDL();
        public static EntityDL EntityDL = new EntityDL();
        public static RoleDL RoleDL = new RoleDL();
        public static PersonDL PersonDL = new PersonDL();
        public static HouseholdDL HouseholdDL = new HouseholdDL();
        public static RelationshipDL RelationshipDL = new RelationshipDL();
        public static ProviderDL ProviderDL = new ProviderDL();
        public static ServiceDL ServiceDL = new ServiceDL();
        public static RoomDL RoomDL = new RoomDL();
        public static ClinicDL ClinicDL = new ClinicDL();
        public static AppointmentDL AppointmentDL = new AppointmentDL();
        public static ReportDL ReportDL = new ReportDL();
    }
}