using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class RelationshipBL
    {
        public Relationship Get(int id)
        {
            return DataLayer.RelationshipDL.Get(id);
        }

        public Relationship Get(string name)
        {
            return DataLayer.RelationshipDL.Get(name);
        }

        public List<Relationship> GetRelationships() {
            return DataLayer.RelationshipDL.GetRelationships();
        }
    }
}