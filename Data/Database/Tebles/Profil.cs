using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManGo.Data.Database.Tebles
{
    public class Profil
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] Photo { get; set; }
        public int Theme { get; set; }
        public Profil(int userId, byte[] photo, int theme)
        {
            UserId = userId;
            Photo = photo;
            Theme = theme;
        }
    }
}
