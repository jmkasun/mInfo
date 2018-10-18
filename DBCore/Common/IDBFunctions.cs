using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DBCore.Common
{

    public interface IDBFunctions
    {
        int Add();
        DataTable SelectAll();
        int Delete();
        int Update();

    }
}
