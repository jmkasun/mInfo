using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MahamewnawaInfo.Forms.ChangeLst
{
    public class TabItemPanel : DevComponents.DotNetBar.PanelEx
    {
        public List<string> BhikkuList;


        public TabItemPanel()
        {
            BhikkuList = new List<string>();
        }

        public void ClearList()
        {
            for (int i = 0; i < this.Controls.Count; i++)
            {
                System.Windows.Forms.Control c = this.Controls[i];
                c = null;
            }

            this.Controls.Clear();
            GC.Collect();

        }

    }
}
