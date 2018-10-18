using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Data;
using System.Windows.Forms;

namespace DBCore.Classes
{
    public class District : DBBase, IDBFunctions
    {
        public int ID;
        public string DistrictName;

        public District()
        {

        }

        public District(bool withConn)
            : base(withConn)
        {

        }

        #region IDBFunctions Members

        public int Add()
        {
            string SQL = "INSERT INTO District(District,Deleted) VALUES(@District,0)";
            AddParameter("@District", DistrictName);

            return ExecuteNonQuery(SQL);
        }

        public System.Data.DataTable SelectAll()
        {
            //string SQL = "SELECT ID,District FROM District ORDER BY District";
            return GetTable("District_SelAll");
        }

        public int Delete()
        {
            string SQL = "UPDATE District SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@ID", ID);

            return ExecuteNonQuery(SQL);
        }

        public int Update()
        {
            string SQL = "UPDATE District SET District = @District WHERE ID = @ID";

            AddParameter("@District", DistrictName);
            AddParameter("@ID", ID);

            return ExecuteNonQuery(SQL);
        }

        #endregion


        public void BindToCombo(ComboBox combo)
        {
            combo.DataSource = SelectAll();

            combo.DisplayMember = "District";
            combo.ValueMember = "ID";
        }

        public DataTable SelectFind()
        {
            string SQL = "SELECT District,ID FROM District WHERE District LIKE '%'+@District+'%' AND Deleted = 0 ORDER BY District";
            AddParameter("@District", DistrictName);

            return GetTable(SQL);
        }

    }
}
