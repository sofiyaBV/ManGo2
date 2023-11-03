using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManGo.Data.Database.Tebles
{
    public class Manga
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Descruption { get; set; }
        public int Year { get; set; }
        public string Status { get; set; }
        public string Transtation { get; set; }
        public string Ganre { get; set; }
        public string Adapt_Name { get; set; }
        public string Way_of_reading { get; set; }
        public string Num_of_views { get; set; }
        public string Author { get; set; }
        public float Rating { get; set; }

        public Manga() { }

        public Manga(string name, string photo, string description, int year, string status, string transtation, string ganre, string adapt_name, string way_of_reading, string num_of_views, string author, float rating)
        {
            Name = name;
            Photo = photo;
            Descruption = description;
            Year = year;
            Status = status;
            Transtation = transtation;
            Ganre = ganre;
            Adapt_Name = adapt_name;
            Way_of_reading = way_of_reading;
            Num_of_views = num_of_views;
            Author = author;
            Rating = rating;
        }
    }
}
