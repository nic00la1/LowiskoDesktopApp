using LowiskoDesktopApp.DB_Managament;

namespace LowiskoDesktopApp.UI
{
    public class Utilities
    {
        public void WyswietlRyby()
        {
            Console.Clear();
            Console.WriteLine("Wszystkie ryby:");

            DatabaseManagement db = new DatabaseManagement();

            db.WyswietlRyby();
            Console.ReadKey(); // Dopoki nie nacisniesz klawisza program nie wyjdzie z metody
        }

        public void WyswietlLowiska()
        {
            Console.Clear();
            Console.WriteLine("Wszystkie lowiska + Ryby w nich występujące:");

            DatabaseManagement db = new DatabaseManagement();

            db.WyswietlLowiska();
            Console.ReadKey(); // Dopoki nie nacisniesz klawisza program nie wyjdzie z metody
        }

        public void DodajRybaka()
        {
            Console.Clear();
            Console.WriteLine("Dodaj nowego rybaka:\n");

            DatabaseManagement db = new DatabaseManagement();

            db.DodajRybaka();
            Console.ReadKey(); // Dopoki nie nacisniesz klawisza program nie wyjdzie z metody
        }
    }
}
