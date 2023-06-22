using AnnouncementsServer.Exceptions;
using AnnouncementsServer.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;

namespace AnnouncementsServer.Services
{
    /// <summary>
    /// This class contains all functionality for working with Announcements
    /// </summary>
    public class AnnouncementsService
    {
        DatabaseContext db;
        public AnnouncementsService(DatabaseContext dbToSet)
        {
            db = dbToSet;
        }
        public async Task<List<Announcement>> GetAll()
        {
            List<Announcement> announcements = await db.Announcements.AsNoTracking().ToListAsync();
            return announcements;
        }
        public async Task<Announcement> GetById(int id)
        {
            Announcement? announcement = await db.Announcements.AsNoTracking().FirstOrDefaultAsync((a)=>a.Id == id);
            if (announcement == null)
            {
                throw new NotFoundException($"Announcement with id={id} cannot be found");
            }
            return announcement;
        }
        /// <summary>
        /// Adds new announcement to database and returns primary key
        /// </summary>
        public async Task<int> Add(Announcement announcement)
        {
            announcement.Id = 0;
            db.Announcements.Add(announcement);
            await db.SaveChangesAsync();
            return announcement.Id;
        }
        public async Task Update(Announcement announcement)
        {
            bool doesAnnouncementExists = await db.Announcements.AnyAsync(a => a.Id == announcement.Id);
            if (doesAnnouncementExists == false)
            {
                throw new NotFoundException($"Announcement with id={announcement.Id} cannot be found");
            }
            db.Announcements.Update(announcement);
            db.SaveChanges();
        }
        public async Task Delete(int id)
        {
            Announcement? announcement = await db.Announcements.AsNoTracking().FirstOrDefaultAsync((a) => a.Id == id);
            if (announcement == null)
            {
                throw new NotFoundException($"Announcement with id={id} cannot be found");
            }
            db.Announcements.Remove(announcement);
            await db.SaveChangesAsync();
        }

        public async Task<List<Announcement>> GetSimilarAnnouncements(Announcement target, int maxAmount)
        {
            var idParam = new SqlParameter("@Id", target.Id);
            var titleParam = new SqlParameter("@Title", target.Title);
            var descriptionParam = new SqlParameter("@Description", target.Description);
            var topParam = new SqlParameter("@Top", maxAmount);
            List<Announcement> similarAnnouncements = await db.Announcements
                .FromSqlRaw("EXECUTE GetSimilarAnnouncements @Id, @Title, @Description, @Top", idParam, titleParam, descriptionParam, topParam)
                .ToListAsync();
            return similarAnnouncements;

        }
    }
}
