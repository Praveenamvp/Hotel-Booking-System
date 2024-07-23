using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Interfaces;
using UserAPI.Models.DTO;
using UserAPI.Models;
using UserAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly IRepo<string, User> _repo;

        public UserController(UserService userService, IRepo<string, User> repo)
        {
            _service = userService;
            _repo = repo;
        }
       
        [HttpPost("Register User")]
        [ProducesResponseType(typeof(UserDTO), 201)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Register([FromBody] UserRegisterDTO userDTO)
        {
            var user = _service.Register(userDTO);
            if (user != null)
            {
                return Created("Home", user);
            }
            return BadRequest(new Error(2, "Unable to register user"));



        }
        //[Authorize]
        [HttpPost("Login User")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> Login([FromBody] UserDTO userDTO)
        {
            UserDTO user = _service.Login(userDTO);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(new Error(2,"Cannot Login user. Password or username may be incorrect or user may be not registered" ));


        }
        [Authorize]
        [HttpPut("Update User Password")]
        [ProducesResponseType(typeof(ICollection<UserDTO>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> UpdatePassword([FromBody] UserDTO userDTO)
        {
            User userData = _repo.Get(userDTO.UserName);
            if (userData == null)
                return NotFound(new Error(1, "No such user is present" ));
            User user = _repo.Update(userDTO);
            if (user != null)
            {
                return Ok(user);
            }
            return BadRequest(new Error(2, "Cannot Update Password" ));
        }
        [Authorize]
        [HttpPut("Update User Details")]
        [ProducesResponseType(typeof(ICollection<User>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public ActionResult<UserDTO> UpdateUserDeatils([FromBody] UserRegisterDTO user)
        {
            User userData = _repo.Get(user.UserName);
            if (userData == null)
                return NotFound(new Error(1, "No such user is present" ));
            User userDetails = _repo.Update(user);
            if (userDetails != null)
            {
                return Ok(userDetails);
            }
            return BadRequest(new Error(2, "Cannot Update User Details" ));
        }
        [Authorize(Roles ="admin")]
        [HttpGet("GetAllUser")]
        [ProducesResponseType(typeof(ICollection<User>), 200)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetAllUser()
        {
            ICollection<User> users = _repo.GetAll().ToList();
            if (users != null)
            {
                return Ok(users);
            }
            return NotFound(new Error(1,"No User Details Currently" ));

        }
        [Authorize]
        [HttpDelete("Delete User")]
        [ProducesResponseType(typeof(UserDTO), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDTO> DeleteRoom(string userName)
        {
            var user = _repo.Get(userName);
            if (user == null)
                return NotFound(new Error(1, "No such user is present" ));
            user = _repo.Delete(userName);
            if (user == null)
                return BadRequest(new Error(2, "Unable to delete room details" ));
            return Ok(user);
        }
    }
}
