using AnnouncementsServer.Models;
using AnnouncementsServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnnouncementsServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        AnnouncementsService announcementsService;
        public AnnouncementsController(AnnouncementsService announcementsServiceToSet)
        {
            announcementsService = announcementsServiceToSet;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetAnnouncements()
        {
            List<Announcement> announcements = await announcementsService.GetAll();
            return Ok(announcements);
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Announcement>> GetAnnouncement(int id)
        {
            Announcement announcement = await announcementsService.GetById(id);
            return Ok(announcement);
        }
        [HttpPut]
        public async Task<ActionResult> PutAnnouncement(Announcement announcement)
        {
            await announcementsService.Update(announcement);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostAnnouncement(Announcement announcement)
        {
            int insertedPrimaryKey = await announcementsService.Add(announcement);
            return Ok(insertedPrimaryKey);
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAnnouncement(int id)
        {
            await announcementsService.Delete(id);
            return NoContent();
        }
        [HttpPut("getSimilar/{amount:int}")]
        public async Task<ActionResult<IEnumerable<Announcement>>> GetSimilar(Announcement announcement, int amount)
        {
            List<Announcement> announcements=await announcementsService.GetSimilarAnnouncements(announcement, amount);
            return Ok(announcements);
        }
    }
}
