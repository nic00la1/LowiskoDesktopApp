namespace LowiskoDesktopApp.UI
{
    public class Menu
    {
        static string[] positions =
            {
                "1. Wyswietl wszystkie ryby + info",
                "2. Wyswietl wszystkie lowiska + ryby jakie tam wystepuja",
                "3. Dodaj nowego rybaka",
                "4. Wyswietl wszystkich rybakow",
                "5. Koniec"
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
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine(">>> Menu: <<<");
            Console.WriteLine();

            for (int i = 0; i < positions.Length; i++) // dla kazdej pozycji menu
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
                    activePosition = activePosition > 0 ? activePosition - 1 : positions.Length - 1;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    activePosition = (activePosition + 1) % positions.Length;
                    ShowMenu();
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    activePosition = positions.Length - 1;
                    break;
                }
                else if (key.Key == ConsoleKey.Enter)
                    break;

            } while (true);
        }

        private static void RunOption()
        {
            Utilities utilities = new Utilities();

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
                    utilities.WyswietlRybakow();
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
