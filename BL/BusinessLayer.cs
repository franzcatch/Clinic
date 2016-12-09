using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public static class BusinessLayer
    {
        public static UserBL UserBL = new UserBL();
        public static HouseholdBL HouseholdBL = new HouseholdBL();
        public static PersonBL PersonBL = new PersonBL();
        public static RoleBL RoleBL = new RoleBL();
        public static EntityBL EntityBL = new EntityBL();
        public static RelationshipBL RelationshipBL = new RelationshipBL();
        public static ClinicBL ClinicBL = new ClinicBL();
        public static ServiceBL ServiceBL = new ServiceBL();
        public static RoomBL RoomBL = new RoomBL();
        public static ReportBL ReportBL = new ReportBL();
    }
}