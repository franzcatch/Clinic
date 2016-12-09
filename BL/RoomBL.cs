using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class RoomBL
    {
        public List<Room> GetRoomsByClinicId(int clinicId)
        {
            return DataLayer.RoomDL.GetRoomsByClinicId(clinicId);
        }
    }
}