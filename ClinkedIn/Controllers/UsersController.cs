using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Data;
using ClinkedIn.Models;
using ClinkedIn.Validators;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly CreateUserRequestValidator _validator;

        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }

        // Get All Users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            //var users = _userRepository.GetAllUsers();
            return Ok(_userRepository.GetAllUsers());
        }

        // Get Single User
        [HttpGet("{id}")]
        public ActionResult GetSingleUser(string id)
        {
            return Ok(_userRepository.GetSingleUser(id));
        }

        // Add User
        [HttpPost("register")]
        //public ActionResult AddUser([FromBody]CreateUserRequest createRequest)
        public ActionResult AddUser(CreateUserRequest createRequest)
        {
            if (!_validator.Validate(createRequest))
            {
                return BadRequest(new { error = "users must have a username and password" });
            }
            var newUser = _userRepository.AddUser(createRequest.Username, createRequest.Password, createRequest.ReleaseDate);

            return Created($"api/users/{newUser.Id}", newUser);
        }

        // Delete User
        [HttpDelete("{id}")]
        public void DeleteUser(string id)
        {
            _userRepository.DeleteUser(id);
        }

        // Update User
        //[HttpPut("{id}")]


        // Add Friend to User
        [HttpPut("users/{id}/newfriend/{friendId}")]
        public ActionResult AddFriend(string userId, string friendId)
        {
            var friend = _userRepository.GetSingleUser(friendId);
            var userFriends = _userRepository.GetSingleUser(userId).Friends;
            //var updatedFriends = userFriends.Where(friend => friend.Id != friendId).
            if (userFriends.Contains(friend))
            {
                return BadRequest(new { error = $"The user is already friends with {friend.Username}" });


            }
            else
            {
                userFriends.Add(friend);
                return Ok();
            }     
        }
    }
}
