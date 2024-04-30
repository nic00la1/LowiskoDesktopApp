using ConsoleTables;
using LowiskoDesktopApp.Models;
using MySql.Data.MySqlClient;

namespace LowiskoDesktopApp.DB_Managament
{
    public class DatabaseManagement
    {
        public List<Fish> lista_ryb = new List<Fish>(); // Lista wszystkich ryb
        public List<Lowisko> lista_lowisk = new List<Lowisko>();

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

        public List<Lowisko> WyswietlLowiska()
        {
            string query = "SELECT lowisko.*, ryby.nazwa AS Nazwa_ryby " +
                "FROM lowisko " +
                "JOIN ryby ON lowisko.ryby_id = ryby.id";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            // jesli polaczenie juz jest, to nie trzeba go otwierac
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            MySqlDataReader reader = cmd.ExecuteReader();

            List<Lowisko> lista_lowisk = new List<Lowisko>(); // Przechowuje listę łowisk

            while (reader.Read())
            {
                Lowisko lowisko = new Lowisko();
                lowisko.Id = Convert.ToInt32(reader["id"]);
                lowisko.Akwen = reader["akwen"].ToString();
                lowisko.Wojewodztwo = reader["wojewodztwo"].ToString();
                lowisko.Nazwa_ryby = reader["Nazwa_ryby"].ToString();

                lista_lowisk.Add(lowisko);
            }

            conn.Close();

            var table = new ConsoleTable("Id", "Jaka Ryba?",
                               "Akwen", "Wojewodztwo");

            foreach (Lowisko lowisko in lista_lowisk)
            {
                table.AddRow(lowisko.Id, lowisko.Nazwa_ryby, lowisko.Akwen, lowisko.Wojewodztwo);
            }

            table.Options.EnableCount = false;
            table.Write();

            return lista_lowisk; // Zwraca listę łowisk
        }

        public void DodajRybaka()
        {
            string createDb_query = "CREATE TABLE IF NOT EXISTS `cwiczenia_1`.`rybacy` " +
                "(`Id` INT NOT NULL AUTO_INCREMENT , " +
                "`Imie` VARCHAR(255) NOT NULL , " +
                "`Nazwisko` VARCHAR(255) NOT NULL , " +
                "`Wiek` INT NOT NULL , " +
                "`Ulubione_Lowisko` VARCHAR(255) NOT NULL , " +
                "`Data_Rejestracji` DATE NOT NULL , " +
                "PRIMARY KEY (`Id`)) ENGINE = InnoDB;";
            MySqlCommand cmd_createDb = new MySqlCommand(createDb_query, conn);

            conn.Open();

            cmd_createDb.ExecuteNonQuery(); // Wykonaj zapytanie

            Console.WriteLine("Podaj imie: ");
            string imie = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko: ");
            string nazwisko = Console.ReadLine();
            Console.WriteLine("Podaj wiek: ");
            int wiek = Convert.ToInt32(Console.ReadLine());

            string ulubione_lowisko = WybierzUlubioneLowisko();

            DateOnly data_rejestracji = DateOnly.FromDateTime(DateTime.Today);

            string formattedDate = data_rejestracji.ToString("yyyy-MM-dd"); // Format daty

            string insert_query = "INSERT INTO rybacy (Imie, Nazwisko, Wiek, Ulubione_Lowisko, Data_Rejestracji) " +
                $"VALUES ('{imie}', '{nazwisko}', {wiek}, '{ulubione_lowisko}', '{formattedDate}')";

            conn.Open();

            MySqlCommand cmd_insert = new MySqlCommand(insert_query, conn);

            cmd_insert.ExecuteNonQuery();

            Console.WriteLine("\nDodano nowego rybaka!");

            conn.Close();
        }

        private string WybierzUlubioneLowisko()
        {
            Console.WriteLine("Wybierz łowisko: ");

            var lowiska = WyswietlLowiska();
            string wybrane_lowisko = "";

            if (lowiska.Count > 0)
            {
                // Wybieranie lowiska spośród listy lowisk (strzałkami góra/dół)
                int activePosition = 0;
                Console.ReadKey();

                while (true)
                {
                    Console.Clear(); // Clear the console before displaying the lowiska
                    for (int i = 0; i < lowiska.Count; i++)
                    {
                        if (i == activePosition)
                        {
                            Console.BackgroundColor = ConsoleColor.DarkCyan;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("{0, -35}", lowiska[i].Akwen);
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                        }
                        else
                        {
                            Console.WriteLine(lowiska[i].Akwen);
                        }
                    }

                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.UpArrow)
                    {
                        activePosition = activePosition > 0 ? activePosition - 1 : lowiska.Count - 1;
                    }
                    else if (key.Key == ConsoleKey.DownArrow)
                    {
                        activePosition = activePosition < lowiska.Count - 1 ? activePosition + 1 : 0;
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        wybrane_lowisko = lowiska[activePosition].Akwen;
                        break;
                    }
                }
                Console.WriteLine($"\nWybrano łowisko: {wybrane_lowisko}");
            }
            else
            {
                Console.WriteLine("Brak łowisk dostępnych w bazie danych.");
            }

            return wybrane_lowisko;
        }
    }
}
