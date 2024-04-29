using LowiskoDesktopApp.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowiskoDesktopApp.DB_Managament
{
    public class DatabaseManagement
    {
        public List<Fish> lista_ryb = new List<Fish>(); // Lista wszystkich ryb

        string connectionString = $"server={Properties.Resources.server};" +
            $"uid={Properties.Resources.uid};" +
            $"pwd={Properties.Resources.pwd};" +
            $"database={Properties.Resources.database}";
        MySqlConnection conn;

        public DatabaseManagement() // Konstruktor
        {
            conn = new MySqlConnection(connectionString);
        }

        public void WyswietlRyby()
        {
            string query = "SELECT * FROM ryby";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Fish fish = new Fish();
                fish.Nazwa = reader["nazwa"].ToString();
                fish.Wystepowanie = reader["wystepowanie"].ToString();
                fish.Styl_Zycia = reader["styl_zycia"].ToString();

                lista_ryb.Add(fish);
            }

            conn.Close();

            foreach (Fish fish in lista_ryb)
            {
                Console.WriteLine($"Nazwa: {fish.Nazwa}");
                Console.WriteLine($"Wystepowanie: {fish.Wystepowanie}");

                // Jesli Styl zycia ryby to 1 = drapiezne , 2 = spokojnego zeru
                if (fish.Styl_Zycia == "1")
                {
                    Console.WriteLine($"Styl zycia: DRAPIEZNE");
                }
                else if (fish.Styl_Zycia == "2")
                {
                    Console.WriteLine($"Styl zycia: SPOKOJNEGO ZERU");
                }
                Console.WriteLine();
            }
        }
    }
}
