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
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _service;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("{accelerationId}/{userId}")]
        public ActionResult<IEnumerable<ChallengeDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            var accelerationHasValue = accelerationId != null;
            var cuserIdHasValue = userId != null;
            IList<Models.Challenge> result = new List<Models.Challenge>();

            if (accelerationHasValue && cuserIdHasValue)
            {
                result = _service.FindByAccelerationIdAndUserId((int)accelerationId, (int)userId);
                var modelDTO = _mapper.Map<IEnumerable<ChallengeDTO>>(result);
                return Ok(modelDTO);
            }
            else 
            {
                return StatusCode(204);
            }
        }

        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody] ChallengeDTO value)
        {
            var model = _mapper.Map<Models.Challenge>(value);

            var result = _service.Save(model);

            var modelDTO = _mapper.Map<ChallengeDTO>(result);

            return Ok(modelDTO);
        }

    }
}
