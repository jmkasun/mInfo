using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using DBCore;
using System.Collections;
using System.Xml;
using System.Configuration;
using System.Security.Cryptography;

namespace MahamewnawaInfo.Common
{
    public delegate void CallbackAdd(int index);


    public class Utility
    {

        //// return appsetting from config file, according to given key
        public static string GetAppsetingData(string key)
        {

            return ConfigurationManager.AppSettings[key];

        }


        internal static void WriteWrrorLog(Exception ex)
        {
            try
            {
                string fileName = string.Concat(@"Logs\", DateTime.Now.ToString("yyyy-MMM"), ".csv");

                using (StreamWriter fileWriter = File.AppendText(fileName))
                {
                    fileWriter.WriteLine("\n#####################################");
                    fileWriter.WriteLine(string.Concat(DateTime.Now, "\n", ex.Message, "\n", ex.StackTrace, "\n\n"));
                    fileWriter.Flush();
                }
            }
            catch
            {

            }
        }




        public static void SetDatagridViewRow(DataGridView grid)
        {
            //set row number
            int rowCount = 1;
            foreach (DataGridViewRow row in grid.Rows)
            {
                row.HeaderCell.Value = (rowCount++).ToString();
            }
        }

        // get buddhist year
        public static DateTime GetBYDate(DateTime date)
        {
            if (date == new DateTime())
                return new DateTime();

            if (date.Month > 4)
                return date.AddYears(544);

            return date.AddYears(543);
        }

        // get crist year
        public static DateTime GetCYDate(DateTime date)
        {
            if (date == new DateTime())
                return new DateTime();

            if (date.Month > 4)
                return date.AddYears(-544);

            return date.AddYears(-543);
        }

        public static Color GetBhikkuLabelColor(BhikkuType bhikkuType)
        {
            switch (bhikkuType)
            {
                case BhikkuType.SangaUpasthayaka:
                    {
                        return Color.OrangeRed;
                    }
                case BhikkuType.AnusangaUpasthayaka:
                    {
                        return Color.Chocolate;
                    }
                case BhikkuType.Upasampada:
                    {
                        return Color.LightGreen;
                    }
                case BhikkuType.Samanera:
                    {
                        return Color.LightSkyBlue;
                    }
                case BhikkuType.all:
                    {
                        return Color.LightYellow;
                    }
                default:
                    {
                        return Color.Transparent;
                    }
            }
        }

        public static Image GetBhikkuLabelImage(BhikkuType bhikkuType)
        {
            switch (bhikkuType)
            {
                case BhikkuType.SangaUpasthayaka:
                    {
                        return global::MahamewnawaInfo.Properties.Resources.SANGHOPASTHAYAKA;
                    }
                case BhikkuType.AnusangaUpasthayaka:
                    {
                        return global::MahamewnawaInfo.Properties.Resources.ANU_SANGHOPASTHAYAKA;
                    }
                case BhikkuType.Upasampada:
                    {
                        return global::MahamewnawaInfo.Properties.Resources.Upasampada;
                    }
                default:
                    {
                        return global::MahamewnawaInfo.Properties.Resources.SAMANERA;
                    }
            }
        }

        public static List<Image> GetBhikkuLabelImageList(BhikkuType bhikkuType, BhikkuChangeType changeType, bool isDraged, bool hover)
        {
            List<Image> imgList = new List<Image>();

            if (isDraged)
            {
                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Btn_body_dis);
                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Btn_tail_dis);
            }
            else
            {
                if (!hover)
                {
                    imgList.Add(global::MahamewnawaInfo.Properties.Resources.Btn_body);
                    imgList.Add(global::MahamewnawaInfo.Properties.Resources.Btn_tail);
                }
            }

            switch (bhikkuType)
            {
                case BhikkuType.SangaUpasthayaka:
                    {
                        if (isDraged)
                        {
                            imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_dis, changeType));
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_tail_hov);
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_hov, changeType));
                            }
                            else
                            {
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_, changeType));
                            }
                        }

                        return imgList;
                    }
                case BhikkuType.AnusangaUpasthayaka:
                    {
                        if (isDraged)
                        {
                            imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_dis, changeType));
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_tail_hov);
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_hov, changeType));
                            }
                            else
                            {
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_, changeType));
                            }
                        }

                        return imgList;
                    }
                case BhikkuType.Upasampada:
                    {
                        if (isDraged)
                        {
                            imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_dis, changeType));
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_tail_hov);
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_hov, changeType));
                            }
                            else
                            {
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_, changeType));
                            }
                        }

                        return imgList;
                    }
                default:
                    {
                        if (isDraged)
                        {
                            imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_dis, changeType));
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_tail_hov);
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_hov, changeType));
                            }
                            else
                            {
                                imgList.Add(DrowOnImage(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_, changeType));

                            }
                        }

                        return imgList;
                    }
            }
        }

        public void GetDateDiff(DateTime fromDate, DateTime Todate)
        {

        }

        private static Bitmap DrowOnImage(Bitmap image, BhikkuChangeType changeType)
        {
            if (changeType == BhikkuChangeType.OnSuSwRequest || changeType == BhikkuChangeType.onRequest)
            {

                Pen blackPen = new Pen(changeType == BhikkuChangeType.OnSuSwRequest ? Color.Red : Color.Blue, 3);

                int x1 = image.Width - 5;
                int y1 = 0;
                int x2 = image.Width - 5;
                int y2 = image.Height - 2;
                // Draw line to screen.
                using (var graphics = Graphics.FromImage(image))
                {
                    graphics.DrawLine(blackPen, x1, y1, x2, y2);
                }
            }

            return image;
        }

        public static string GetPostString(DBCore.BhikkuPost post)
        {
            switch (post)
            {
                case DBCore.BhikkuPost.SangaUpasthayaka:
                    return "ස: උ:";
                case DBCore.BhikkuPost.AnusangaUpasthayaka:
                    return "අනු ස: උ:";
            }
            return "";
        }

        public static string GetPostStringLong(DBCore.BhikkuPost post)
        {
            switch (post)
            {
                case DBCore.BhikkuPost.SangaUpasthayaka:
                    return "(සංඝ උපස්ථායක ස්වාමින්වහන්සේ)";
                case DBCore.BhikkuPost.AnusangaUpasthayaka:
                    return "(අනු සංඝ උපස්ථායක ස්වාමින්වහන්සේ)";
            }
            return "";
        }


        public static void setBhikkuPictureFromByteArray(byte[] picData, PictureBox picBox)
        {
            try
            {

                // set byte array
                MemoryStream mem = new MemoryStream(picData);
                // bikkuImage = picData;

                Image img = Image.FromStream(mem);
                picBox.Image = DBCore.Utility.getThumbImage(img, picBox.Width, picBox.Height);
            }
            catch (Exception ex)
            {
                MessageView.ExceptionError(ex);
            }
        }

        public static Image GetImageFromBase64(string base64String)
        {
            try
            {
                // set byte array
                MemoryStream mem = new MemoryStream(Convert.FromBase64String(base64String));
                // bikkuImage = picData;

                return Image.FromStream(mem);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static string GetBhuddhistlaDate(DateTime date)
        {
            string sinhalaMonthName = "";
            switch (date.Month)
            {
                case 1:
                    sinhalaMonthName = "දුරුතු";
                    break;
                case 2:
                    sinhalaMonthName = "නවම්";
                    break;
                case 3:
                    sinhalaMonthName = "මැදින්";
                    break;
                case 4:
                    sinhalaMonthName = "බක්";
                    break;
                case 5:
                    sinhalaMonthName = "වෙසක්";
                    break;
                case 6:
                    sinhalaMonthName = "පොසොන්";
                    break;
                case 7:
                    sinhalaMonthName = "ඇසල";
                    break;
                case 8:
                    sinhalaMonthName = "නිකිණි";
                    break;
                case 9:
                    sinhalaMonthName = "බිනර";
                    break;
                case 10:
                    sinhalaMonthName = "වප්";
                    break;
                case 11:
                    sinhalaMonthName = "ඉල්";
                    break;
                case 12:
                    sinhalaMonthName = "උඳුවප්";
                    break;

            }

            return string.Concat(date.Year, " ", sinhalaMonthName, " මස  ", date.Day);
        }


        public static string GetSinhalaDate(DateTime date)
        {
            string sinhalaMonthName = "";
            switch (date.Month)
            {
                case 1:
                    sinhalaMonthName = "ජනවාරි";
                    break;
                case 2:
                    sinhalaMonthName = "පෙබරවාරි";
                    break;
                case 3:
                    sinhalaMonthName = "මාර්තු";
                    break;
                case 4:
                    sinhalaMonthName = "අප්‍රේල්";
                    break;
                case 5:
                    sinhalaMonthName = "මැයි";
                    break;
                case 6:
                    sinhalaMonthName = "ජූනි";
                    break;
                case 7:
                    sinhalaMonthName = "ජූලි";
                    break;
                case 8:
                    sinhalaMonthName = "අගෝස්තු";
                    break;
                case 9:
                    sinhalaMonthName = "සැප්තැම්බර්";
                    break;
                case 10:
                    sinhalaMonthName = "ඔක්තෝම්බර්";
                    break;
                case 11:
                    sinhalaMonthName = "නොවැම්බර්";
                    break;
                case 12:
                    sinhalaMonthName = "දෙසැම්බර්";
                    break;

            }

            return string.Concat(date.Year, " ", sinhalaMonthName, " මස ", date.Day);
        }


        public static Guid GetMD5HashGUID(string input)
        {
            if (input == null)
                input = string.Empty;

            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = x.ComputeHash(Encoding.UTF8.GetBytes(input));
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return new Guid(s.ToString());
        }

    }

    public class HistryDatagridSort : IComparer
    {

        public int Compare(object x, object y)
        {

            return DateTime.Parse(((DataGridViewRow)y).Cells[3].Value.ToString()).CompareTo(DateTime.Parse(((DataGridViewRow)x).Cells[3].Value.ToString()));
        }

    }
   
}
