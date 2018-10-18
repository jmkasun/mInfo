using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace DBCore
{
    public enum DBResult { Add, Update, Delete, Exception }

    public enum AppSetting { BgImage }
    public enum UserLevel { SystemAdmin = 1, SystemUser = 2 };

    public enum UtilityDataName { OtherLang = 1, MahanayakaHimi = 2, acharyaHimi = 3, Nikaya = 4, UpadyaHimi = 5, placeRobing = 6, PlaceUpasampada = 7, Country = 8 };

    public enum CurrenStatus { Siti = 1, OtherPlace = 2, Upavidi = 3 };

    public enum BhikkuType { SangaUpasthayaka, AnusangaUpasthayaka, Upasampada, Samanera };

    public enum BhikkuPost {NAN = 0, SangaUpasthayaka = 1, AnusangaUpasthayaka = 2 };

    public class Utility
    {
        public static string GetConnectionString()
        {
            return string.Concat(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Data.mdb;Persist Security Info=True;Jet OLEDB:Database Password=";

        }

        public static string GetAppsetting(AppSetting key)
        {
            return ConfigurationManager.AppSettings[key.ToString()];
        }

        public static string GetDateDiff(DateTime d1, DateTime d2)
        {
            TimeSpan ts = d2.Subtract(d1);
            int years = 0;
            int months = 0;
            int days = 0;

            while (ts.TotalDays > 364)
            {
                d1 = d1.AddYears(1);
                ts = d2.Subtract(d1);
                years++;
            }

            while (ts.TotalDays > 30)
            {
                d1 = d1.AddMonths(1);
                ts = d2.Subtract(d1);
                months++;
            }

            while (ts.TotalDays > 0)
            {
                d1 = d1.AddDays(1);
                ts = d2.Subtract(d1);
                days++;
            }

            string dateDiff = "";

            if (years > 0)
                dateDiff = string.Concat("අවු:", years, "  ");

            if (months > 0)
                dateDiff = string.Concat(dateDiff,"මාස:", months, "  ");

            if (days > 0)
                dateDiff = string.Concat(dateDiff,"දින:", days, "  ");

            return dateDiff;
        }
    }
}

