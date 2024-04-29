using LowiskoDesktopApp.DB_Managament;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowiskoDesktopApp.UI
{
    public  class Utilities
    {
        public void WyswietlRyby()
        {
            Console.Clear();
            Console.WriteLine("Wszystkie ryby:");

            DatabaseManagement db = new DatabaseManagement();

            db.WyswietlRyby();
        }
    }
}
