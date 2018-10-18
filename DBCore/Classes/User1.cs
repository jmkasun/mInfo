using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;

namespace DBCore.Classes
{
    public class User : DBBase, IDBFunctions
    {
        public int ID;
        public string Name;
        public string UserName;
        public string Password;
        public string Mobile;
        public string Email;
        public int PermissionLevel;

        public User()
        {

        }

        public User(bool withConn)
            : base(withConn)
        {

        }

        #region IDBFunctions Members

        public int Add()
        {

            AddParameter("@p_Name", Name);
            AddParameter("@p_UserName", UserName);
            AddParameter("@p_Password", Password);
            AddParameter("@p_Mobile", Mobile);
            AddParameter("@p_Email", Email);
            AddParameter("@p_PermissionLevel", (int)PermissionLevel);


            return ExecuteNonQuery("User_Add");
        }


        public int Delete()
        {
            //string SQL = "UPDATE SystemUser SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("User_Del");
        }

        public int Update(bool changePwd)
        {

            AddParameter("@p_Name", Name);
            AddParameter("@p_UserName", UserName);

            if (!changePwd)
            {
                Password = "-1";
            }

            AddParameter("@p_Password", Password);
            AddParameter("@p_Mobile", Mobile);
            AddParameter("@p_Email", Email);
            AddParameter("@p_PermissionLevel", (int)PermissionLevel);
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("User_Upd");
        }

        #endregion



        // check item name or code already added
        public bool SelectExists(int userID)
        {
            AddParameter("@p_UserName", UserName);
            AddParameter("@p_ID", userID);

            object val = ExecuteScalar("User_Exist");

            return val != null;
        }

        public System.Data.DataTable SelectFind()
        {

            AddParameter("@p_Name", Name);
            AddParameter("@p_UserName", UserName);
            AddParameter("@p_Mobile", Mobile);
            AddParameter("@p_Email", Email);
            AddParameter("@p_PermissionLevel", (int)PermissionLevel);

            return GetTable("User_Find");

        }

        #region IDBFunctions Members


        public int Update()
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool Login()
        {
            AddParameter("@p_UserName", UserName);
            AddParameter("@p_Password", Password);

            using (MySqlDataReader reader = ExecuteReader("User_Login"))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                    Name = reader.GetString(1);
                    UserName = reader.GetString(2);
                    Mobile = reader.GetString(3);
                    Email = reader.GetString(4);
                    PermissionLevel = reader.GetInt32(5);

                    return true;
                }
            }

            return false;
        }

        #region IDBFunctions Members


        public DataTable SelectAll()
        {
            throw new NotImplementedException();
        }

        #endregion


        public User SelectUser(int ID)
        {

            AddParameter("@p_ID", ID);

            using (MySqlDataReader reader = ExecuteReader("User_Sel"))
            {


                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                    Name = reader.GetString(1);
                    UserName = reader.GetString(2);
                    Mobile = reader.GetString(3);
                    Email = reader.GetString(4);
                    PermissionLevel = reader.GetInt32(5);
                }


            }

            return this;
        }
    }
}
