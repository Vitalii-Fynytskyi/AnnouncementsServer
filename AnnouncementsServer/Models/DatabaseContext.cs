using Microsoft.EntityFrameworkCore;

namespace AnnouncementsServer.Models
{
    public class DatabaseContext :DbContext
    {
        public DbSet<Announcement> Announcements { get; set; } = null!;
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            if(Database.EnsureCreated() == true)
            {
                string CreateFullTextIndex = File.ReadAllText(Environment.CurrentDirectory + @"\sql queries\CreateFullTextIndex.sql");
                string CreateProcedure_GetSimilarAnnouncements = File.ReadAllText(Environment.CurrentDirectory + @"\sql queries\CreateProcedure_GetSimilarAnnouncements.sql");

                Database.ExecuteSqlRaw(CreateFullTextIndex);
                Database.ExecuteSqlRaw(CreateProcedure_GetSimilarAnnouncements);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Announcement announcement1 = new Announcement()
            {
                Id = 1,
                Title= "Fruits sale",
                Description="banana kiwi orange strawberry",
                DateAdded= DateTime.Now
            };
            Announcement announcement2 = new Announcement()
            {
                Id = 2,
                Title = "Cars sale",
                Description = "bananacar kiwicar orangecar strawberrycar banana horsecar bananamonkey",
                DateAdded = DateTime.Now
            };
            Announcement announcement3 = new Announcement()
            {
                Id = 3,
                Title = "Animal sale",
                Description = "horsecar bananamonkey strawberryfly orangekiwi",
                DateAdded = DateTime.Now
            };
            Announcement announcement4 = new Announcement()
            {
                Id = 4,
                Title = "NotSimilarAnnouncement",
                Description = "NotSimilarDescription",
                DateAdded = DateTime.Now
            };
            Announcement announcement5 = new Announcement()
            {
                Id = 5,
                Title = "Fruits sale",
                Description = "banana word1 word2 word3",
                DateAdded = DateTime.Now
            };
            modelBuilder.Entity<Announcement>().HasData(announcement1, announcement2, announcement3, announcement4,announcement5);
            base.OnModelCreating(modelBuilder);
        }
    }
}
