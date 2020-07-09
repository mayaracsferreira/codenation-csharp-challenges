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
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _service;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("higherScore")]
        public ActionResult<decimal> GetHigherScore(int? challengeId = null)
        {
            var challengeHasValue = challengeId != null;            

            if (challengeHasValue)
            {
                var result = _service.FindHigherScoreByChallengeId((int)challengeId);
                
                return Ok(result);
            }
            else
            {
                return StatusCode(204);
            }
        }

        [HttpGet("{challengeId}/{accelerationId}")]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId = null, int? accelerationId = null)
        {
            var challengeHasValue = challengeId != null;
            var accelarationHasValue = accelerationId != null;

            if (challengeHasValue && accelarationHasValue)
            {
                var result = _service.FindByChallengeIdAndAccelerationId((int)challengeId, (int)accelerationId);

                var modelDTO = _mapper.Map<IEnumerable<SubmissionDTO>>(result);
                return Ok(modelDTO);
            }
            else
            {
                return StatusCode(204);
            }
        }


        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody] SubmissionDTO value)
        {
            var model = _mapper.Map<Submission>(value);

            var result = _service.Save(model);

            var modelDTO = _mapper.Map<SubmissionDTO>(result);

            return Ok(modelDTO);
        }


    }
}
