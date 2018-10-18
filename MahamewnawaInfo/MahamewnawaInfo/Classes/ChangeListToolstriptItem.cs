using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;
using System.Drawing;

namespace MahamewnawaInfo.Classes
{
    class ChangeListToolstriptItem : ToolStripMenuItem
    {
        public Asapuwa asapuwa;
        public ChangeListItemAsapuwa asapuChangeListItem;
        public bool AddedToChangeList;

        public ChangeListToolstriptItem(string text):base(text)
        {

          
        }

        internal void Reset()
        {
            this.ForeColor = Color.Black;
            this.asapuChangeListItem = null;
            AddedToChangeList = false;
        }
    }
}
