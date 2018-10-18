using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Compression;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using DBCore;
using System.Collections;

namespace MahamewnawaInfo.Common
{
    public delegate void CallbackAdd(int index);


    public class Utility
    {
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


        public static string Get64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static byte[] GetByte64String(string data)
        {
            return Convert.FromBase64String(data);
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

        public static byte[] Serialize(object obj)
        {
            IFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, obj);
                obj = null;
                GC.Collect();

                stream.Seek(0, SeekOrigin.Begin);

                byte[] data = new byte[stream.Length];

                stream.Read(data, 0, data.Length);

                return data;
            }

        }



        /// <summary>
        /// Deserialize the given byte array
        /// </summary>
        public static object Deserialize(byte[] bytesBuffer)
        {
            IFormatter formatter = new BinaryFormatter();

            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytesBuffer, 0, bytesBuffer.Length);
                stream.Seek(0, SeekOrigin.Begin);
                GC.Collect();
                object obj = formatter.Deserialize(stream);

                return obj;
            }
        }

        public static byte[] CompressGZip(byte[] bytesbuffer)
        {
            using (MemoryStream gz_ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(gz_ms, System.Text.Encoding.ASCII))
                {
                    writer.Write(bytesbuffer.Length);
                    using (GZipStream gz_mgzip = new GZipStream(gz_ms, CompressionMode.Compress, true))
                    {
                        gz_mgzip.Write(bytesbuffer, 0, bytesbuffer.Length);

                    }

                    byte[] data = new byte[gz_ms.Length];
                    gz_ms.Seek(0, SeekOrigin.Begin);
                    gz_ms.Read(data, 0, data.Length);
                    return data;

                }
            }
        }


        //decompress given byteArray
        public static byte[] DecompressGZip(byte[] obj)
        {
            using (MemoryStream gzipUncompress = new MemoryStream())
            {
                gzipUncompress.Write(obj, 0, obj.Length);
                gzipUncompress.Seek(0, SeekOrigin.Begin);

                using (BinaryReader reader = new BinaryReader(gzipUncompress))
                {
                    Int32 size = reader.ReadInt32();
                    byte[] buffer = new byte[size];

                    using (GZipStream gzipDecompress = new GZipStream(gzipUncompress, CompressionMode.Decompress))
                    {
                        int count = gzipDecompress.Read(buffer, 0, buffer.Length);

                        return buffer;
                    }
                }
            }
        }


        internal static byte[] GetFileByteCompress(string fileName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);

            return CompressGZip(fileData);
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
                default:
                    {
                        return Color.LightSkyBlue;
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

        public static List<Image> GetBhikkuLabelImageList(BhikkuType bhikkuType, bool isDraged, bool hover)
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
                            imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_dis);
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_tail_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_hov);
                            }
                            else
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Sang_Btn_head_);
                            }
                        }

                        return imgList;
                    }
                case BhikkuType.AnusangaUpasthayaka:
                    {
                        if (isDraged)
                        {
                            imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_dis);
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_tail_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_hov);
                            }
                            else
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Anu_Sang_Btn_head_);
                            }
                        }

                        return imgList;
                    }
                case BhikkuType.Upasampada:
                    {
                        if (isDraged)
                        {
                            imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_dis);
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_tail_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_hov);
                            }
                            else
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Upasampada_Btn_head_);
                            }
                        }

                        return imgList;
                    }
                default:
                    {
                        if (isDraged)
                        {
                            imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_dis);
                        }
                        else
                        {
                            if (hover)
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_body_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_tail_hov);
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_hov);

                            }
                            else
                            {
                                imgList.Add(global::MahamewnawaInfo.Properties.Resources.Samanera_Btn_head_);
                            }
                        }

                        return imgList;
                    }
            }
        }

        public void GetDateDiff(DateTime fromDate, DateTime Todate)
        {

        }

        public static Image getThumbImage(Image image, int width, int height)
        {
            Image.GetThumbnailImageAbort del = new Image.GetThumbnailImageAbort(ThumbCallback);
            int thumbWidth = width;
            int thumbHeight = height;

            // set thumb images with and height, by considering actual image with and height ratio
            if (image.Width > image.Height)
            {
                thumbWidth = width;
                thumbHeight = (int)Math.Round((image.Height / (float)image.Width) * width);
            }
            else
            {
                thumbHeight = height;
                thumbWidth = (int)Math.Round((image.Width / (float)image.Height) * height);
            }

            return image.GetThumbnailImage(thumbWidth, thumbHeight, del, IntPtr.Zero);
        }

        // use in SetImageData, for delegate
        private static bool ThumbCallback()
        {
            return false;
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


        
    }

    public class HistryDatagridSort : IComparer
    {

        public int Compare(object x, object y)
        {

            return DateTime.Parse(((DataGridViewRow)y).Cells[3].Value.ToString()).CompareTo(DateTime.Parse(((DataGridViewRow)x).Cells[3].Value.ToString()));
        }

    }
}
