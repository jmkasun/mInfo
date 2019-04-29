using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;

namespace DBCore.Classes
{
    [Serializable]
    public class BikkuInfo : DBBase, IDBFunctions
    {

        public string NIC;
        public string SamaneraNumber;
        public string PassportNumber;
        public string PlaceOfBirth;
        public string LayNameInFull;
        public string NameOfFatherInFull;
        public string NameAssumedAtRobing;
        public string HomeTP;
        public string HomeAddress;
        public string ImageData;
        public string BloodGroup = string.Empty;
        public string OtheLanguage = string.Empty;
        public string UpasampadaNumber = string.Empty;
        public string CurrentStatusComment;
        public string HomeTP2;

        public DateTime DateOfBirth;
        public DateTime DateOfRobing;
        public DateTime DateOfCame;
        public DateTime DateOfHigherOrdination;
        public DateTime UpasampadaRegDate;

        public int ID;
        public int NameOfRobingTutor;
        public int TempleRobing;
        public int TempleOfResidence;
        public int TempleOfResidenceBeforeChange; //  keep whether templae has edited
        public int NameOfViharadhipathi;
        public int PlaceOfHigherOrdination;
        public int NameOfUpadyaAtHigherOrdination;
        public int District;

        public string KarmacharyaHimi1;
        public string KarmacharyaHimi2;
        public string KarmacharyaHimi3;

        public int MahaNayakaHimidetails;
        public int AcharyaHimiDetails;
        public int Nikaya;

        public int UpasampadaMahaNayakaHimidetails;
        public int UpasampadaAcharyaHimiDetails;
        public int UpadyaTheroName;
        public int UpasampadaNikaya;

        public int Country;
        public int OrderNumber;
        public int Number;
        public int SortListOrdeNumber;

        // Upasampada
        public bool IsUpasampanna;
        public bool IsUpadyaThero;
        public bool Dharmadeshanaa;
        public bool Vandanaa;
        public bool Sajjayana;
        public bool Sinhala;
        public bool Tamil;
        public bool English;
        public bool Hindhi;

        /// <summary>
        /// Tha post of bhikku
        /// </summary>
        public BhikkuPost Post;

        /// <summary>
        /// The type of change
        /// </summary>
        public BhikkuChangeType ChangeType;

        public List<Activity> Activities;
        public List<BhikkuAsapuHistry> AsapuHistry;
        public List<OtherData> OtherData;
        public List<UtilityData> OtherLanguages;

        public CurrenStatus CurrentStatus = CurrenStatus.Siti;

        public BikkuInfo()
        {

        }

        public BikkuInfo(bool withConn)
            : base(withConn)
        {

        }

        #region IDBFunctions Members

        public int Add(bool duplicateNumber)
        {

            if (duplicateNumber)
                ClearParameters();

            AddParameter("@p_NIC", NIC);
            AddParameter("@p_SamaneraNumber", SamaneraNumber);
            AddParameter("@p_PassportNumber", PassportNumber);
            AddParameter("@p_PlaceOfBirth", PlaceOfBirth);
            AddParameter("@p_LayNameInFull", LayNameInFull);
            AddParameter("@p_DateOfBirth", DateOfBirth);
            AddParameter("@p_NameOfFatherInFull", NameOfFatherInFull);
            AddParameter("@p_DateOfRobing", DateOfRobing);
            AddParameter("@p_NameAssumedAtRobing", NameAssumedAtRobing);
            AddParameter("@p_NameOfRobingTutor", NameOfRobingTutor);
            AddParameter("@p_TempleRobing", TempleRobing);
            AddParameter("@p_TempleOfResidence", TempleOfResidence);
            AddParameter("@p_NameOfViharadhipathi", NameOfViharadhipathi);
            AddParameter("@p_IsUpasampanna", IsUpasampanna);
            AddParameter("@p_PlaceOfHigherOrdination", PlaceOfHigherOrdination);
            AddParameter("@p_DateOfHigherOrdination", DateOfHigherOrdination);
            AddParameter("@p_NameOfUpadyaAtHigherOrdination", NameOfUpadyaAtHigherOrdination);
            AddParameter("@p_IsUpadyaThero", IsUpadyaThero);
            AddParameter("@p_Post", Post);
            AddParameter("@p_District", District);
            AddParameter("@p_DateOfCame", DateOfCame);
            AddParameter("@p_ImageData", ImageData);
            AddParameter("@p_HomeTP", HomeTP);
            AddParameter("@p_HomeAddress", HomeAddress);
            AddParameter("@p_BloodGroup", BloodGroup);

            AddParameter("@p_Dharmadeshanaa", Dharmadeshanaa);
            AddParameter("@p_Vandanaa", Vandanaa);
            AddParameter("@p_Sajjayana", Sajjayana);
            AddParameter("@p_Sinhala", Sinhala);
            AddParameter("@p_Tamil", Tamil);
            AddParameter("@p_English", English);
            AddParameter("@p_Hindhi", Hindhi);
            AddParameter("@p_OtheLanguage", OtheLanguage);
            AddParameter("@p_UpasampadaNumber", UpasampadaNumber);
            AddParameter("@p_UpasampadaRegDate", UpasampadaRegDate);
            AddParameter("@p_KarmacharyaHimi1", KarmacharyaHimi1);
            AddParameter("@p_KarmacharyaHimi2", KarmacharyaHimi2);
            AddParameter("@p_KarmacharyaHimi3", KarmacharyaHimi3);
            AddParameter("@p_MahaNayakaHimidetails", MahaNayakaHimidetails);
            AddParameter("@p_AcharyaHimiDetails", AcharyaHimiDetails);
            AddParameter("@p_UpadyaTheroName", UpadyaTheroName);
            AddParameter("@p_Nikaya", Nikaya);


            AddParameter("@P_UpasampadaMahaNayakaHimidetails", UpasampadaMahaNayakaHimidetails);
            AddParameter("@p_UpasampadaAcharyaHimiDetails", UpasampadaAcharyaHimiDetails);
            AddParameter("@p_UpasampadaNikaya", UpasampadaNikaya);

            AddParameter("@p_country", Country);
            AddParameter("@p_number", Number);
            AddParameter("@p_currentStatus", (int)CurrentStatus);
            AddParameter("@p_currentStatusComment", CurrentStatusComment);
            AddParameter("@p_duplicateNumber", duplicateNumber);
            AddParameter("@p_HomeTP2", HomeTP2);

            AddParameter("@p_ID", MySqlDbType.Int32);


            int ret = ExecuteNonQueryOutput("Bikku_Add");

            ID = (int)GetOutputValue("@p_ID");

            if (Activities != null)
            {
                ClearParameters();
                foreach (Activity act in Activities)
                {
                    AddActivity(act.ActivityInfo);
                }
            }

            if (AsapuHistry != null)
            {
                ClearParameters();
                foreach (BhikkuAsapuHistry his in AsapuHistry)
                {
                    AddAsapuHistry(his.AsapuID, his.FromDate, his.ToDate, his.Post, his.Note,0);
                }
            }

            if (OtherData != null)
            {
                ClearParameters();
                foreach (OtherData oth in OtherData)
                {
                    AddOtherData(oth.Description, oth.FileName, oth.Data);
                }
            }

            return ret;
        }



        public System.Data.DataTable SelectAll()
        {
            return GetTable("Bikku_SelAll");
        }

        public void BindToComboNameSeparate(ComboBox combo)
        {
            DataTable tbl = SelectAllNameSeparate();

            combo.DataSource = tbl;

            combo.DisplayMember = "NameAssumedAtRobing";
            combo.ValueMember = "ID";
        }

        public System.Data.DataTable SelectAllNameSeparate()
        {
            //string SQL = "SELECT  ID,NameAssumedAtRobing FROM BikkuInfo WHERE Deleted = 0";

            return GetTable("Bikku_SelAll_NameSeparate");
        }

        public System.Data.DataTable SelectAllUpadyaThero()
        {
            //string SQL = "SELECT  ID,NameAssumedAtRobing FROM BikkuInfo WHERE IsUpadyaThero = -1 AND Deleted = 0";

            return GetTable("Bikku_Sel_UpadyaThero");
        }

        public System.Data.DataTable SelectAllSangaUpsThero()
        {
            //string SQL = "SELECT  ID,NameAssumedAtRobing FROM BikkuInfo WHERE Post = -1 AND Deleted = 0";

            return GetTable("Bikku_Sel_SangaUpsThero");
        }

        public int Delete()
        {
            //string SQL = "UPDATE BikkuInfo SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Bikku_Del");
        }

        public int Update(bool duplicateNumber)
        {
            if (duplicateNumber)
                ClearParameters();

            AddParameter("@p_NIC", NIC);
            AddParameter("@p_SamaneraNumber", SamaneraNumber);
            AddParameter("@p_PassportNumber", PassportNumber);
            AddParameter("@p_PlaceOfBirth", PlaceOfBirth);
            AddParameter("@p_LayNameInFull", LayNameInFull);
            AddParameter("@p_DateOfBirth", DateOfBirth);
            AddParameter("@p_NameOfFatherInFull", NameOfFatherInFull);
            AddParameter("@p_DateOfRobing", DateOfRobing);
            AddParameter("@p_NameAssumedAtRobing", NameAssumedAtRobing);
            AddParameter("@p_NameOfRobingTutor", NameOfRobingTutor);
            AddParameter("@p_TempleRobing", TempleRobing);
            AddParameter("@p_TempleOfResidence", TempleOfResidence);
            AddParameter("@p_NameOfViharadhipathi", NameOfViharadhipathi);
            AddParameter("@p_IsUpasampanna", IsUpasampanna);
            AddParameter("@p_PlaceOfHigherOrdination", PlaceOfHigherOrdination);
            AddParameter("@p_DateOfHigherOrdination", DateOfHigherOrdination);
            AddParameter("@p_NameOfUpadyaAtHigherOrdination", NameOfUpadyaAtHigherOrdination);
            AddParameter("@p_IsUpadyaThero", IsUpadyaThero);
            AddParameter("@p_Post", Post);
            AddParameter("@p_District", District);
            AddParameter("@p_DateOfCame", DateOfCame);
            AddParameter("@p_ImageData", ImageData);
            AddParameter("@p_HomeTP", HomeTP);
            AddParameter("@p_HomeAddress", HomeAddress);
            AddParameter("@p_BloodGroup", BloodGroup);

            AddParameter("@p_Dharmadeshanaa", Dharmadeshanaa);
            AddParameter("@p_Vandanaa", Vandanaa);
            AddParameter("@p_Sajjayana", Sajjayana);
            AddParameter("@p_Sinhala", Sinhala);
            AddParameter("@p_Tamil", Tamil);
            AddParameter("@p_English", English);
            AddParameter("@p_Hindhi", Hindhi);
            AddParameter("@p_OtheLanguage", OtheLanguage);
            AddParameter("@p_UpasampadaNumber", UpasampadaNumber);
            AddParameter("@p_UpasampadaRegDate", UpasampadaRegDate);
            AddParameter("@p_KarmacharyaHimi1", KarmacharyaHimi1);
            AddParameter("@p_KarmacharyaHimi2", KarmacharyaHimi2);
            AddParameter("@p_KarmacharyaHimi3", KarmacharyaHimi3);
            AddParameter("@p_MahaNayakaHimidetails", MahaNayakaHimidetails);
            AddParameter("@p_AcharyaHimiDetails", AcharyaHimiDetails);
            AddParameter("@p_UpadyaTheroName", UpadyaTheroName);
            AddParameter("@p_Nikaya", Nikaya);

            AddParameter("@P_UpasampadaMahaNayakaHimidetails", UpasampadaMahaNayakaHimidetails);
            AddParameter("@p_UpasampadaAcharyaHimiDetails", UpasampadaAcharyaHimiDetails);
            AddParameter("@p_UpasampadaNikaya", UpasampadaNikaya);

            AddParameter("@p_country", Country);
            AddParameter("@p_number", Number);
            AddParameter("@p_currentStatus", (int)CurrentStatus);
            AddParameter("@p_currentStatusComment", CurrentStatusComment);
            AddParameter("@p_duplicateNumber", duplicateNumber);
            AddParameter("@p_HomeTP2", HomeTP2);

            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Bikku_Upd");
        }

        #endregion


        public void BindToComboViharadhipathi(ComboBox combo)
        {
            DataTable tbl = SelectAll();

            combo.DataSource = tbl;

            combo.DisplayMember = "FullUserName";
            combo.ValueMember = "ID";
        }

        public System.Data.DataTable SelectFind()
        {

            AddParameter("@p_NameAssumedAtRobing", NameAssumedAtRobing);
            AddParameter("@p_NIC", NIC);

            return GetTable("Bikku_Find");

        }

        public BikkuInfo SelectBhikku(int ID)
        {

            AddParameter("@p_ID", ID);

            using (MySqlDataReader reader = ExecuteReader("Bikku_Sel"))
            {


                if (reader.Read())
                {
                    SetFields(reader);

                }
            }

            Activities = GetActivities(ID);

            AsapuHistry = GetAsapuHistry(ID);

            OtherData = GetOtherData(ID);

            return this;
        }



        public BikkuInfo SelectBhikkuReport()
        {

            AddParameter("@p_ID", ID);

            using (MySqlDataReader reader = ExecuteReader("Bikku_SelReport"))
            {


                if (reader.Read())
                {

                    this.NameAssumedAtRobing = reader.GetString(0);
                    this.UpasampadaNumber       = reader.GetString(1);
                    this.SamaneraNumber         = reader.GetString(2);
                    this.NIC                    = reader.GetString(3);
                    this.DateOfBirth             = reader.GetDateTime(4);
                    this.DateOfRobing        = reader.GetDateTime(5);
                    this.HomeAddress         = reader.GetString(6);
                    this.HomeTP             = reader.GetString(7);
                    this.HomeTP2 = reader.GetString(8);
                    this.BloodGroup         = reader.GetString(9);
                    this.ImageData = reader.GetString(10);
                }
            }

            return this;

        }
        public BikkuInfo SelectBhikkuChangeList(int ID)
        {

            AddParameter("@p_ID", ID);

            using (MySqlDataReader reader = ExecuteReader("Bikku_Sel"))
            {
                if (reader.Read())
                {
                    SetFields(reader);
                }
            }

            AsapuHistry = GetAsapuHistry(ID);
            Activities = GetActivities(ID);
            return this;
        }

        public DataTable SelectBhikkuCustomReport(int ID)
        {

            AddParameter("@p_ID", ID);

            return GetTable("bikku_CustomReprort");
        }

        private void SetFields(MySqlDataReader reader)
        {
            NIC = reader.GetString(0);
            SamaneraNumber = reader.GetString(1);
            PassportNumber = reader.GetString(2);
            PlaceOfBirth = reader.GetString(3);
            LayNameInFull = reader.GetString(4);
            DateOfBirth = reader.GetDateTime(5);
            NameOfFatherInFull = reader.GetString(6);
            DateOfRobing = reader.GetDateTime(7);
            NameAssumedAtRobing = reader.GetString(8);
            NameOfRobingTutor = reader.GetInt32(9);
            TempleRobing = reader.GetInt32(10);
            TempleOfResidence = reader.GetInt32(11);
            NameOfViharadhipathi = reader.GetInt32(12);
            IsUpasampanna = reader.GetBoolean(13);
            PlaceOfHigherOrdination = reader.GetInt32(14);
            DateOfHigherOrdination = reader.GetDateTime(15);
            NameOfUpadyaAtHigherOrdination = reader.GetInt32(16);
            IsUpadyaThero = reader.GetBoolean(17);
            Post = (BhikkuPost)reader.GetInt32(18);
            District = reader.GetInt32(19);
            DateOfCame = reader.GetDateTime(20);
            ImageData = reader.IsDBNull(21) ? null : reader.GetString(21);
            BloodGroup = reader.GetString(22);

            Dharmadeshanaa = reader.GetBoolean(23);
            Vandanaa = reader.GetBoolean(24);
            Sajjayana = reader.GetBoolean(25);
            Sinhala = reader.GetBoolean(26);
            Tamil = reader.GetBoolean(27);
            English = reader.GetBoolean(28);
            Hindhi = reader.GetBoolean(29);

            OtheLanguage = reader.GetString(30); ;
            UpasampadaNumber = reader.GetString(31); ;
            UpasampadaRegDate = reader.GetDateTime(32);

            KarmacharyaHimi1 = reader.GetString(33);
            KarmacharyaHimi2 = reader.GetString(34);
            KarmacharyaHimi3 = reader.GetString(35);
            MahaNayakaHimidetails = reader.GetInt32(36);
            AcharyaHimiDetails = reader.GetInt32(37);
            UpadyaTheroName = reader.GetInt32(38);
            Nikaya = reader.GetInt32(39);

            HomeTP = reader.GetString(40);
            HomeAddress = reader.GetString(41);

            UpasampadaMahaNayakaHimidetails = reader.GetInt32(42);
            UpasampadaAcharyaHimiDetails = reader.GetInt32(43);
            UpasampadaNikaya = reader.GetInt32(44);

            Country = reader.GetInt32(45);
            OrderNumber = reader.GetInt32(46);
            Number = reader.GetInt32(47);
            CurrentStatus = (CurrenStatus)reader.GetInt32(48);
            CurrentStatusComment = reader.GetString(49);
            HomeTP2 = reader.GetString(50);

            using (UtilityData ud = new UtilityData(true))
            {
                OtherLanguages = ud.SelectByIds(OtheLanguage);

            }
        }


        #region IDBFunctions Members

        #endregion


        public void BindToCombo_KarmacharyaBhikkuName(ComboBox[] comboList)
        {
            using (MySqlDataReader reader = ExecuteReader("Bikku_SelKarmacharyaName"))
            {
                while (reader.Read())
                {
                    string name = reader.GetString(0);

                    foreach (var combo in comboList)
                    {
                        combo.Items.Add(name);
                    }
                }
            }
        }


        public void BindToComboUpadyaThero(ComboBox combo)
        {
            DataTable tbl = SelectAllUpadyaThero();

            combo.DataSource = tbl;

            combo.DisplayMember = "NameAssumedAtRobing";
            combo.ValueMember = "ID";
        }

        public void BindToComboSangaUpsThero(ComboBox combo)
        {
            DataTable tbl = SelectAllSangaUpsThero();

            combo.DataSource = tbl;

            combo.DisplayMember = "NameAssumedAtRobing";
            combo.ValueMember = "ID";
        }

        //public int GetScopeIdentity()
        //{
        //    string SQL = "SELECT @@IDENTITY FROM BikkuInfo";

        //    return (int)ExecuteScalar(SQL);
        //}

        // activity
        public void AddActivity(string activity)
        {
            //string SQL = "INSERT INTO Activity(BhikkuID,BhikkuActivity,Deleted) VALUES(@BhikkuID,p_BhikkuActivity,0)";

            AddParameter("@p_BhikkuID", ID);
            AddParameter("@p_BhikkuActivity", activity);

            ExecuteNonQuery("Activity_Add");
        }


        public List<Activity> GetActivities(int id)
        {
            List<Activity> act = new List<Activity>();

            //string SQL = "SELECT ID,BhikkuActivity FROM Activity WHERE BhikkuID = @BhikkuID AND Deleted = 0";

            AddParameter("@p_BhikkuID", id);

            using (MySqlDataReader reader = ExecuteReader("Activity_Sel"))
            {
                while (reader.Read())
                {
                    act.Add(new Activity(reader.GetInt32(0), reader.GetString(1)));
                }
            }

            return act;
        }

        public int RemoveActivity(int id)
        {
            //string SQL = "UPDATE Activity SET Deleted = 1 WHERE ID = @ID";

            AddParameter("@p_ID", id);

            return ExecuteNonQuery("Activity_Del");
        }



        //Asapu histry
        public void AddAsapuHistry(int asapuID, DateTime fromDate, DateTime toDate, BhikkuPost _post, string note,int ChangeListID)
        {
            //string SQL = "INSERT INTO BhikkuAsapu(BhikkuID,AsapuID,FromDate,ToDate,Deleted) VALUES(@BhikkuID,p_AsapuID,p_FromDate,p_ToDate,0)";

            AddParameter("@p_BhikkuID", ID);
            AddParameter("@p_AsapuID", asapuID);
            AddParameter("@p_FromDate", fromDate);
            AddParameter("@p_ToDate", toDate);
            AddParameter("@p_Post", (int)_post);
            AddParameter("@p_Note", note);
            AddParameter("@p_ChangeListID", ChangeListID);

            ExecuteNonQuery("AsapuHistry_Add");

            UpdatePost(ID, _post, fromDate, toDate);
        }

        public void UpdateAsapuHistry(int hisID, int asapuID, DateTime fromDate, DateTime toDate, BhikkuPost _post, string note)
        {
            //string SQL = "INSERT INTO BhikkuAsapu(BhikkuID,AsapuID,FromDate,ToDate,Deleted) VALUES(@BhikkuID,p_AsapuID,p_FromDate,p_ToDate,0)";

            AddParameter("@p_ID", hisID);
            AddParameter("@p_BhikkuID", ID);
            AddParameter("@p_AsapuID", asapuID);
            AddParameter("@p_FromDate", fromDate);
            AddParameter("@p_ToDate", toDate);
            AddParameter("@p_Post", (int)_post);
            AddParameter("@p_Note", note);

            ExecuteNonQuery("AsapuHistry_Upd");

            UpdatePost(ID, _post, fromDate, toDate);
        }


        public List<BhikkuAsapuHistry> GetAsapuHistry(int id)
        {
            List<BhikkuAsapuHistry> histry = new List<BhikkuAsapuHistry>();

            //string SQL = "SELECT b.ID,b.AsapuID,b.FromDate,b.ToDate,a.AsapuwaName FROM BhikkuAsapu b LEFT JOIN Asapuwa a ON  b.AsapuID = a.ID WHERE b.BhikkuID = @BhikkuID AND b.Deleted = 0";

            AddParameter("@p_BhikkuID", id);

            using (MySqlDataReader reader = ExecuteReader("AsapuHistry_Sel"))
            {
                while (reader.Read())
                {
                    histry.Add(new BhikkuAsapuHistry(reader.GetInt32(0), reader.GetInt32(1), reader.GetDateTime(2), reader.GetDateTime(3), reader.GetString(4), (BhikkuPost)reader.GetInt32(5), reader.GetString(6)));
                }
            }

            return histry;
        }

        public int RemoveAsapuHistry(int id)
        {
            //string SQL = "UPDATE BhikkuAsapu SET Deleted = 1 WHERE ID = @ID";

            AddParameter("@p_ID", id);

            return ExecuteNonQuery("AsapuHistry_del");
        }

        // Other Data
        public int AddOtherData(string description, string fileName, byte[] data)
        {
            AddParameter("@p_BhikkuID", ID);
            AddParameter("@p_Description", description);
            AddParameter("@p_FileName", fileName);
            AddParameter("@p_Data", data);

            return ExecuteNonQuery("OtherData_Add");
        }

        public List<OtherData> GetOtherData(int id)
        {
            List<OtherData> histry = new List<OtherData>();

            //string SQL = "SELECT b.ID,b.AsapuID,b.FromDate,b.ToDate,a.AsapuwaName FROM BhikkuAsapu b LEFT JOIN Asapuwa a ON  b.AsapuID = a.ID WHERE b.BhikkuID = @BhikkuID AND b.Deleted = 0";

            AddParameter("@p_BhikkuID", id);

            using (MySqlDataReader reader = ExecuteReader("OtherData_Sel"))
            {
                while (reader.Read())
                {
                    histry.Add(new OtherData(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), null));
                }
            }

            return histry;
        }

        public int RemoveOtherData(int id)
        {
            //string SQL = "UPDATE BhikkuAsapu SET Deleted = 1 WHERE ID = @ID";

            AddParameter("@p_ID", id);

            return ExecuteNonQuery("OtherData_del");
        }

        public byte[] GetOtherDatafile(int id)
        {
            AddParameter("@p_ID", id);

            return (byte[])ExecuteScalar("OtherData_SelFile");
        }

        public bool GetNextNumber()
        {
            //AddParameter("@p_ID", id);

            using (MySqlDataReader reader = ExecuteReader("Bikku_Sel_Number"))
            {
                if (reader.Read())
                {
                    OrderNumber = reader.GetInt32(0);
                    if (reader.IsDBNull(1))
                    {
                        Number = 1;
                    }
                    else
                    {
                        Number = reader.GetInt32(1);
                    }
                }
            }

            return true;
        }

        public int ValidateIndexNICNumbers(string nic, string index, int id)
        {

            AddParameter("@p_NIC", nic);
            AddParameter("@p_SamaneraNumber", index);

            bool dupNIC = false, dupIndex = false;

            using (MySqlDataReader reader = ExecuteReader("Validate_IndexNICNumber"))
            {
                while (reader.Read())
                {
                    if (id == reader.GetInt32(2))
                        continue;

                    if (!dupNIC && nic == reader.GetString(0))
                    {
                        dupNIC = true;
                    }

                    if (!dupIndex && index == reader.GetString(1))
                    {
                        dupIndex = true;
                    }
                }
            }

            if (dupIndex & dupNIC)
                return 4;

            if (dupIndex)
                return 3;

            if (dupNIC)
                return 2;

            return 1;
        }

        public Dictionary<string, BikkuInfo> SelectAllDictionary(ref int maxNamelength)
        {
            Dictionary<string, BikkuInfo> list = new Dictionary<string, BikkuInfo>();
            maxNamelength = 0;


            AddParameter("@p_CurrentStatus", (int)CurrenStatus.Siti);
            command.CommandTimeout = 120;

            using (MySqlDataReader reader = ExecuteReader("Bikku_SelAllDictionary"))
            {
                while (reader.Read())
                {
                    BikkuInfo bInfo = new BikkuInfo();
                    bInfo.NameAssumedAtRobing = reader.GetString(1);
                    bInfo.ID = reader.GetInt32(0);
                    bInfo.IsUpasampanna = reader.GetBoolean(2);
                    bInfo.Post = (BhikkuPost)reader.GetInt32(3);
                    bInfo.Number = reader.GetInt32(4);
                    bInfo.SortListOrdeNumber = list.Count + 1;

                    string imagePath = "images/" + bInfo.ID + ".jpg";

                    if (File.Exists(imagePath))
                    {
                        bInfo.ImageData = Utility.Get64StringFromImage(Image.FromFile(imagePath));
                    }
                    else
                    {


                        try
                        {
                            bInfo.ImageData = reader.GetString(5);

                            if (!string.IsNullOrEmpty(bInfo.ImageData))
                            {
                                using (Image img = Utility.GetImageFromString(bInfo.ImageData))
                                {
                                    img.Save(imagePath);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.LogError(ex, "public Dictionary<string, BikkuInfo> SelectAllDictionary(ref int maxNamelength).save image  ID: " + bInfo.ID + "  ImageData.Length: " + bInfo.ImageData.Length);
                        }
                    }

                    list.Add(bInfo.NameAssumedAtRobing, bInfo);

                    if (bInfo.NameAssumedAtRobing.Length > maxNamelength)
                    {
                        maxNamelength = bInfo.NameAssumedAtRobing.Length;
                    }


                }
            }

            return list;
        }


        public List<BikkuInfo> SelectAllList(CurrenStatus currentStatus)
        {
            List<BikkuInfo> list = new List<BikkuInfo>();

            AddParameter("@p_CurrentStatus", (int)currentStatus);

            using (MySqlDataReader reader = ExecuteReader("Bikku_SelAllDictionary"))
            {
                while (reader.Read())
                {
                    BikkuInfo bInfo = new BikkuInfo();
                    bInfo.NameAssumedAtRobing = reader.GetString(1);
                    bInfo.ID = reader.GetInt32(0);
                    bInfo.IsUpasampanna = reader.GetBoolean(2);
                    bInfo.Post = (BhikkuPost)reader.GetInt32(3);
                    bInfo.Number = reader.GetInt32(4);

                    bInfo.SortListOrdeNumber = list.Count + 1;
                    bInfo.UpasampadaNumber = reader.GetString(6);
                    bInfo.SamaneraNumber = reader.GetString(7);
                    bInfo.NIC = reader.GetString(8);
                    bInfo.DateOfRobing = reader.GetDateTime(9);

                    list.Add(bInfo);

                }
            }

            return list;
        }


        public List<BikkuInfo> SelectAllImage(CurrenStatus currentStatus)
        {
            List<BikkuInfo> list = new List<BikkuInfo>();
            AddParameter("@p_CurrentStatus", (int)currentStatus);

            using (MySqlDataReader reader = ExecuteReader("Bikku_SelAllImage"))
            {
                while (reader.Read())
                {
                    BikkuInfo bInfo = new BikkuInfo();
                    bInfo.NameAssumedAtRobing = reader.GetString(0);
                    bInfo.SortListOrdeNumber = list.Count + 1;
                    bInfo.ImageData = reader.GetString(1); 
                   
                    list.Add(bInfo);

                }
            }

            return list;
        }


        public List<BikkuInfo> SelectAllListWithAsapuwa()
        {
            List<BikkuInfo> list = new List<BikkuInfo>();

            

            using (MySqlDataReader reader = ExecuteReader("Bikku_SlAllwithAsapuwa"))
            {
                while (reader.Read())
                {
                    BikkuInfo bInfo = new BikkuInfo();
                    bInfo.NameAssumedAtRobing = reader.GetString(1);
                    bInfo.ID = reader.GetInt32(0);
                    bInfo.IsUpasampanna = reader.GetBoolean(2);          
                    bInfo.Number = reader.GetInt32(3);
                    bInfo.SortListOrdeNumber = list.Count + 1;
                    bInfo.UpasampadaNumber = reader.GetString(4); // this is asapuwa name
                    bInfo.SamaneraNumber = reader.GetString(5); // this is image

                    list.Add(bInfo);

                }
            }

            return list;
        }


        public List<BikkuInfo> SelectAllSanghaUpasthayaka()
        {
            List<BikkuInfo> list = new List<BikkuInfo>();



            using (MySqlDataReader reader = ExecuteReader("Bikku_SlSanghaUpasthayaka"))
            {
                while (reader.Read())
                {
                    BikkuInfo bInfo = new BikkuInfo();
                    bInfo.NameAssumedAtRobing = reader.GetString(0);               
                    bInfo.UpasampadaNumber = reader.GetString(1); // this is asapuwa name


                    list.Add(bInfo);

                }
            }

            return list;
        }

        public void UpdatePost(int bhikkuID, BhikkuPost _post, DateTime fromDate, DateTime toDate)
        {
            if (DateTime.Now < toDate)
            {
                AddParameter("@p_ID", bhikkuID);
                AddParameter("@p_Post", (int)_post);

                ExecuteNonQuery("Bikku_UpdPost");
            }
        }

        public BhikkuType BhikkuType
        {
            get
            {
                if (Post == BhikkuPost.SangaUpasthayaka)
                    return DBCore.BhikkuType.SangaUpasthayaka;

                if (Post == BhikkuPost.AnusangaUpasthayaka)
                    return DBCore.BhikkuType.AnusangaUpasthayaka;

                if (IsUpasampanna)
                    return DBCore.BhikkuType.Upasampada;
                else
                    return DBCore.BhikkuType.Samanera;
            }
        }

        #region IDBFunctions Members

        public int Add()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IDBFunctions Members


        public int Update()
        {
            throw new NotImplementedException();
        }

        #endregion

        public void SelectBhikkuFromNumber(int _number)
        {
            AddParameter("@p_Number", _number);

            using (MySqlDataReader reader = ExecuteReader("Bikku_SelFromNumber"))
            {


                if (reader.Read())
                {
                    SetFields(reader);
                    ID = reader.GetInt32(51);
                    Number = _number;
                }
            }

            Activities = GetActivities(ID);

            AsapuHistry = GetAsapuHistry(ID);

            OtherData = GetOtherData(ID);
        }

        public bool IsIdenticle(BikkuInfo binfo)
        {



            if(NIC == binfo.NIC)
             if(SamaneraNumber == binfo.SamaneraNumber)
             if(PassportNumber == binfo.PassportNumber)
             if(PlaceOfBirth == binfo.PlaceOfBirth)
             if(LayNameInFull == binfo.LayNameInFull)
             if( NameOfFatherInFull == binfo.NameOfFatherInFull)
             if( NameAssumedAtRobing== binfo.NameAssumedAtRobing)
             if(HomeTP == binfo.HomeTP)
             if(HomeAddress == binfo.HomeAddress)
             if(ImageData == binfo.ImageData)
             if( BloodGroup== binfo.BloodGroup)
             if(OtheLanguage == binfo.OtheLanguage)
             if( UpasampadaNumber== binfo.UpasampadaNumber)
             if( CurrentStatusComment== binfo.CurrentStatusComment)

             if( DateOfBirth== binfo.DateOfBirth)
             if( DateOfRobing== binfo.DateOfRobing)
             if( DateOfCame== binfo.DateOfCame)
             if(DateOfHigherOrdination == binfo.DateOfHigherOrdination)
             if( UpasampadaRegDate== binfo.UpasampadaRegDate)


             if( NameOfRobingTutor== binfo.NameOfRobingTutor)
             if( TempleRobing== binfo.TempleRobing)
             if( TempleOfResidence== binfo.TempleOfResidence)
             if(PlaceOfHigherOrdination == binfo.PlaceOfHigherOrdination)
             if(NameOfUpadyaAtHigherOrdination == binfo.NameOfUpadyaAtHigherOrdination)
             if( District== binfo.District)
             if( KarmacharyaHimi1== binfo.KarmacharyaHimi1)
             if( KarmacharyaHimi2== binfo.KarmacharyaHimi2)
             if( KarmacharyaHimi3== binfo.KarmacharyaHimi3)
             if( MahaNayakaHimidetails== binfo.MahaNayakaHimidetails)
             if( AcharyaHimiDetails== binfo.AcharyaHimiDetails)
             if(Nikaya == binfo.Nikaya)
             if( UpasampadaMahaNayakaHimidetails== binfo.UpasampadaMahaNayakaHimidetails)
             if( UpasampadaAcharyaHimiDetails== binfo.UpasampadaAcharyaHimiDetails)
             if( UpadyaTheroName== binfo.UpadyaTheroName)
             if( UpasampadaNikaya== binfo.UpasampadaNikaya)
             if(Country == binfo.Country)
             if(Number == binfo.Number)

             if( IsUpasampanna== binfo.IsUpasampanna)
             if( IsUpadyaThero== binfo.IsUpadyaThero)
             if( Dharmadeshanaa== binfo.Dharmadeshanaa)
             if(Vandanaa == binfo.Vandanaa)
             if(Sajjayana == binfo.Sajjayana)
             if( Sinhala== binfo.Sinhala)
             if(Tamil == binfo.Tamil)
             if( English== binfo.English)
             if( Hindhi== binfo.Hindhi)
             if (CurrentStatus == binfo.CurrentStatus)
             if (HomeTP == binfo.HomeTP) 
                 return true;


            return false;
        }
    }
}
