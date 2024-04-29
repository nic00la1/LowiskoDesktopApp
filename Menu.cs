using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowiskoDesktopApp
{
    public class Menu
    {
        // Wyswietlenie menu
        public void ShowMenu()
        {
            Console.WriteLine("1. Wyswietl wszystkie ryby + info");
            Console.WriteLine("2. Wyswietl wszystkie lowiska + ryby jakie tam wystepuja");
            Console.WriteLine("3. Dodaj nowego rybaka");

            Console.Write("Wybierz opcję: ");
            string opcja = Console.ReadLine();

            switch (opcja)
            {
                case "1":
                    Console.WriteLine("Wyswietl wszystkie ryby + info");
                    break;
                case "2":
                    Console.WriteLine("Wyswietl wszystkie lowiska + ryby jakie tam wystepuja");
                    break;
                case "3":
                    Console.WriteLine("Dodaj nowego rybaka");
                    break;
                default:
                    Console.WriteLine("Nieznana opcja");
                    break;
            }
        }
    }
}
