using CinemaPoster.Entities;
using CinemaPoster.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CinemaPoster.Helpers
{
    public class Options
    {
        public List<FilmToSearch> reservedFilms = new List<FilmToSearch>();
        private List<DateEvent> _dateEvents = DataBase.Get();
        public FilmToSearch MakeFilmBooking()
        {
            DateTime date = DateTime.Now;
            DateTime time = DateTime.Now;
            string name = " ";
            Console.Write("Введите дату на которую хотите забронировать фильм в виде \"число.месяц\": ");
            try
            {
                date = Convert.ToDateTime(Console.ReadLine());
                Console.Write("Введите время на которое хотите забронировать фильм в виде \"часы:минуты\": ");
                time = ToDateTime(Console.ReadLine());
                Console.Write("Введите название фильма: ");
                name = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("Неверный формат данных!");
                MakeFilmBooking();
            }
            return new FilmToSearch() { Name = name, Time = time, Date = date };

        }
        public void SaveSelectedFilm(FilmToSearch filmToSearch)
        {
            if (CheckSelectedFilm(filmToSearch)) { reservedFilms.Add(filmToSearch); Console.WriteLine("Забронировано!"); }
            else Console.WriteLine("Фильма на указанную дату не было найдено!");
        }
        public bool CheckSelectedFilm(FilmToSearch filmToSearch)
        {
            foreach (var e in _dateEvents)
            {
                if (filmToSearch.Date == e.FilmSchedule)
                {
                    var resultFilms = e.Films.Where(x => x.Name.ToLower() == filmToSearch.Name.ToLower()).ToList();
                    foreach (var r in resultFilms)
                    {
                        foreach (var d in r.Time)
                            if (d == filmToSearch.Time)
                            {
                                return true;
                            }
                    }
                }
            }
            return false;
        }
        public static DateTime ToDateTime(string time)
        {
            return Convert.ToDateTime("01/01/1800 " + time + ":00.00");
        }
    }
}
