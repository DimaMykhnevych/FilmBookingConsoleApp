using CinemaPoster.Entities;

namespace CinemaPoster.Helpers
{
    public static class FilmDatesCounter
    {
        public static int AmountOfFilmDates(Film film)
        {
            int amountOffilmDates = 0;

            foreach (var date in film.Time)
            {
                amountOffilmDates++;
            }

            return amountOffilmDates;
        }
    }
}
