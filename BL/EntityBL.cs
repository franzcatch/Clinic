using Clinic.BO;
using Clinic.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.BL
{
    public class EntityBL
    {
        public Entity Get(int id)
        {
            return DataLayer.EntityDL.Get(id);
        }

        public void Create(Entity entity)
        {
            DataLayer.EntityDL.Create(entity);
        }
        
        public void Update(Entity entity)
        {
            DataLayer.EntityDL.Update(entity);
        }
    }
}