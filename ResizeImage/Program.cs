using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCore;
using MySql.Data.MySqlClient;

namespace ResizeImage
{
    class Program
    {
        static void Main(string[] args)
        {

            using (MySqlConnection conn = new MySqlConnection(Utility.GetConnectionString()))
            {
                Dictionary<int, string> data = new Dictionary<int, string>();
                MySqlCommand comm = new MySqlCommand("SELECT ID,ImageData FROM bikkuinfo WHERE ID > 0",conn);

                conn.Open();

                using (MySqlDataReader reader = comm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string img = reader.GetString(1);

                        if (!string.IsNullOrEmpty(img))
                        {
                            

                           string thumbString =   DBCore.Utility.getThumbString(img, 250, 250);
                           data.Add(id, thumbString);
                                                      

                        }
                    }
                }


                foreach (int id in data.Keys)
                {
                    MySqlCommand Updcomm = new MySqlCommand("UPDATE BikkuInfo SET ImageData = '" + data[id]+ "' WHERE ID =" + id, conn);
                    Updcomm.ExecuteNonQuery();
                }

                Console.WriteLine("Finish");
                Console.ReadLine();

            }

        }
    }
}
