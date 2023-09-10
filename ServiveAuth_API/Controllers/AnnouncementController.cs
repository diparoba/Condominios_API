using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceAuth_API.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("PolicyLocal")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly IServiceAnnouncement _serviceAnnouncement;

        public AnnouncementController(IServiceAnnouncement serviceAnnouncement)
        {
            _serviceAnnouncement = serviceAnnouncement;
        }

        [HttpPost]
        public async Task<IActionResult> AddAnnouncement(Announcement announcement)
        {
            var createdAnnouncement = await _serviceAnnouncement.AddAnnouncementAsync(announcement);
            return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdAnnouncement.Id }, createdAnnouncement);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(ObjectId id)
        {
            await _serviceAnnouncement.DeleteAnnouncementAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _serviceAnnouncement.GetAllAnnouncementsAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(ObjectId id)
        {
            var announcement = await _serviceAnnouncement.GetAnnouncementByIdAsync(id);
            return Ok(announcement);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(ObjectId id, Announcement announcement)
        {
            await _serviceAnnouncement.UpdateAnnouncementAsync(id, announcement);
            return Ok();
        }
    }
}
