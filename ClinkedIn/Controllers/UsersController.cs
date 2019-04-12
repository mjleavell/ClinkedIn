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
        readonly InterestRepository _interestRepository;
        readonly User _user;
        readonly Interests interest;

        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
            //Freind Repo
            //Enemy Repo
            //Service Repo
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

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(_userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public ActionResult GetSingleUser(string id)
        {
            return Ok(_userRepository.GetSingleUser(id));
        }

        [HttpGet("{id}/interest")]
        public ActionResult ListInterest(string id)
        {
            var userIntrestList = _userRepository.GetSingleUser(id).Interests;
            return Ok(userIntrestList);
        }


        [HttpPut("{id}/interest/add")]
        public ActionResult AddInterest(string id, string interest)
        {
    
            var userIntrestList = _userRepository.GetSingleUser(id).Interests;

            userIntrestList.Add(interest);
            return Ok();
            //return Created($"users/{_user.Id}", interest);
        }
    }
}
