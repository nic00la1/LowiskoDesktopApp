using ConsoleTables;
using LowiskoDesktopApp.Models;
using MySql.Data.MySqlClient;

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
                fish.Id = Convert.ToInt32(reader["id"]);
                fish.Nazwa = reader["nazwa"].ToString();
                fish.Wystepowanie = reader["wystepowanie"].ToString();
                fish.Styl_Zycia = reader["styl_zycia"].ToString();

                lista_ryb.Add(fish);
            }

            conn.Close();

            //  Uzywam paczke z NugetPackage - ,,ConsoleTables"
            var table = new ConsoleTable("Id", "Nazwa ryby",
                "Wystepowanie", "Styl Zycia");

            // Wyswietl wszystkie ryby
            foreach (Fish fish in lista_ryb)
            {
                string stylZycia;
                if (fish.Styl_Zycia == "1")
                {
                    stylZycia = "drapieżna";
                }
                else if (fish.Styl_Zycia == "2")
                {
                    stylZycia = "spokojnego żeru";
                }
                else
                {
                    stylZycia = "nieznany";
                }

                table.AddRow(fish.Id, fish.Nazwa, fish.Wystepowanie, stylZycia);
            }

            table.Options.EnableCount = false; // Wylacz numerowanie wierszy
            table.Write(); // Wyswietla tabele
        }
    }
}
