using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManGo.Data.Database.Tebles
{
    public class Reading_List
    {
        public int Id { get; set; }
        public int MangaId { get; set; }
        public int UserId { get; set; }
        public int List { get; set; }

        public Reading_List() { }
        public Reading_List(int mangaId, int userId, int list)
        {
            MangaId=mangaId;
            UserId=userId;
            List=list;
        }
    }
}
