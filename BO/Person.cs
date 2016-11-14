using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Person : Entity
    {
        public bool IsPayer { get; set; }
        public int HouseholdId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}