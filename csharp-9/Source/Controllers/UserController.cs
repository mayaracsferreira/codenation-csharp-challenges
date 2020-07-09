using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET api/user
        [HttpGet("{accelerationName}/{companyId}")]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            var accelerationHasValue = accelerationName != null;
            var companyIdHasValue = companyId != null;
            IList<User> result = new List<User>();

            if (accelerationHasValue == companyIdHasValue)
            {
                return StatusCode(204);
            }
            else if (accelerationHasValue)
            {
                result = _service.FindByAccelerationName(accelerationName);
            }
            else
            {
                result = _service.FindByCompanyId((int)companyId);
            }

            var userDTO = _mapper.Map<IEnumerable<UserDTO>>(result);
            return Ok(userDTO);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var user = _service.FindById(id);

            var result = _mapper.Map<UserDTO>(user);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody] UserDTO value)
        {
            var user = _mapper.Map<User>(value);

            var result = _service.Save(user);

            var userDTO = _mapper.Map<UserDTO>(result);

            return Ok(userDTO);
        }

    }
}
