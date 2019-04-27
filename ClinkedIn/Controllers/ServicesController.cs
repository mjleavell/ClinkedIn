using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Data;
using ClinkedIn.Models;
using ClinkedIn.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinkedIn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        readonly UserRepository _userRepository;
        readonly ServiceRepository _servicesRepository;
        readonly CreateUserRequestValidator _validator;

        public ServicesController()
        {
            _validator = new CreateUserRequestValidator();
            _userRepository = new UserRepository();
            _servicesRepository = new ServiceRepository();
        }

        [HttpGet("{id}/service")]
        public ActionResult <List<Services>> GetAllServices()
        {
            var serviceList = _servicesRepository.GetAllServices();
            return Ok(serviceList);
        }

        [HttpPost("{id}/service/add")]
        public ActionResult ListService(CreateServiceRequest createRequest)
        {
            var serviceList = _servicesRepository.AddService(createRequest.Name, createRequest.Description, createRequest.Price);
            return Ok(serviceList);
        }


        //[HttpPut("{id}/service/remove")]
        //public ActionResult RemoveService(string id, string service)
        //{
        //    var userServicesList = _userRepository.GetSingleUser(id).Services;

        //    userServicesList.Remove(service);
        //    return Ok();
        //}
    }
}