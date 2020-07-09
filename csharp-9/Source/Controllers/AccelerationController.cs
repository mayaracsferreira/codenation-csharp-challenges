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

    public class AccelerationController : ControllerBase
    {
        private readonly IAccelerationService _service;
        private readonly IMapper _mapper;

        public AccelerationController(IAccelerationService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll([FromBody]int? companyId = null)
        {
            var companyHasValue = companyId != null;            

            if (companyHasValue)
            {
                var result = _service.FindByCompanyId((int)companyId);
                var modelDTO = _mapper.Map<IEnumerable<AccelerationDTO>>(result);
                return Ok(modelDTO);
            }
            else
            {
                return StatusCode(204);                
            }
        }

        [HttpGet("{id}")]
        public ActionResult<AccelerationDTO> Get(int id)
        {
            var model = _service.FindById(id);

            var result = _mapper.Map<AccelerationDTO>(model);

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
        public ActionResult<AccelerationDTO> Post([FromBody] AccelerationDTO value)
        {
            var model = _mapper.Map<Acceleration>(value);

            var result = _service.Save(model);

            var modelDTO = _mapper.Map<AccelerationDTO>(result);

            return Ok(modelDTO);
        }
    }
}
