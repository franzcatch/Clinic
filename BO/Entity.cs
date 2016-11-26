using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BO
{
    public class Entity : BusinessBase
    {
        public int? EntityId { get; set; }
        protected string Name1 { get; set; }
        protected string Name2 { get; set; }
        protected string Name3 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }

        public void SetName(string name1, string name2, string name3)
        {
            Name1 = name1;
            Name2 = name2;
            Name3 = name3;
        }

        public string GetName1()
        {
            return Name1;
        }

        public string GetName2()
        {
            return Name2;
        }

        public string GetName3()
        {
            return Name3;
        }

        public Entity Copy ()
        {
            return new Entity
            {
                EntityId = this.EntityId,
                Name1 = this.Name1,
                Name2 = this.Name2,
                Name3 = this.Name3,
                Address1 = this.Address1,
                Address2 = this.Address2,
                City = this.City,
                State = this.State,
                Zip = this.Zip,
                Phone1 = this.Phone1,
                Phone2 = this.Phone2,
                Phone3 = this.Phone3
            };
        }

        public bool IsEmpty() {
            return !EntityId.HasValue &&
                    string.IsNullOrWhiteSpace(Name1) &&
                    string.IsNullOrWhiteSpace(Name2) &&
                    string.IsNullOrWhiteSpace(Name3) &&
                    string.IsNullOrWhiteSpace(Address1) &&
                    string.IsNullOrWhiteSpace(Address2) &&
                    string.IsNullOrWhiteSpace(City) &&
                    string.IsNullOrWhiteSpace(State) &&
                    string.IsNullOrWhiteSpace(Zip) &&
                    string.IsNullOrWhiteSpace(Phone1) &&
                    string.IsNullOrWhiteSpace(Phone2) &&
                    string.IsNullOrWhiteSpace(Phone3);
        }
    }
}