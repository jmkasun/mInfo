using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBCore.Common
{
    [Serializable]
    public class DBBase : IDisposable
    {
        [NonSerialized]
        protected MySqlConnection conn = null;
        [NonSerialized]
        protected MySqlCommand command = null;
        [NonSerialized]
        protected MySqlDataAdapter adapter = null;
        private bool rollback;


        public DBBase()
        {
        }

        public DBBase(bool initConn)
        {
            InitConnection();
        }

        protected void InitConnection()
        {
            conn = new MySqlConnection(Utility.GetConnectionString());

            conn.Open();
        }



        #region IDisposable Members

        public void Dispose()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                if (command != null)
                    command.Parameters.Clear();

                conn.Close();
                conn.Dispose();
            }
        }

        #endregion


        protected void AddParameter(string name, object value)
        {
            if (command == null)
            {
                command = new MySqlCommand();
                command.CommandType = CommandType.StoredProcedure;
            }
          
            command.Parameters.AddWithValue(name, value);
        }

        protected void ClearParameters()
        {
            if (command != null)
            {
                command.Parameters.Clear();
            }

        }

        // add output parameter
        protected void AddParameter(string name, MySqlDbType type)
        {
            if (command == null)
            {
                command = new MySqlCommand();
                command.CommandType = CommandType.StoredProcedure;
            }

            MySqlParameter param = new MySqlParameter(name, type);
            param.Direction = ParameterDirection.Output;
            param.IsNullable = true;

            command.Parameters.Add(param);
        }

        protected object GetOutputValue(string name)
        {
            return command.Parameters[name].Value;
        }

        protected object ExecuteScalar(string SQL)
        {
            setCommandProperties(SQL);

            object res = command.ExecuteScalar();

            command.Parameters.Clear();
            return res;
        }

        protected int ExecuteNonQuery(string SQL)
        {
            setCommandProperties(SQL);
            
            int res = command.ExecuteNonQuery();
            command.Parameters.Clear();
            return res;
        }

        protected int ExecuteNonQueryOutput(string SQL)
        {
            setCommandProperties(SQL);

            int res = command.ExecuteNonQuery();
           // command.Parameters.Clear();
            return res;
        }

        protected MySqlDataReader ExecuteReader(string SQL)
        {
            setCommandProperties(SQL);

            MySqlDataReader reader = command.ExecuteReader();
            command.Parameters.Clear();
            return reader;
        }


        protected DataTable GetTable(string SQL)
        {
            DataTable data = new DataTable();
            setCommandProperties(SQL);
            adapter = new MySqlDataAdapter(command);

            adapter.Fill(data);

            command.Parameters.Clear();
            return data;
        }

        private void setCommandProperties(string SQL)
        {
            if (command == null)
            {
                command = new MySqlCommand(SQL, conn);
                command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                command.CommandText = SQL;
                command.Connection = conn;
            }
        }
    }
}
