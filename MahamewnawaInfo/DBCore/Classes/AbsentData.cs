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
    public class AbsentData : DBBase, IDBFunctions
    {
        public int ID;
        public string Name;
        public string FullName;
        public string NICNumber;
        public int Unit;
        public int Regt;
        public int Rank;
        public string RecNo;
        public string RegisterNo;

        public DateTime AbsentDate;
        public string NOK;
        public int Relationship;
        public string Address1;
        public string Address2;
        public int GSArea;
        public int AGADivision;
        public int NearestPolice;
        public int District;
        public int Creator;
        public DateTime CreationDate;

        public bool Returned;
        public DateTime ReturnedDate;

        public string ImageData;

        public AbsentData()
        {

        }

        public AbsentData(bool withConn)
            : base(withConn)
        {

        }

        #region IDBFunctions Members

        public int Add()
        {
            string SQL = "INSERT INTO AbsentData(NameWithInitials,FullName,NICNumber,Unit,Regt,Rank,RecNumber,RegisterNumber,AbsentDate,NOK,Relationship,Address1,Address2,GSArea,AGADivision,NearestPolice,District,Creator,CreationDate,Returned,ReturnedDate,ImageData,Deleted) " +
                         "VALUES(@NameWithInitials,@FullName,@NICNumber,@Unit,@Regt,@Rank,@RecNumber,@RegisterNumber,@AbsentDate,@NOK,@Relationship,@Address1,@Address2,@GSArea,@AGADivision,@NearestPolice,@District,@Creator,@CreationDate,@Returned,@ReturnedDate,@ImageData,0)";

            AddParameter("@NameWithInitials", Name);
            AddParameter("@FullName", FullName);
            AddParameter("@NICNumber", NICNumber);
            AddParameter("@Unit", Unit);
            AddParameter("@Regt", Regt);
            AddParameter("@Rank", Rank);
            AddParameter("@RecNumber", RecNo);
            AddParameter("@RegisterNumber", RegisterNo);

            AddParameter("@AbsentDate", AbsentDate);
            AddParameter("@NOK", NOK);
            AddParameter("@Relationship", Relationship);
            AddParameter("@Address1", Address1);
            AddParameter("@Address2", Address2);
            AddParameter("@GSArea", GSArea);
            AddParameter("@AGADivision", AGADivision);
            AddParameter("@NearestPolice", NearestPolice);
            AddParameter("@District", District);
            AddParameter("@Creator", Creator);
            AddParameter("@CreationDate", DateTime.Now.Date);

            AddParameter("@Returned", Returned);
            AddParameter("@ReturnedDate", ReturnedDate);

            AddParameter("@ImageData", ImageData);

            return ExecuteNonQuery(SQL);
        }

        public System.Data.DataTable SelectAll()
        {
            string SQL = "SELECT ID,NameWithInitials,FullName,NICNumber,Unit,Regt,Rank,RecNumber,RegisterNumber,AbsentDate,NOK,Relationship,Address1,Address2,GSArea,AGADivision,NearestPolice,District,Returned,ReturnedDate FROM AbsentData WHERE Deleted = 0 ORDER BY AGADivision";
            return GetTable(SQL);
        }

        public int Delete()
        {
            string SQL = "UPDATE AbsentData SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@ID", ID);

            return ExecuteNonQuery(SQL);
        }

        public int Update()
        {
            string SQL =    "UPDATE AbsentData SET NameWithInitials = @NameWithInitials,FullName =@FullName,NICNumber = @NICNumber,Unit = @Unit,Regt = @Regt, Rank = @Rank ,"+
                            "RecNumber = @RecNumber,RegisterNumber = @RegisterNumber,AbsentDate = @AbsentDate,NOK = @NOK ,Relationship = @Relationship,Address1 = @Address1,"+
                            "Address2 = @Address2,GSArea = @GSArea,AGADivision = @AGADivision,NearestPolice = @NearestPolice,District = @District,Returned = @Returned,"+
                            "ReturnedDate = @ReturnedDate,ImageData = @ImageData WHERE ID = @ID";

            AddParameter("@NameWithInitials", Name);
            AddParameter("@FullName", FullName);
            AddParameter("@NICNumber", NICNumber);
            AddParameter("@Unit", Unit);
            AddParameter("@Regt", Regt);
            AddParameter("@Rank", Rank);
            AddParameter("@RecNumber", RecNo);
            AddParameter("@RegisterNumber", RegisterNo);

            AddParameter("@AbsentDate", AbsentDate);
            AddParameter("@NOK", NOK);
            AddParameter("@Relationship", Relationship);
            AddParameter("@Address1", Address1);
            AddParameter("@Address2", Address2);
            AddParameter("@GSArea", GSArea);
            AddParameter("@AGADivision", AGADivision);
            AddParameter("@NearestPolice", NearestPolice);
            AddParameter("@District", District);

            AddParameter("@Returned", Returned);
            AddParameter("@ReturnedDate", ReturnedDate);

            AddParameter("@ImageData", ImageData);

            AddParameter("@ID", ID);

            return ExecuteNonQuery(SQL);
        }

        #endregion


        public void BindToCombo(ComboBox combo)
        {
            combo.DataSource = SelectAll();

            combo.DisplayMember = "AGADivision";
            combo.ValueMember = "ID";
        }

        public DataTable SelectFind()
        {
            string SQL = "SELECT AbsentData.NameWithInitials, District.District, Unit.Unit,Regt.Regt AS Regiment, Rank.Rank,AbsentData.ID " +
                            "FROM (((AbsentData LEFT JOIN Unit ON AbsentData.Unit = Unit.ID) LEFT JOIN Rank ON AbsentData.Rank = Rank.ID) LEFT JOIN District ON AbsentData.District = District.ID) LEFT JOIN Regt ON AbsentData.Regt = Regt.ID "+
                            "WHERE (NameWithInitials LIKE '%'+@NameWithInitials+'%' OR @NameWithInitials = '') AND (FullName LIKE '%'+@FullName+'%' OR @FullName = '') AND (NICNumber LIKE '%'+@NICNumber+'%' OR @NICNumber = '') AND " +
                            " (District.ID = @District OR @District = -1) AND (Unit.ID= @Unit OR @Unit = -1)  AND (Regt.ID = @Regt OR @Regt = -1) AND (Rank.ID = @Rank OR @Rank = -1) AND  (AbsentData.Returned = @Returned OR @Returned = 0 ) AND AbsentData.Deleted = 0 ORDER BY AbsentData.NameWithInitials";

            AddParameter("@NameWithInitials", Name);
            AddParameter("@FullName", FullName);
            AddParameter("@NICNumber", NICNumber);
            AddParameter("@District", District);
            AddParameter("@Unit", Unit);
            AddParameter("@Regt", Regt);
            AddParameter("@Rank", Rank);
            AddParameter("@Returned", Returned);

            return GetTable(SQL);
        }

        public void GetAbsentData(int Id)
        {
            string SQL = "SELECT NameWithInitials,FullName,NICNumber,Unit,Regt,Rank,RecNumber,"+
                         "RegisterNumber,AbsentDate,NOK,Relationship,Address1,Address2,GSArea,"+
                         "AGADivision,NearestPolice,District,Returned,ReturnedDate,"+
                         "ImageData FROM AbsentData WHERE ID = @ID";

            AddParameter("@ID", Id);

            using (OleDbDataReader reader = ExecuteReader(SQL))
            {
                if (reader.Read())
                {
                    Name = reader.GetString(0);
                    FullName = reader.GetString(1);
                    NICNumber = reader.GetString(2);
                    Unit = reader.GetInt32(3);
                    Regt = reader.GetInt32(4);
                    Rank = reader.GetInt32(5);

                    RecNo = reader.GetString(6);
                    RegisterNo = reader.GetString(7);
                    AbsentDate = reader.GetDateTime(8);
                    NOK = reader.GetString(9);
                    Relationship = reader.GetInt32(10);

                    Address1 = reader.GetString(11);
                    Address2 = reader.GetString(12);

                    GSArea = reader.GetInt32(13);
                    AGADivision = reader.GetInt32(14);
                    NearestPolice = reader.GetInt32(15);
                    District = reader.GetInt32(16);

                    Returned = reader.GetBoolean(17);
                    ReturnedDate = reader.GetDateTime(18);

                    ImageData = reader.GetString(19);
                }
            }

            //return ExecuteReader(SQL);
        }

        public bool IsValiedName()
        {
            string SQL = "SELECT ID  FROM AbsentData WHERE (NameWithInitials = @NameWithInitials OR FullName = @FullName) AND Deleted = 0";
            AddParameter("@NameWithInitials", Name);
            AddParameter("@FullName", FullName);

            return ExecuteScalar(SQL) == null;
        }

        public bool IsValiedNIC()
        {
            string SQL = "SELECT ID  FROM AbsentData WHERE NICNumber = @NICNumber AND Deleted = 0";
            AddParameter("@NICNumber", NICNumber);

            return ExecuteScalar(SQL) == null;
        }

        public bool IsValiedRegisterNumber()
        {
            string SQL = "SELECT ID  FROM AbsentData WHERE RegisterNumber = @RegisterNumber AND Deleted = 0";
            AddParameter("@RegisterNumber", RegisterNo);

            return ExecuteScalar(SQL) == null;
        }

        public bool Validate()
        {
            bool result = true;

            if (FullName.Length > 8 && !IsValiedName())
            {               
                result = false;
            }


            if (NICNumber.Length == 10 && !IsValiedNIC())
            {
                result = false;
            }

            if (RegisterNo.Length > 6 && !IsValiedRegisterNumber())
            {              
                result = false;
            }

            return result;
        }
    }
}
