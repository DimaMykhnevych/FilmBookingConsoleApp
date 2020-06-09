using CinemaPoster.Entities;
using CinemaPoster.Helpers;
using CinemaPoster.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaPoster
{
    class Program
    {
        private static int TakeInfoFromUser()
        {

            return Convert.ToInt32(Console.ReadLine());
        }
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();
            var newl = DataBase.Get();
            ConsoleOutput.XmlDocConsoleOutput(newl);
            Options options = new Options();
            while (true)
            {
                ConsoleOutput.ShowChoiceInfo();
                int choice = TakeInfoFromUser();
                if (choice == 1)
                {
                    FilmToSearch filmToSearch = options.MakeFilmBooking();
                    options.SaveSelectedFilm(filmToSearch);
                }
                else
                    ConsoleOutput.ShowChosenFilms(options.reservedFilms);
            }
        }
    }
}
