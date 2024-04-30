using LowiskoDesktopApp.DB_Managament;

namespace LowiskoDesktopApp.UI
{
    public class Menu
    {
        static List<string> positions = new List<string>
            {
                "1. Wyswietl wszystkie ryby + info",
                "2. Wyswietl wszystkie lowiska + ryby jakie tam wystepuja",
                "3. Dodaj nowego rybaka",
                "4. Koniec"
            };
        static int activePosition = 0;

        public static void StartMenu()
        {
            Console.Title = "Lowisko Desktop App";
            Console.CursorVisible = false;
            while (true)
            {
                ShowMenu();
                ChoosingOption();
                RunOption();
            }
        }

        private static void ShowMenu()
        {
            DatabaseManagement db = new DatabaseManagement();
            List<string> tempPositions = new List<string>
            {
                "1. Wyswietl wszystkie ryby + info",
                "2. Wyswietl wszystkie lowiska + ryby jakie tam wystepuja",
                "3. Dodaj nowego rybaka",
                "4. Koniec"
            };

            // jesli tabela rybacy istnieje to dodaj pozycje do menu
            if (db.CzyTabelaIstnieje("rybacy"))
            {
                tempPositions.Insert(3, "4. Wyswietl wszystkich rybakow");
                tempPositions[4] = "5. Koniec";
            }

            positions = tempPositions;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Menu: <<<");
            Console.WriteLine();

            for (int i = 0; i < positions.Count; i++) // dla kazdej pozycji menu
            {
                if (i == activePosition) // jesli jest aktywna
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0, -35}", positions[i]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                else
                {
                    Console.WriteLine(positions[i]);
                }
            }
        }

        private static void ChoosingOption()
        {
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow)
                {
                    activePosition = activePosition > 0 ? activePosition - 1 : positions.Count - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = (activePosition + 1) % positions.Count;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = positions.Count - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                    break;

            } while (true);
        }

        private static void RunOption()
        {
            Utilities utilities = new Utilities();
            DatabaseManagement db = new DatabaseManagement();

            switch (activePosition)
            {
                case 0:
                    utilities.WyswietlRyby();
                    break;
                case 1:
                    utilities.WyswietlLowiska();
                    break;
                case 2:
                    utilities.DodajRybaka();
                    break;
                case 3:
                    if (db.CzyTabelaIstnieje("rybacy"))
                        utilities.WyswietlRybakow();
                    else
                        Console.WriteLine("Tabela rybacy nie istnieje w bazie danych.");
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
