using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCore.Classes
{
    public class AsapuwaHistryCurrentBhikku
    {
        public string BhikkuName;
        public string Duration;
        public BhikkuPost Post;

        public AsapuwaHistryCurrentBhikku(string name, string dur, BhikkuPost pst)
        {
            BhikkuName = name;
            Duration = dur;
            Post = pst;
        }
    }
}
