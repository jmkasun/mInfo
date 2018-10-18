using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Windows.Forms;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBCore.Classes
{
    public class Asapuwa : DBBase, IDBFunctions
    {
        public int ID;
        public string AsapuwaName;
        public string Address;
        public int District;
        public string ContactNumber1;
        public string ContactNumber2;
        public int SangaUpasthayakahimi;
        public DateTime OpeningDate;
        public bool HeldUpasampada;
        public int Country;
        public string PostalCode;
        public int NumberOfKuti;
        public string AsapuwaNameKey;

        public Asapuwa()
        {

        }

        public Asapuwa(bool withConn)
            : base(withConn)
        {

        }


        #region IDBFunctions Members

        public int Add()
        {
            //string SQL = "INSERT INTO Asapuwa(AsapuwaName,Address,District,ContactNumber1,ContactNumber2,SangaUpasthayakahimi,OpeningDate,HeldUpasampada,Deleted) " +
            //          "VALUES(@AsapuwaName,@Address,@District,@ContactNumber1,@ContactNumber2,@SangaUpasthayakahimi,@OpeningDate,@HeldUpasampada,0)";


            AddParameter("@p_AsapuwaName", AsapuwaName);
            AddParameter("@p_Address", Address);
            AddParameter("@p_District", District);
            AddParameter("@p_ContactNumber1", ContactNumber1);
            AddParameter("@p_ContactNumber2", ContactNumber2);
            AddParameter("@p_SangaUpasthayakahimi", SangaUpasthayakahimi);
            AddParameter("@p_OpeningDate", OpeningDate);
            AddParameter("@p_HeldUpasampada", HeldUpasampada);
            AddParameter("@p_Country", Country);
            AddParameter("@p_PostalCode", PostalCode);
            AddParameter("@p_NumberOfKuti", NumberOfKuti);

            return ExecuteNonQuery("Asapuwa_Add");
        }

        public System.Data.DataTable SelectAll()
        {
            //string SQL = "SELECT AsapuwaName,ID FROM Asapuwa " +
            //    "WHERE Deleted = 0";


            return GetTable("Asapuwa_Sel");
        }


        public Dictionary<string, Asapuwa> SelectAllDictionary(ref int maxNameLength)
        {
            Dictionary<string, Asapuwa> list = new Dictionary<string, Asapuwa>();
            maxNameLength = 0;

            using (MySqlDataReader reader = ExecuteReader("Asapuwa_SelCountry"))
            {

                while (reader.Read())
                {
                    Asapuwa asp = new Asapuwa();
                    asp.AsapuwaName = reader.GetString(0);
                    asp.ID = reader.GetInt32(1);
                    asp.Country = reader.GetInt32(2);
                    asp.NumberOfKuti = reader.GetInt32(3);
                    asp.AsapuwaNameKey = reader.GetString(4);

                    list.Add(asp.AsapuwaNameKey, asp);


                    //testLabel.Text = asp.AsapuwaNameKey;
                    int l = asp.AsapuwaName.Length - asp.AsapuwaName.IndexOf("-");

                    if ( l > maxNameLength)
                    {
                        maxNameLength = l;
                    }

                }


            }

            return list;
        }


        public System.Data.DataTable SelectAllHeldUpasampada()
        {
            //string SQL = "SELECT AsapuwaName,ID FROM Asapuwa " +
            //    "WHERE HeldUpasampada = -1 AND Deleted = 0";


            return GetTable("Asapuwa_Sel_Upasampada");
        }

        public int Delete()
        {
            //  string SQL = "UPDATE Asapuwa SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Asapuwa_Del");
        }

        public int Update()
        {
            //string SQL = "UPDATE Asapuwa SET AsapuwaName = @AsapuwaName,Address = @Address,District = @District,ContactNumber1 = @ContactNumber1,ContactNumber2 = @ContactNumber2,SangaUpasthayakahimi = @SangaUpasthayakahimi,OpeningDate = @OpeningDate, HeldUpasampada = @HeldUpasampada " +
            //            "WHERE ID = @ID";


            AddParameter("@p_AsapuwaName", AsapuwaName);
            AddParameter("@p_Address", Address);
            AddParameter("@p_District", District);
            AddParameter("@p_ContactNumber1", ContactNumber1);
            AddParameter("@p_ContactNumber2", ContactNumber2);
            AddParameter("@p_SangaUpasthayakahimi", SangaUpasthayakahimi);
            AddParameter("@p_OpeningDate", OpeningDate);
            AddParameter("@p_HeldUpasampada", HeldUpasampada);
            AddParameter("@p_Country", Country);
            AddParameter("@p_PostalCode", PostalCode);
            AddParameter("@p_NumberOfKuti", NumberOfKuti);

            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Asapuwa_Upd");
        }

        #endregion

        public System.Data.DataTable SelectFind()
        {
            //string SQL = "SELECT AsapuwaName,Address,d.District,ContactNumber1,a.District AS DistrictID,ContactNumber2,SangaUpasthayakahimi,OpeningDate,HeldUpasampada,a.ID FROM Asapuwa a LEFT JOIN District d on a.District = d.ID " +
            //    "WHERE AsapuwaName LIKE '%"+AsapuwaName+"%'  AND  ("+District+" = 0 OR a.District = "+District+") AND (ContactNumber1 LIKE '%"+ContactNumber1+"%' OR ContactNumber2 LIKE '%"+ContactNumber1+"%') AND a.Deleted = 0";

            AddParameter("@p_AsapuwaName", AsapuwaName);
            AddParameter("@p_District", District);
            AddParameter("@p_ContactNumber", ContactNumber1);

            return GetTable("Asapuwa_find");

        }


        public void BindToCombo(ComboBox combo)
        {
            DataTable tbl = SelectAll();

            combo.DataSource = tbl;

            combo.DisplayMember = "AsapuwaName";
            combo.ValueMember = "ID";
        }

        public void BindToComboHeldUpasampada(ComboBox combo)
        {
            DataTable tbl = SelectAllHeldUpasampada();

            combo.DataSource = tbl;

            combo.DisplayMember = "AsapuwaName";
            combo.ValueMember = "ID";
        }

        //public string AsapuwaNameKey
        //{
        //    get
        //    {
        //        return AsapuwaName.Substring(AsapuwaName.IndexOf("-") + 1).Trim();
        //    }
        //}

        public List<AsapuwaHistryCurrentBhikku> SelectCurrentBhikkuList()
        {
            List<AsapuwaHistryCurrentBhikku> list = new List<AsapuwaHistryCurrentBhikku>();

            AddParameter("@p_AsapuwaID", ID);

            using (MySqlDataReader reader = ExecuteReader("AsapuHistry_Set_CurrentBhikku"))
            {
                while (reader.Read())
                {
                    list.Add(new AsapuwaHistryCurrentBhikku(reader.GetString(0), Utility.GetDateDiff(reader.GetDateTime(1), reader.GetDateTime(2)),BhikkuPost.NAN));
                }
            }

            return list;
        }

    }
}
