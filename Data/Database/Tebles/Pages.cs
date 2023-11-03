using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManGo.Data.Database.Tebles
{
    public class Pages
    {
        public int Id { get; set; }
        public int ChaptersId { get; set; }
        public string Photo { get; set; }
        public Pages() { }
        public Pages(int chaptersId, string photo)
        {
            ChaptersId = chaptersId;
            Photo = photo;
        }
    }
}
