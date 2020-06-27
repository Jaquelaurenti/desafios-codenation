using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*
 * Deve utilizar o serviço registrado para IChallengeService
Rota api/challenge: essa rota deve ter dois parâmetros opcionais. Quando os dois não forem informados, deve retornar status 204
parâmetros accelerationId e userId: deve apontar para o método FindByAccelerationIdAndUserId e retornar uma lista de ChallengeDTO
Rota api/challenge com POST: deve receber um ChallengeDTO, apontar para o método Save e retornar um ChallengeDTO
*/

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private IChallengeService _challengeService;
        private readonly IMapper _mapper;

        public ChallengeController(IChallengeService challengeService, IMapper mapper)
        {
            _challengeService = challengeService;
            _mapper = mapper;
        }

        // GET/api/Challenge
        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {
            if (accelerationId.HasValue && userId.HasValue)
            {
                var challenge = _challengeService.FindByAccelerationIdAndUserId(accelerationId.Value, userId.Value)
                                                 .ToList();
                var retorno = _mapper.Map<List<ChallengeDTO>>(challenge);

                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
                
        }


        [HttpPost]
        public ActionResult<ChallengeDTO> Post([FromBody]ChallengeDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var challenge = _mapper.Map<Models.Challenge>(value);

                var retorno = _challengeService.Save(challenge);

                return Ok(_mapper.Map<ChallengeDTO>(retorno));
            }
        }

    }
}