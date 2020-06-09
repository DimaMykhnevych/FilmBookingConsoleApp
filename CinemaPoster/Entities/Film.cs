using System;
using System.Collections.Generic;

namespace CinemaPoster.Entities
{
    public class Film
    {
        public string Name { get; set; }
        public List <DateTime> Time { get; set; }
        public int Id { get; set; }
    }
}
