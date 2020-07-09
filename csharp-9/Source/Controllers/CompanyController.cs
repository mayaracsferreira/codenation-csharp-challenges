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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{accelerationId}/{userId}")]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            var accelerationHasValue = accelerationId != null;
            var cuserIdHasValue = userId != null;
            IList<Company> result = new List<Company>();

            if (accelerationHasValue == cuserIdHasValue)
            {
                return StatusCode(204);
            }
            else if (accelerationHasValue)
            {
                result = _service.FindByAccelerationId((int)accelerationId);
            }
            else
            {
                result = _service.FindByUserId((int)userId);
            }

            var modelDTO = _mapper.Map<IEnumerable<CompanyDTO>>(result);
            return Ok(modelDTO);
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var model = _service.FindById(id);

            var result = _mapper.Map<CompanyDTO>(model);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody] CompanyDTO value)
        {
            var model = _mapper.Map<Company>(value);

            var result = _service.Save(model);

            var modelDTO = _mapper.Map<CompanyDTO>(result);

            return Ok(modelDTO);
        }
    }
}
