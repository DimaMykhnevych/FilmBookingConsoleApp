using System.Collections.Generic;

namespace CinemaPoster.Helpers
{
    public static class Converter
    {
        private static Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                { "Monday", "ПН"},
                { "Tuesday", "ВТ"},
                { "Wednesday", "СР"},
                { "Thursday", "ЧТ"},
                { "Friday", "ПТ"},
                { "Saturday", "СБ"},
                { "Sunday", "ВС"}
            };
        public static string DaysConverter(string engWeekDay)
        {          
            return dictionary[engWeekDay];
        }
    }
}
