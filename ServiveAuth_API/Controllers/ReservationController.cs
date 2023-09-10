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
    public class ReservationController : ControllerBase
    {
        private readonly IServiceReservation _serviceReservation;

        public ReservationController(IServiceReservation serviceReservation)
        {
            _serviceReservation = serviceReservation;
        }

        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            var createdReservation = await _serviceReservation.AddReservationAsync(reservation);
            return CreatedAtAction(nameof(GetReservationById), new { id = createdReservation.Id }, createdReservation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(ObjectId id)
        {
            await _serviceReservation.DeleteReservationAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReservations()
        {
            var reservations = await _serviceReservation.GetAllReservationsAsync();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservationById(ObjectId id)
        {
            var reservation = await _serviceReservation.GetReservationByIdAsync(id);
            return Ok(reservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(ObjectId id, Reservation reservation)
        {
            await _serviceReservation.UpdateReservationAsync(id, reservation);
            return Ok();
        }
    }
}
