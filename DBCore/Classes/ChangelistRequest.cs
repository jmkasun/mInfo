using DBCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace DBCore.Classes
{
    public class ChangelistRequest : DBBase, IDBFunctions
    {
        public int ID;
        public int ChangelistId;
        public int BhikkuId;
        public int Asapuwa1Id;
        public int Asapuwa2Id;
        public int Asapuwa3Id;

        public ChangelistRequest()
        {

        }

        public ChangelistRequest(bool withConn)
            : base(withConn)
        {

        }

        public int Add()
        {
            AddParameter("@P_changeListId", ChangelistId);
            AddParameter("@p_bhikkuId", BhikkuId);
            AddParameter("@p_asapuwa1", Asapuwa1Id);
            AddParameter("@p_asapuwa2", Asapuwa2Id);
            AddParameter("@p_asapuwa3", Asapuwa3Id);

            AddParameter("@p_ID", MySqlDbType.Int32);

            return ExecuteNonQuery("ChangeListRequest_Add");
        }

        public int Delete(int Id)
        {
            AddParameter("@p_ID", Id);

            return ExecuteNonQuery("ChangeListRequest_Delete");
        }

        public DataTable SelectAll()
        {
            throw new NotImplementedException();
        }


        public DataTable SelectAll(int changeListId)
        {
            AddParameter("@p_changeListId", changeListId);

            return GetTable("ChangeListRequest_Select");
        }

        public int Update()
        {
            throw new NotImplementedException();
        }

        public int Delete()
        {
            throw new NotImplementedException();
        }
    }
}
