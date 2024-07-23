using BookingAPI.Interfaces;
using BookingAPI.Models;
using BookingAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingRepo<int, Booking> _repo;
        private readonly BookingService _service;

        public BookingController(IBookingRepo<int,Booking> bookingRepo,BookingService bookingService) {
            _repo = bookingRepo;
            _service= bookingService;

        }
        [Authorize(Roles = "user")]
        [HttpPost("Add Booking Details")]
        [ProducesResponseType(typeof(Booking), 201)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> AddRoomDetails([FromBody] Booking Booking)
        {
            var roomstatus=_service.CheckStatusOfRoom(Booking);
            if (roomstatus == null)
            {
                var resultBooking = _repo.Add(Booking);
                if (resultBooking != null)
                {
                    return Created("Booking",resultBooking);
                }
                return BadRequest(new { message = "Cannot Book Room" });
            }
            return NotFound(new { message = "This room is booked already" });


        }
        [Authorize(Roles = "user")]
        [HttpDelete("Delete Booking Details")]
        [ProducesResponseType(typeof(Booking), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> DeleteBookingDetails(int bookingId)
        {
            Booking booking = _repo.Get(bookingId);
            if (booking == null)
                return NotFound(new { message = "No such booking is present" });
            booking = _repo.Delete(bookingId);
            if (booking == null)
                return BadRequest(new { message = "Unable to delete room details" });
            return Ok(booking);
        }
        [Authorize(Roles = "staff")]
        [HttpGet("Get All Booking Details")]
        [ProducesResponseType(typeof(ICollection<Booking>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> GetAllRoomDetails()
        {
            ICollection<Booking> bookings = _repo.GetAll().ToList();
            if (bookings != null)
            {
                return Ok(bookings);
            }
            return NotFound(new { message = "No Booking Details" });

        }
        [Authorize]
        [HttpGet("Get bookings Details By Id")]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> GetRoomDetailsById(int bookingId)
        {
            var booking = _repo.Get(bookingId);
            if (booking != null)
            {
                return Ok(booking);
            }
            return NotFound(new { message = "No booking Details in this particular Id" });

        }
        [Authorize]
        [HttpPut("Updating Booking Details")]
        [ProducesResponseType(typeof(Booking), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Booking> GetRoomDetailsById(Booking booking)
        {
            var bookingData = _repo.Get(booking.BookingId);
            if (bookingData!= null)
            {
                Booking bookingDetails= _repo.Update(booking);
                return bookingDetails;
                
            }
            return NotFound(new { message = "No booking Details in this particular Id" });

        }
    }
}
