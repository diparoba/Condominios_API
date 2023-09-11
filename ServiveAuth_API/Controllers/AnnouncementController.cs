using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ServiceAuth_API.Models;
using ServiceAuth_API.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceAuth_API.DTO;

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
        public async Task<IActionResult> AddAnnouncement(AnnouncementDTO announcementDTO)
        {
            var announcement = new Announcement
            {
                Id = ObjectId.Parse(announcementDTO.Id),
                Title = announcementDTO.Title,
                Content = announcementDTO.Content,
                PostedDate = announcementDTO.PostedDate,
                PostedBy = ObjectId.Parse(announcementDTO.PostedBy)
            };

            var createdAnnouncement = await _serviceAnnouncement.AddAnnouncementAsync(announcement);
            var createdAnnouncementDTO = new AnnouncementDTO
            {
                Id = createdAnnouncement.Id.ToString(),
                Title = createdAnnouncement.Title,
                Content = createdAnnouncement.Content,
                PostedDate = createdAnnouncement.PostedDate,
                PostedBy = createdAnnouncement.PostedBy.ToString()
            };
            return CreatedAtAction(nameof(GetAnnouncementById), new { id = createdAnnouncementDTO.Id }, createdAnnouncementDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(string id)
        {
            await _serviceAnnouncement.DeleteAnnouncementAsync(ObjectId.Parse(id));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAnnouncements()
        {
            var announcements = await _serviceAnnouncement.GetAllAnnouncementsAsync();
            var announcementDTOs = announcements.ToList().ConvertAll(a => new AnnouncementDTO
            {
                Id = a.Id.ToString(),
                Title = a.Title,
                Content = a.Content,
                PostedDate = a.PostedDate,
                PostedBy = a.PostedBy.ToString()
            });
            return Ok(announcementDTOs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnnouncementById(string id)
        {
            var announcement = await _serviceAnnouncement.GetAnnouncementByIdAsync(ObjectId.Parse(id));
            var announcementDTO = new AnnouncementDTO
            {
                Id = announcement.Id.ToString(),
                Title = announcement.Title,
                Content = announcement.Content,
                PostedDate = announcement.PostedDate,
                PostedBy = announcement.PostedBy.ToString()
            };
            return Ok(announcementDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnnouncement(string id, AnnouncementDTO announcementDTO)
        {
            var announcement = new Announcement
            {
                Id = ObjectId.Parse(announcementDTO.Id),
                Title = announcementDTO.Title,
                Content = announcementDTO.Content,
                PostedDate = announcementDTO.PostedDate,
                PostedBy = ObjectId.Parse(announcementDTO.PostedBy)
            };
            await _serviceAnnouncement.UpdateAnnouncementAsync(ObjectId.Parse(id), announcement);
            return Ok();
        }
    }
}
