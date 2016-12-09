using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Provider : Entity
    {
        public string FirstName
        {
            get
            {
                return this.Name1;
            }
            set
            {
                this.Name1 = value;
            }
        }
        public string MiddleName
        {
            get
            {
                return this.Name2;
            }
            set
            {
                this.Name2 = value;
            }
        }
        public string LastName
        {
            get
            {
                return this.Name3;
            }
            set
            {
                this.Name3 = value;
            }
        }
        public List<Service> Services { get; set; }
    }
}