using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*Deve utilizar o serviço registrado para ISubmissionService
Rota api/submission/higherScore: essa rota deve ter um parâmetro e caso não seja fornecido deve retornar status 204
parâmetro challengeId: deve apontar para o método FindHigherScoreByChallengeId e retornar o valor do maior score
Rota api/submission: essa rota deve ter dois parâmetros opcionais. Quando os dois não forem informados, deve retornar status 204
parâmetros challengeId e accelerationId: deve apontar para o método FindByChallengeIdAndAccelerationId e retornar uma lista de SubmissionDTO
Rota api/submission com POST: deve receber um SubmissionDTO, apontar para o método Save e retornar um SubmissionDTO*/

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {

        private ISubmissionService _submissionService;
        private readonly IMapper _mapper;

        public SubmissionController(ISubmissionService submissionService, IMapper mapper)
        {
            _submissionService = submissionService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubmissionDTO>> GetAll(int? challengeId, int? accelerationId)
        {

            if (challengeId.HasValue && accelerationId.HasValue)
            {
                var submissions = _submissionService.FindByChallengeIdAndAccelerationId(challengeId.Value, accelerationId.Value);
                return Ok(_mapper.Map<List<SubmissionDTO>>(submissions));
            }
            else
            {
                // nao encontrado
                return NoContent();
            }   
        }


        [HttpGet("higherScore")]
        public ActionResult<decimal> GetHigherScore(int challengeId)
        {
            return Ok(_submissionService.FindHigherScoreByChallengeId(challengeId));
        }


        [HttpPost]
        public ActionResult<SubmissionDTO> Post([FromBody]SubmissionDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var submission = _mapper.Map<Models.Submission>(value);

                var retorno = _submissionService.Save(submission);

                return Ok(_mapper.Map<SubmissionDTO>(retorno));

            }
        }

        [HttpPut]
        public ActionResult<CompanyDTO> Put([FromBody]CompanyDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var submission = _mapper.Map<Submission>(value);

                var retorno = _submissionService.Save(submission);

                return Ok(_mapper.Map<SubmissionDTO>(retorno));

            }
        }
    }
}