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
    }
}
