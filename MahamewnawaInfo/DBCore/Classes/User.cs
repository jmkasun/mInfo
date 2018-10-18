using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace DBCore.Classes
{
    public class User : DBBase, IDBFunctions
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string UserName;
        public int Password;
        public string Mobile;
        public UserLevel PermissionLevel;

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
            string SQL = "INSERT INTO SystemUser(FirstName,LastName,UserName,Pwd,PermissionLevel,MobileNumber,Deleted)" +
                        " VALUES(@FirstName,@LastName,@UserName,@Password,@PermissionLevel,@MobileNumber,0)";

            AddParameter("@FirstName", FirstName);
            AddParameter("@LastName", LastName);
            AddParameter("@UserName", UserName);
            AddParameter("@Password", Password);
            AddParameter("@PermissionLevel", (int)PermissionLevel);
            AddParameter("@MobileNumber", Mobile);

            return ExecuteNonQuesry(SQL);
        }



        public System.Data.DataTable SelectAll()
        {
            string SQL = "SELECT  ID,FirstName+' '+LastName AS FullUserName,UserName,Pwd,PermissionLevel,MobileNumber FROM SystemUser WHERE Deleted = 0";

            return GetTable(SQL);
        }

        public int Delete()
        {
            string SQL = "UPDATE SystemUser SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@ID", ID);

            return ExecuteNonQuesry(SQL);
        }

        public int Update(bool changePwd)
        {
            string SQL = "";
            if (changePwd)
                SQL = "UPDATE SystemUser SET FirstName = @FirstName,LastName = @LastName,UserName = @UserName,Pwd = @Password,PermissionLevel = @PermissionLevel,MobileNumber = @MobileNumber WHERE ID = @ID";
            else
                SQL = "UPDATE SystemUser SET FirstName = @FirstName,LastName = @LastName,UserName = @UserName,PermissionLevel = @PermissionLevel,MobileNumber = @MobileNumber WHERE ID = @ID";

            AddParameter("@FirstName", FirstName);
            AddParameter("@LastName", LastName);
            AddParameter("@UserName", UserName);
            if (changePwd)
                AddParameter("@Password", Password);
            AddParameter("@PermissionLevel", (int)PermissionLevel);
            AddParameter("@MobileNumber", Mobile);
            AddParameter("@ID", ID);

            return ExecuteNonQuesry(SQL);
        }

        #endregion


        //public void BindToCombo(ComboBox combo, Dictionary<int, Customer> customerDic)
        //{
        //    DataTable tbl = SelectAll();

        //    combo.DataSource = tbl;

        //    combo.DisplayMember = "FullUserName";
        //    combo.ValueMember = "ID";

        //    foreach (DataRow row in tbl.Rows)
        //    {
        //        Customer cus = new Customer();

        //        cus.ID = (int)row[0];
        //        cus.Name = row[1].ToString();
        //        cus.Mobile = row[2].ToString();
        //        cus.HomeTP = row[2].ToString();

        //        customerDic.Add(cus.ID, cus);
        //    }
        //}

        // check item name or code already added
        public bool SelectExists(int userID)
        {
            string SQL = "SELECT ID FROM SystemUser WHERE UserName = @UserName AND (ID <> @ID OR @ID = 0)AND Deleted = 0";
            AddParameter("@UserName", UserName);
            AddParameter("@ID", userID);

            object val = ExecuteScalar(SQL);

            return val != null;
        }

        public System.Data.DataTable SelectFind()
        {
            string SQL = "SELECT  FirstName+' '+LastName AS FullUserName,UserName,FirstName,LastName,Pwd,PermissionLevel,MobileNumber,ID FROM SystemUser " +
                "WHERE FirstName LIKE '%'+@FirstName+'%' AND LastName LIKE '%'+@LastName+'%' AND UserName LIKE '%'+@UserName+'%'  AND  Deleted = 0";

            AddParameter("@FirstName", FirstName);
            AddParameter("@LastName", LastName);
            AddParameter("@UserName", UserName);

            return GetTable(SQL);

        }

        #region IDBFunctions Members


        public int Update()
        {
            throw new NotImplementedException();
        }

        #endregion

        public bool Login()
        {
            string SQL = "SELECT ID,FirstName,LastName,PermissionLevel,MobileNumber FROM SystemUser WHERE UserName = @UserName AND Pwd = @Pwd AND Deleted = 0";
            AddParameter("@UserName", UserName);
            AddParameter("@Pwd", Password);


            using (OleDbDataReader reader = ExecuteReader(SQL))
            {
                if (reader.Read())
                {
                    ID = reader.GetInt32(0);
                    FirstName = reader.GetString(1);
                    LastName = reader.GetString(2);
                    PermissionLevel = (UserLevel)reader.GetInt16(3);
                    return true;
                }
            }

            return false;
        }
    }
}
