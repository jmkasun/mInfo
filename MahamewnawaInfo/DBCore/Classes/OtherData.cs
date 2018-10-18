using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBCore.Classes
{
    [Serializable]
    public class OtherData
    {
        public int ID;
        public string Description;
        public string FileName;
        public byte[] Data;


        public OtherData(int id, string description, string fileName, byte[] data)
        {
            ID = id;
            Description = description;
            FileName = fileName;
            Data = data;
        }
    }
}
