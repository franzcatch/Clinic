using Clinic.BO;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class RoomDL : DlBase
    {
        public void Populate(Object obj, OracleDataReader reader)
        {
            var target = (Room)obj;

            target.Id = Int32.Parse(reader["room_id"].ToString());
            target.Name = reader["name"].ToString();
        }

        public List<Room> GetRoomsByClinicId(int clinicId)
        {
            var obj = new List<Room>();

            string sql = string.Format(@"
                         SELECT s.* 
                         FROM ROOM
                         WHERE CLINIC_ID = {0}
                         ", clinicId);

            ExecuteReader(sql, obj, Populate);

            return obj;
        }

        public void Create(int clinicId, Room room)
        {
            int id = GetNextVal(Sequences.Room);

            string sql = string.Format(@"
                         INSERT INTO ROOM
                         (ROOM_ID, CLINIC_ID, NAME)
                         VALUES 
                         ({0},{1},'{2}')
                         ",
                         id,
                         clinicId,
                         room.Name);

            ExecuteQuery(sql);

            room.Id = id;
        }

        public void Update(Room room)
        {
            string sql = string.Format(@"
                         UPDATE ROOM
                         SET NAME = {1}
                         WHERE ROOM_ID = {0}
                         ",
                         room.Id,
                         room.Name);

            ExecuteQuery(sql);
        }

        public void Delete(Room room)
        {
            string sql = string.Format(@"
                         DELETE FROM ROOM
                         WHERE ROOM_ID = {0}
                         ",
                         room.Id);

            ExecuteQuery(sql);
        }
    }
}