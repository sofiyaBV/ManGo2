using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManGo.Data.Database.Tebles
{
    public class Chapters
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int Count { get; set; }
        public Chapters() { }
        public Chapters(int mangaId, int count)
        {
            MangaId=mangaId;
            Count=count;
        }
    }
}
