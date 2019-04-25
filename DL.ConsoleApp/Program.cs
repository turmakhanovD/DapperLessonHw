using DL.DataAccess;
using System;

namespace DL.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var repository = new ReceiversRepository())
            {
                Menu menu = new Menu();

                menu.StartMenu();
                Console.ReadLine();
            }
        }
    }
}
