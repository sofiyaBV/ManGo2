using ManGo.Data.Database.Tebles;
using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;

namespace ManGo.Data.Database
{
    public class ManGoBDContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Profil> Profil { get; set; }
        public DbSet<Manga> Manga { get; set; }
        public DbSet<Chapters> Chapter { get; set; }
        public DbSet<Pages> Page { get; set; }
        public DbSet<Reading_List> Reading_List { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=MIPC\\SQLEXPRESS01;Initial Catalog=ManGo;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
