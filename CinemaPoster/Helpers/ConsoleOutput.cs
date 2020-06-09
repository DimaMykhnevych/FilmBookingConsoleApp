using CinemaPoster.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CinemaPoster.Helpers
{
    public static class ConsoleOutput
    {
        public static void XmlDocConsoleOutput(List<DateEvent> dateEvents)
        {
            bool firstFilmDate = true;
            int amountOfFilmDates;
            int filmDatesCounter = 0;
            foreach (var daySchedule in dateEvents)
            {
                Console.WriteLine(daySchedule.FilmSchedule.Day + " " + ToMonthName(daySchedule.FilmSchedule) + " " +
                    $"({ Converter.DaysConverter(daySchedule.FilmSchedule.DayOfWeek.ToString())})");
                foreach (var film in daySchedule.Films)
                {
                    amountOfFilmDates = FilmDatesCounter.AmountOfFilmDates(film);
                    Console.Write(film.Name.PadRight(17).PadLeft(21));
                    foreach (var time in film.Time)
                    {
                        filmDatesCounter++;
                        string timeToStr = time.TimeOfDay.ToString().Substring(0, 5);
                        if (firstFilmDate && filmDatesCounter != amountOfFilmDates)
                            Console.Write(timeToStr.PadLeft(17) + " | ");
                        else if (firstFilmDate && filmDatesCounter == amountOfFilmDates)
                            Console.Write(timeToStr.PadLeft(17));
                        else if (filmDatesCounter == amountOfFilmDates)
                            Console.Write(timeToStr);
                        else Console.Write(timeToStr + " | ");
                        firstFilmDate = false;
                    }
                    firstFilmDate = true;
                    filmDatesCounter = 0;
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        public static void ShowChosenFilms(List<FilmToSearch> filmToSearches)
        {

            Console.WriteLine("Список забронированных сеансов: ");
            if (filmToSearches.Count == 0) Console.WriteLine("Вы еще не бронировали сеансы");
            foreach(var film in filmToSearches)
            {
                Console.WriteLine("Название: " + film.Name);
                Console.WriteLine("Время: " + film.Time.TimeOfDay.ToString().Substring(0, 5));
                Console.WriteLine("Дата: " + film.Date.ToString().Substring(0, 10));
                Console.WriteLine();
            }
        }
        public static void ShowChoiceInfo()
        {
            Console.WriteLine();
            Console.WriteLine("Команды:");
            Console.WriteLine("1. Забронировать сеанс");
            Console.WriteLine("2. Список забронированных сеансов");
            Console.WriteLine();
            Console.Write(">");
        }

        private static string ToMonthName(DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }
    }
}
