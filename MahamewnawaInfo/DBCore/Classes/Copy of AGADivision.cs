using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore.Common;
using System.Data;
using System.Windows.Forms;

namespace DBCore.Classes
{
    class AGADivision : DBBase, IDBFunctions
    {
        int ID;
        string AGAdivision;

        public AGADivision()
        {

        }

        public AGADivision(bool withConn)
            : base(withConn)
        {

        }

        #region IDBFunctions Members

        public int Add()
        {
            string SQL = "INSERT INTO AGADivision(AGADivision,Deleted) VALUES(@AGADivision,0)";
            AddParameter("@Name", AGAdivision);

            return ExecuteNonQuesry(SQL);
        }

        public System.Data.DataTable SelectAll()
        {
            string SQL = "SELECT ID,AGADivision FROM AGADivision WHERE Deleted = 0 ORDER BY AGADivision";
            return GetTable(SQL);
        }

        public int Delete()
        {
            string SQL = "UPDATE AGADivision SET Deleted = 1 WHERE ID = @ID";
            AddParameter("@ID", ID);

            return ExecuteNonQuesry(SQL);
        }

        public int Update()
        {
            string SQL = "UPDATE AGADivision SET AGADivision = @AGADivision WHERE ID = @ID";

            AddParameter("@AGADivision", AGAdivision);
            AddParameter("@ID", ID);

            return ExecuteNonQuesry(SQL);
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
            string SQL = "SELECT AGADivision,ID FROM AGADivision WHERE CatName LIKE '%'+@AGADivision+'%' AND Deleted = 0 ORDER BY AGADivision";
            AddParameter("@AGADivision", AGAdivision);

            return GetTable(SQL);
        }

    }
}
