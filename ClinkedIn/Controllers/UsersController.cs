﻿using System;
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
        readonly User _user;
        readonly Interests interest;

        public UsersController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
        }


        // -------------------------------- Users --------------------------------

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


        // -------------------------------- Friends --------------------------------
        // Add Friend to User
        [HttpPut("{userId}/addFriend/{friendId}")]
        public ActionResult AddFriend(string userId, string friendId)
        {
            var users = _userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == userId);
            var friendToAdd = users.FirstOrDefault(f => f.Id == friendId);

            if (!user.Friends.Contains(friendToAdd) && user.Id != friendId)
            {
                user.Friends.Add(friendToAdd);
                return Ok(user);
            }
            else if (user.Id == friendId)
            {
                return BadRequest(new { error = "Sorry but you can't be friends with yourself" });
            }
            else
            {
                return BadRequest(new { error = $"The user is already friends with {friendToAdd.Username}" });
            }
        }

        // Remove Friend from User
        [HttpPut("{userId}/removeFriend/{friendId}")]
        public ActionResult RemoveFriend(string userId, string friendId)
        {
            var users = _userRepository.GetAllUsers();
            var user = users.FirstOrDefault(u => u.Id == userId);
            var friendToRemove = users.FirstOrDefault(f => f.Id == friendId);

            if (user.Friends.Contains(friendToRemove))
            {
                user.Friends.Remove(friendToRemove);
                return Ok(user);
            }
            else
            {
                return BadRequest(new { error = $"I'm sorry {user.Username}, but you don't have any friends. You should be nicer to people." });
            }
        }

        // -------------------------------- Interests --------------------------------


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

        // -------------------------------- Services --------------------------------

        [HttpGet("{id}/service")]
        public ActionResult ListService(string id)
        {
            var userServiceList = _userRepository.GetSingleUser(id).Services;
            return Ok(userServiceList);
        }


        [HttpPut("{id}/service/add")]
        public ActionResult AddService(string id, string service)
        {

            var userServiceList = _userRepository.GetSingleUser(id).Services;

            if(!userServiceList.Contains(service))
            {
                userServiceList.Add(service);
                return Ok();
            }
            else
            {
                return BadRequest("You can't add the same service twice...");
            }
        }

        // -------------------------------- Enemies --------------------------------
        // Add Enemy to User //
        [HttpPut("{userId}/addEnemy/{enemyId}")]
        public ActionResult AddEnemy(string userId, string enemyId)
        {
            var users = _userRepository.GetAllUsers();
            var user = users.First(u => u.Id == userId);
            var enemyToAdd = users.First(f => f.Id == enemyId);

            if (!user.Enemies.Contains(enemyToAdd) && user.Id != enemyId)
            {
                user.Enemies.Add(enemyToAdd);
                return Ok(user);
            }
            else if (userId == enemyId)
            {
                return BadRequest("Are you enemies with yourself?");
            }
            else
            {
                return BadRequest($"You are already enemies with {enemyToAdd.Username}");
            }
        }

        // Get enemy of User //
        [HttpGet("{userId}/enemies")]
        public ActionResult GetEnemies(string userId)
        {
            var inmateEnemies = _userRepository.GetSingleUser(userId);
            return Ok(inmateEnemies.Enemies);
        }

        // Remove enemy //
        [HttpPut("{userId}/removeEnemy/{enemyId}")]
        public ActionResult RemoveEnemy(string userId, string enemyId)
        {
            var users = _userRepository.GetAllUsers();
            var user = users.First(u => u.Id == userId);
            var enemyToRemove = users.First(f => f.Id == enemyId);

            if (user.Enemies.Contains(enemyToRemove))
            {
                user.Enemies.Remove(enemyToRemove);
                return Ok(user);
            }
            else
            {
                return BadRequest("Congratulations! You don't have any enemies...or so you think...so watch your back...");
            }
        }

        //------------------------ Release Date Calculation ----------------------
        
        [HttpGet("{userId}/release")]
        public ActionResult GetDaysTilRelease(string userId)
        {
            var inmate = _userRepository.GetSingleUser(userId);
            var daysTilRelease = inmate.ReleaseDate.Subtract(DateTime.Today).Days;

            return Ok($"{inmate.Username} has {daysTilRelease} days till they are released");
        }
    }
}