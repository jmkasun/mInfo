using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCore.Classes
{
    [Serializable]
   public class BhikkuAsapuHistry
    {
       public int ID;
       public int AsapuID;
       public DateTime FromDate;
       public DateTime ToDate;
       public string AsapuName;
       public BhikkuPost Post;
       public string Note;

       public string DateDiff
       {
           get
           {
               return Utility.GetDateDiff(FromDate, ToDate);
           }
       }

       public BhikkuAsapuHistry()
       {

       }

       public BhikkuAsapuHistry(int id, int asapuID, DateTime fromDate, DateTime toDate,string asapuName,BhikkuPost post,string note)
       {
           ID = id;
           AsapuID = asapuID;
           FromDate = fromDate;
           ToDate = toDate;
           AsapuName = asapuName;
           Post = post;
           Note = note;
       }
    }
}
