using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Person : Entity
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
        public int HouseholdId { get; set; }
        public bool IsPayer { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string DateOfBirthString
        {
            get
            {
                return DateOfBirth.ToString("dd-MMM-yyyy").ToUpper();
            }
        }
        public Relationship Relationship { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}