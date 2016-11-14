using Clinic.Lib;
using Oracle.DataAccess.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clinic.DL
{
    public class DlBase
    {
        protected string _connectionString = "Data Source=terminus;User ID=AFRANZEN;Password=P0ppyCh3w";

        public DlBase() {
            
        }

        public OracleCommand GetCommand(string sqlQuery) {
            OracleConnection con = new OracleConnection(_connectionString);
            OracleCommand cmd = new OracleCommand(sqlQuery, con);

            con.Open();

            return cmd;
        }

        public void CloseConnection(OracleCommand cmd)
        {
            cmd.Connection.Close();
        }

        protected void ExecuteQuery(string sqlQuery)
        {
            var cmd = GetCommand(sqlQuery);

            cmd.ExecuteNonQuery();

            CloseConnection(cmd);
        }

        private void PopulateList(Object obj, OracleDataReader reader, PopulateData populateData)
        {
            Type type = obj.GetType().GetGenericArguments()[0];
            var instance = Activator.CreateInstance(type);
            populateData(instance, reader);
            ((IList)obj).Add(instance);
        }

        public delegate void PopulateData(Object data, OracleDataReader reader);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <param name="data">A single object of T or a List<T> where T is the datatype in PopulateData</param>
        /// <param name="populateData"></param>
        protected void ExecuteReader(string sqlQuery, Object data, PopulateData populateData)
        {
            var cmd = GetCommand(sqlQuery);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (data.GetType().IsGenericType && data is IEnumerable)
                {
                    PopulateList(data, reader, populateData);
                }
                else
                {
                    populateData(data, reader);
                }
            }

            CloseConnection(cmd);
        }

        protected Object ExecuteScalar(string sqlQuery)
        {
            var cmd = GetCommand(sqlQuery);

            var obj = cmd.ExecuteScalar();

            CloseConnection(cmd);

            return obj;
        }

        protected int GetNextVal(string sequenceName)
        {
            string sql = "SELECT " + sequenceName + ".nextval FROM dual";

            return Int16.Parse(ExecuteScalar(sql).ToString());
        }
    }
}