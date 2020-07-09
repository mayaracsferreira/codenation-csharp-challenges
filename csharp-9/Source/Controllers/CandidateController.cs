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
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _service;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{companyId}/{accelerationId}")]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? companyId = null, int? accelerationId = null)
        {
            var accelerationHasValue = accelerationId != null;
            var companyIdHasValue = companyId != null;
            IList<Candidate> result = new List<Candidate>();

            if (accelerationHasValue == companyIdHasValue)
            {
                return StatusCode(204);
            }
            else if (accelerationHasValue)
            {
                result = _service.FindByAccelerationId((int)accelerationId);
            }
            else
            {
                result = _service.FindByCompanyId((int)companyId);
            }

            if (result != null)
            {
                var modelDTO = _mapper.Map<IEnumerable<CandidateDTO>>(result);
                return Ok(modelDTO);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get([FromRoute] int userId, int accelerationId, int companyId)
        {
            var model = _service.FindById(userId, accelerationId, companyId);

            var result = _mapper.Map<CandidateDTO>(model);

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
        public ActionResult<CandidateDTO> Post([FromBody] CandidateDTO value)
        {
            var model = _mapper.Map<Candidate>(value);

            var result = _service.Save(model);

            var modelDTO = _mapper.Map<CandidateDTO>(result);

            return Ok(modelDTO);
        }
    }
}
