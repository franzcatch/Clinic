using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class RelationshipDL : DlBase
    {
        private List<Relationship> _relationships;

        private List<Relationship> Relationships {
            get
            {
                if(_relationships == null)
                {
                    _relationships = new List<Relationship>();

                    string sql = string.Format(@"
                         SELECT *
                         FROM RELATIONSHIP");

                    this.ExecuteReader(sql, _relationships, Populate);
                }                

                return _relationships;
            }
        }

        private void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Relationship)obj;
            target.Id = Convert.ToInt32(reader["relationship_id"]);
            target.Name = reader["name"].ToString();
        }

        public Relationship Get(int id)
        {
            return Relationships.First(x => x.Id == id);
        }

        public Relationship Get(string name)
        {
            return Relationships.First(x => x.Name == name);
        }

        public List<Relationship> GetRelationships()
        {
            return Relationships;
        }
    }
}