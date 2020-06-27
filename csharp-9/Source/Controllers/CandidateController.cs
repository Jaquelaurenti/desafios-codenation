using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*Deve utilizar o serviço registrado para ICandidateService
Rota api/candidate/{userId}/{accelerationId}/{companyId}: deve apontar para o método FindById e retornar um CandidateDTO
Rota api/candidate: essa rota deve ter dois parâmetros opcionais. Quando não informados ou quando ambos informados, deve retornar status 204
parâmetro companyId: deve apontar para o método FindByCompanyId e retornar uma lista de CandidateDTO
parâmetro accelerationId: deve apontar para o método FindByAccelerationId e retornar uma lista de CandidateDTO
Rota api/candidate com POST: deve receber um CandidateDTO, apontar para o método Save e retornar um CandidateDTO*/

namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CandidateController : ControllerBase
    {
        private ICandidateService _candidateService;
        private readonly IMapper _mapper;

        public CandidateController(ICandidateService candidateService, IMapper mapper)
        {
            _candidateService = candidateService;
            _mapper = mapper;
        }

        [HttpGet("{userId}/{accelerationId}/{companyId}")]
        public ActionResult<CandidateDTO> Get(int userId, int accelerationId, int companyId)
        {
            var candidate = _candidateService.FindById(userId, accelerationId, companyId);

            if (candidate != null)
            {                
                var retorno = _mapper.Map<CandidateDTO>(candidate);
                return Ok(retorno);

            }
            else
            {
                return NotFound();
            }

        }

        //GET api/candidate/{id}
        [HttpGet]
        public ActionResult<IEnumerable<CandidateDTO>> GetAll(int? accelerationId = null, int? companyId = null)
        {
            if (accelerationId.HasValue && companyId == null)
            {
                var acceleration = _candidateService.FindByAccelerationId(accelerationId.Value).ToList();
                var retorno = _mapper.Map<List<CandidateDTO>>(acceleration);

                return Ok(retorno);
            }
            else if (companyId.HasValue && accelerationId == null)
            {
                return Ok(_candidateService.FindByCompanyId(companyId.Value).
                    Select(x => _mapper.Map<CandidateDTO>(x)).
                    ToList());
            }
            else
            {
                return NoContent();
            }
                
        }

        //POST api/cliente/json do candidato no Body da Requisicao 
        [HttpPost]
        public ActionResult<CandidateDTO> Post([FromBody]CandidateDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var candidate = _mapper.Map<Candidate>(value);

                var retorno = _candidateService.Save(candidate);

                return Ok(_mapper.Map<CandidateDTO>(retorno));

            }
        }

        [HttpPut]
        public ActionResult<CandidateDTO> Put([FromBody]CandidateDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var candidate = _mapper.Map<Candidate>(value);

                var retorno = _candidateService.Save(candidate);

                return Ok(_mapper.Map<CandidateDTO>(retorno));
            }
        }

    }
}