using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class AppointmentDL : DlBase
    {
        private void Populate(Object obj, OracleDataReader reader)
        {
            //var target = (Role)obj;
            //target.Id = Convert.ToInt32(reader["role_id"]);
            //target.Name = reader["role_name"].ToString();
        }
    }
}