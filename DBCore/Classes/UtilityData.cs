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
    public class UtilityData : DBBase, IDBFunctions
    {
        public int ID;
        public int NameID;
        public string Value;

        public UtilityData()
        {

        }

        public UtilityData(bool withConn)
            : base(withConn)
        {

        }


        #region IDBFunctions Members

        public int Add()
        {

            AddParameter("@p_nameID", NameID);
            AddParameter("@p_value", Value);

            return ExecuteNonQuery("Util_Add");
        }

        public System.Data.DataTable SelectAll(int _nameID)
        {
            AddParameter("@p_NameID", _nameID);

            return GetTable("Util_Sel");
        }


        public List<UtilityData> SelectByIds(string  _idList)
        {
            List<UtilityData> list = new List<UtilityData>();

            AddParameter("@p_IDList", _idList);

            using (MySqlDataReader reader = ExecuteReader("Util_SelByIds"))
            {
                while (reader.Read())
                {
                    UtilityData utl = new UtilityData();
                    utl.ID = reader.GetInt32(0);
                    utl.NameID = reader.GetInt32(1);
                    utl.Value = reader.GetString(2);

                    list.Add(utl);
                }
            }

            return list;
        }


        public int Delete()
        {
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Util_Del");
        }

        public int Update()
        {

            AddParameter("@p_value", Value);
            AddParameter("@p_ID", ID);

            return ExecuteNonQuery("Util_Upd");
        }

        #endregion

        public System.Data.DataTable SelectFind()
        {
            AddParameter("@p_NameID", NameID);
            AddParameter("@p_value", Value);

            return GetTable("Util_Find");
        }


        public void BindToCombo(ComboBox combo, DBCore.UtilityDataName type)
        {
            DataTable tbl = SelectAll((int)type);

            combo.DataSource = tbl;

            combo.DisplayMember = "value";
            combo.ValueMember = "ID";
            combo.SelectedIndex = -1;
        }

        #region IDBFunctions Members


        public DataTable SelectAll()
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    public struct OtherLang
    {

        public int ID;
        public string Name;
        public Label label;

        public OtherLang(int _id, string _name,Label _label)
        {
            ID = _id;
            Name = _name;
            label = _label;
        }
    }
}
