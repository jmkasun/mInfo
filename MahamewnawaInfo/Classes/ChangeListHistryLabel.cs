using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCore.Classes;

namespace MahamewnawaInfo.Classes
{
    public class ChangeListHistryLabel:Label
    {
        public ChangeList ChangeList;

        public ChangeListHistryLabel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ChangeListHistryLabel
            // 
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.Size = new System.Drawing.Size(210, 20);
            this.ResumeLayout(false);

        }
    }
}
