using HotelAPI.Interfaces;
using HotelAPI.Models;
using HotelAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepo<int, Hotel> _repo;
        private readonly HotelService _service;

        public HotelController(IHotelRepo<int, Hotel> repo,HotelService service) {
            _repo = repo;
            _service = service;
        }
        [Authorize(Roles ="staff")]
        [HttpPost("Add Hotels")]
        [ProducesResponseType(typeof(ICollection<Hotel>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<Hotel> AddHotelDetails([FromBody] Hotel hotel)
        {
            Hotel resultHotel = _repo.Add(hotel);
            if (resultHotel!=null)
            {
                return Ok(resultHotel);
            }
            return BadRequest(new { message = "Cannot Add Hotel Details" });

        }
        [HttpGet("Get All Hotel Details")]
        [ProducesResponseType(typeof(ICollection<Hotel>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Hotel> GetAllHotelDetails()
        {
            var resultHotel = _repo.GetAll().ToList();
            if (resultHotel != null)
            {
                return Ok(resultHotel);
            }
            return NotFound(new { message = "No Hotel Details Present" });

        }
        [HttpGet("Get Hotel Details By Name")]
        [ProducesResponseType(typeof(Hotel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Hotel> HotelDetailsByName(string hotelName)
        {
            var resultHotel = _service.GetHotelDetailsByName(hotelName);
            if (resultHotel != null)
            {
                return Ok(resultHotel);
            }
            return NotFound(new { message="No Hotel Details " });
        }
        [Authorize(Roles ="staff")]
        [HttpDelete]
        [ProducesResponseType(typeof(Hotel), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<Hotel> DeleteHotel(int hotelId)
        {
            Hotel hotel = _repo.Get(hotelId);
            if (hotel == null)
                return NotFound(new { message = "No such hotel is present" });
            hotel = _repo.Delete(hotelId);
            if (hotel == null)
                return BadRequest(new{message ="Unable to delete product"});
            return Ok(hotel);
        }

        [HttpGet("Get Hotel Details By Location")]
        [ProducesResponseType(typeof(ICollection<Hotel>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<Hotel>> GetHotelDetailsByLocation(string location)
        {
            var resultHotel = _service.GetHotelsByLocation(location);
            if (resultHotel != null)
            {
                return Ok(resultHotel);
            }
            return NotFound(new { message="No Hotel Details" });

        }
        [HttpGet("Get Hotel Details By Amenities")]
        [ProducesResponseType(typeof(ICollection<Hotel>), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ICollection<Hotel>> HotelsByAmenities(string amenities)
        {
            var resultHotel = _service.GetHotelsByAmenities(amenities);
            if (resultHotel != null)
            {
                return Ok(resultHotel);
            }
            return NotFound(new { message = "No Hotel Details" });

        }
        [Authorize(Roles ="staff")]
        [HttpPut("UpdateHotelDetails")]
        [ProducesResponseType(typeof(ICollection<Hotel>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Hotel> UpdatePassword([FromBody] Hotel hotel)
        {
            var hotelResult = _repo.Update(hotel);
            if (hotelResult != null)
            {
                return Ok(hotelResult);
            }
            return BadRequest(new { message = "Cannot Update Password" });
        }


    }
}