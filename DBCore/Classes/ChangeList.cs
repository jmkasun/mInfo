using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using MySql.Data.MySqlClient;

namespace DBCore.Classes
{
    public class ChangeList : DBBase, IDBFunctions
    {
        public int ID;
        public DateTime FromDate;
        public DateTime Todate;
        public bool ForignCountry;
        public bool AddedHistry;

        public List<ChangeListBhikku> ChangelistBhikku;
        private bool p;

        public List<int> FinalizedAsapu;

        public ChangeList()
        {

        }

        public ChangeList(bool withConn)
            : base(withConn)
        {

        }



        #region IDBFunctions Members

        public int Add()
        {

            AddParameter("@p_FromDate", FromDate);
            AddParameter("@p_Todate", Todate);
            AddParameter("@p_ForignCountry", ForignCountry);

            AddParameter("@p_ID", MySqlDbType.Int32);

            ExecuteNonQueryOutput("ChangeList_Add");
            ID = (int)GetOutputValue("@p_ID");
            return ID;
        }

        public List<ChangeList> SelectAllList()
        {
            List<ChangeList> list = new List<ChangeList>();

            using (MySqlDataReader reader = ExecuteReader("ChangeList_Sel"))
            {
                while (reader.Read())
                {
                    ChangeList l = new ChangeList();

                    l.ID = reader.GetInt32(0);
                    l.FromDate = reader.GetDateTime(1);
                    l.Todate = reader.GetDateTime(2);
                    l.ForignCountry = reader.GetBoolean(3);
                    string finalizedAsapulist = reader.GetString(4);
                    l.AddedHistry = reader.GetBoolean(5);

                    l.FinalizedAsapu = new List<int>();

                    foreach (string id in finalizedAsapulist.Split(','))
                    {
                        int aspID = 0;
                        if (Int32.TryParse(id, out aspID))
                        {
                            l.FinalizedAsapu.Add(aspID);
                        }
                    }

                    list.Add(l);
                }
            }

            return list;
        }

        public List<ChangeListBhikku> SelectChangeList(int listID)
        {
            List<ChangeListBhikku> list = new List<ChangeListBhikku>();
            AddParameter("@p_ChangeListID", listID);

            using (MySqlDataReader reader = ExecuteReader("ChangeListBhikku_Sel"))
            {
                while (reader.Read())
                {
                    ChangeListBhikku lb = new ChangeListBhikku();

                    lb.ID = reader.GetInt32(0);
                    lb.BhikkuID = reader.GetInt32(1);
                    lb.AsapuwaID = reader.GetInt32(2);
                    lb.Post = (BhikkuPost)reader.GetInt32(3);

                    list.Add(lb);
                }
            }

            return list;
        }

        public System.Data.DataTable SelectAll()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            AddParameter("@p_ID", ID);
            return ExecuteNonQuery("ChangeList_Del");
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        #endregion

        public int AddBhikkuAsapuwa(int ChangeListID, int asapuwaID, int bhikkuID, BhikkuPost Post)
        {
            ClearParameters();

            AddParameter("@p_ChangeListID", ChangeListID);
            AddParameter("@p_BhikkuID", bhikkuID);
            AddParameter("@p_AsapuwaID", asapuwaID);
            AddParameter("@p_Post", (int)Post);

            AddParameter("@p_ID", MySqlDbType.Int32);

            ExecuteNonQueryOutput("ChangeListBhikku_Add");

            return (int)GetOutputValue("@p_ID");
        }

        public void UpdateBhikkuAsapuwa(int bhikkuChangeListID, BhikkuPost Post)
        {
            ClearParameters();

            AddParameter("@p_ID", bhikkuChangeListID);
            AddParameter("@p_Post", (int)Post);

            ExecuteNonQueryOutput("ChangeListBhikku_Upd");
        }

        public void DeleteBhikkuAsapuwa(int ID)
        {
            ClearParameters();

            AddParameter("@p_ID", ID);


            ExecuteNonQueryOutput("ChangeListBhikku_Del");
        }

        public void Clear(int ChangeListID)
        {
            AddParameter("@p_ChangeListID", ChangeListID);
            ExecuteNonQueryOutput("ChangeListBhikku_Clear");
        }

        public void UpdateFinalizedAsapuList(string asapuID, bool isAdd)
        {
            AddParameter("@p_ID", ID);
            AddParameter("@p_AsapuwaID", asapuID);
            AddParameter("@p_isAdd", isAdd);

            ExecuteNonQueryOutput("ChangeList_Upd_FinalizedAsapu");
        }


        public void SetAddedHistry()
        {
            AddParameter("@p_ID", ID);

            ExecuteNonQueryOutput("ChangeList_Upd_HistryAdded");
        }

        public void DeleteBhikkuHistry()
        {

            AddParameter("@p_ID", ID);
            ExecuteNonQueryOutput("ChangeList_DeleteHistry");
        }
    }

    public class ChangeListBhikku
    {
        public int ID;
        public int AsapuwaID;
        public int BhikkuID;
        public BhikkuPost Post;
    }
}
