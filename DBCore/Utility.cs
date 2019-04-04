using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;
using System.Xml;

namespace DBCore
{
    public enum DBResult { Add, Update, Delete, Exception }

    public enum AppSetting { BgImage, LabelLength, capColor, bgColor, statusColor }

    public enum UserLevel { SystemUser = 0, SystemUser_I = 1, SystemUser_IUD = 2, SystemAdmin = 3 };

    public enum UtilityDataName { OtherLang = 1, MahanayakaHimi = 2, acharyaHimi = 3, Nikaya = 4, UpadyaHimi = 5, placeRobing = 6, PlaceUpasampada = 7, Country = 8 };

    public enum CurrenStatus
    {
        /// <summary>
        /// Asapuwe siti
        /// </summary>
        Siti = 1,

        /// <summary>
        /// has left asapuwa
        /// </summary>
        OtherPlace = 2,

        /// <summary>
        /// Upavidi wee atha
        /// </summary>
        Upavidi = 3,

        all = 4,

        /// <summary>
        /// Passed away
        /// </summary>
        Apawath = 5,

        /// <summary>
        /// has left asapuwa,ශිෂ්‍ය භාවයෙන් ඉවත් වී සිටී
        /// </summary>
        OtherPlaceResignStudent = 6,
    };

    public enum BhikkuType { SangaUpasthayaka, AnusangaUpasthayaka, Upasampada, Samanera, all };

    public enum BhikkuPost { NAN = 0, SangaUpasthayaka = 1, AnusangaUpasthayaka = 2 };

    public enum BhikkuChangeType
    {
        /// <summary>
        /// Change as normal procedure
        /// </summary>
        Normal = 1,

        /// <summary>
        /// Chage on request by him self 
        /// </summary>
        onRequest = 2,

        /// <summary>
        /// Change on request of Sangha Upasthayaka thero
        /// </summary>
        OnSuSwRequest = 3
    }

    public class Utility
    {
        public static string ConnectionString;
        public static string DBConfigDataFile = "config.dat";

        public static string GetConnectionString()
        {
            if (string.IsNullOrEmpty(ConnectionString))
            {
                ConnectionString = string.Concat(ConfigurationManager.ConnectionStrings["DB"].ConnectionString, GetDBPassword());
            }

            return ConnectionString;
        }

        public static string GetAppsetting(AppSetting key)
        {
            return ConfigurationManager.AppSettings[key.ToString()];
        }


        public static void SetAppsetingData(AppSetting key, string value)
        {
            try
            {

                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                foreach (XmlElement element in xmlDoc.DocumentElement)
                {
                    if (element.Name.Equals("appSettings"))
                    {
                        foreach (XmlNode node in element.ChildNodes)
                        {
                            if (node.Attributes[0].Value.Equals(key.ToString()))
                            {
                                node.Attributes[1].Value = value;
                            }
                        }
                    }
                }

                xmlDoc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

                ConfigurationManager.RefreshSection("appSettings");
            }
            catch { }
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
                dateDiff = string.Concat(dateDiff, "මාස:", months, "  ");

            if (days > 0)
                dateDiff = string.Concat(dateDiff, "දින:", days, "  ");

            return dateDiff;
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


        public static byte[] GetFileByteCompress(string fileName)
        {
            byte[] fileData = File.ReadAllBytes(fileName);

            return CompressGZip(fileData);
        }

        public static void CreateDBPassword(string password)
        {
            string pwdString = Convert.ToBase64String(CompressGZip(Serialize(password)));

            File.WriteAllText(DBConfigDataFile, pwdString);
        }

        public static string GetDBPassword()
        {
            try
            {
                string pwdString = File.ReadAllText(DBConfigDataFile);

                if (!string.IsNullOrEmpty(pwdString))
                {
                    return (string)Deserialize(DecompressGZip(Convert.FromBase64String(pwdString)));
                }

                throw new Exception();
            }
            catch
            {
                throw new Exception("Please set db password");
            }
        }

        public static string getThumbString(string image, int width, int height)
        {
            Image img = Image.FromStream(new MemoryStream(Convert.FromBase64String(image)));

            img = getThumbImage(img, width, height); ;

            MemoryStream mem = new MemoryStream();
            img.Save(mem, ImageFormat.Jpeg);

            return Convert.ToBase64String(mem.ToArray());
        }

        public static Image getThumbImage(string image, int width, int height)
        {

            Image img = Image.FromStream(new MemoryStream(Convert.FromBase64String(image)));

            return getThumbImage(img, width, height);
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

        public static Image GetImageFromString(string imageData)
        {

            MemoryStream mem = new MemoryStream(Utility.GetByteFrom64String(imageData));

            Image img = Image.FromStream(mem);
            return img;
        }


        public static string Get64String(byte[] data)
        {
            return Convert.ToBase64String(data);
        }

        public static string Get64StringFromImage(Image img)
        {
            byte[] data = GetByteFromImage(img);
            return Convert.ToBase64String(data);
        }

        public static byte[] GetByteFrom64String(string data)
        {
            return Convert.FromBase64String(data);
        }

        public static byte[] GetByteFromImage(Image img)
        {
            // set byte array
            MemoryStream mem = new MemoryStream();
            //  picDriverImage.Image.Save(mem, ImageFormat.Png);

            img.Save(mem, ImageFormat.Jpeg);
            return mem.ToArray();
        }

        public static void LogError(Exception ex, string method)
        {
            try
            {
                string fileName = DateTime.Now.ToString("yyy-MM-dd-HH") + ".txt";

                File.AppendAllText("errorLog/" + fileName, DateTime.Now.ToString() + "\n\r" + method + "\n\r" + ex.Message + "\n\r" + ex.StackTrace + "\n\r-----------------------------------------------\n\r");
            }
            catch { }
        }
    }
}

