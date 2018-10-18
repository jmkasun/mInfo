using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCore.Classes
{
    [Serializable]
    public class Activity
    {
        public int ID;
        public string ActivityInfo;
       

        public Activity(int id, string act)
        {
            this.ID = id;
            this.ActivityInfo = act;
        }

    }
}
