using CinemaPoster.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml.Linq;

namespace CinemaPoster.Repository
{
    public static class DataBase
    {
        private static XDocument doc;

        //Путь к Xml-документу.
        private static string _filePath = "..//..//..//db.xml";

        //Корневой элемент Xml-документа.
        private static XElement Root;

        /// <summary>
        /// Статический конструктор без параметров.
        /// Создание или подключение документа.
        /// </summary>
        static DataBase()
        {
            if (File.Exists(_filePath))
                doc = XDocument.Load(_filePath);
            else
            {
                doc = new XDocument(new XElement("dates"));
                doc.Save("db.xml");
            }
            Root = doc.Element("dates");
        }

        /// <summary>
        /// Метод сохранения коллекции  в файл.
        /// </summary>
        public static void Save(List<DateEvent> list)
        {
            Root.RemoveAll();
            foreach (var el in list)
            {
                Root.Add(getXElementFromFilm(el));
            }
            doc.Save(_filePath);
        }

        /// <summary>
        ///  Метод создания коллекции 
        ///  с помощью чтения из файла.
        /// </summary>
        /// <returns>Возвращает список компаний.</returns>
        public static List<DateEvent> Get()
        {
            var list = new List<DateEvent>();

            foreach (var elem in Root.Elements())
            {
                var tmp = getFilmsFromXElement(elem);
                if (tmp != null)
                    list.Add(tmp);
            }
            return list;
        }

        //Создание Xml-элемента <date> из объекта dateEvent
        private static XElement getXElementFromFilm(DateEvent dateEvent)
        {
            XElement movies = new XElement("movies");
            foreach (Film movie in dateEvent.Films)
            {
                XElement film = new XElement("movie");
                film.Add(new XAttribute("id", $"{movie.Id}"));
                film.Add(new XAttribute("name", $"{movie.Name}"));


                XElement sessions = new XElement("sessions");
                foreach (var movieStart in movie.Time)
                {
                    XElement session = new XElement("session");
                    session.Add(new XAttribute("time", movieStart.TimeOfDay.ToString().Substring(0, 5)));
                    sessions.Add(session);
                }
                film.Add(sessions);
                movies.Add(film);
            }
            XElement date = new XElement("date");
            date.Add(new XAttribute("value", Convert.ToString(dateEvent.FilmSchedule).Substring(0, 10)));
            date.Add(movies);
            return date;
        }

        //Создание объекта DateEvent из Xml-элемента <date>
        //с помощью чтения из файла.
        private static DateEvent getFilmsFromXElement(XElement xDateEvent)
        {
            if (xDateEvent != null)
            {
                DateEvent dateEvent = new DateEvent();
                var films = new List<Film>();
                foreach (XElement element in xDateEvent.Element("movies")?.Elements("movie"))
                {
                    Film movie = new Film();
                    movie.Id = Convert.ToInt32(element.FirstAttribute?.Value ?? "-1");
                    movie.Name = element.Attribute("name").Value;
                    movie.Time = new List<DateTime>();
                    foreach (XElement movieStart in element.Element("sessions")?.Elements("session"))
                    {
                        movie.Time.Add(ToDateTime(movieStart.FirstAttribute?.Value ?? "-1"));

                    }
                    films.Add(movie);
                }
                dateEvent.FilmSchedule = Convert.ToDateTime(xDateEvent.FirstAttribute?.Value, CultureInfo.GetCultureInfo("ru-RU"));
                dateEvent.Films = films;
                return dateEvent;
            }
            return null;
        }

        // Метод перевода строки формата "hh:mm" в формат DateTime      
        public static DateTime ToDateTime(string time)
        {
            return Convert.ToDateTime("01/01/1800 " + time + ":00.00");
        }
    }
}
