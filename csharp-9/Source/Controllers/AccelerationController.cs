using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*Classe AccelerationController
Deve utilizar o serviço registrado para IAccelerationService
Rota api/acceleration/{id}: deve apontar para o método FindById e retornar um AccelerationDTO
Rota api/acceleration: essa rota deve ter um parâmetro opcional. Quando não informado, deve retornar status 204
parâmetro companyId: deve apontar para o método FindByCompanyId e retornar uma lista de AccelerationDTO
Rota api/acceleration com POST: deve receber um AccelerationDTO, apontar para o método Save e retornar um AccelerationDTO*/
namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccelerationController : ControllerBase
    {
        private IAccelerationService _accelerationService;
        private IMapper _mapper;

        public AccelerationController(IAccelerationService accelerationService, IMapper mapper)
        {
            _accelerationService = accelerationService;
            _mapper = mapper;
        }

        //GET api/acceleration/{id}
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<AccelerationDTO>> Get(int Id)
        {
            var acceleration = _accelerationService.FindById(Id);

            if (acceleration != null)
            {
                var retorno = _mapper.Map<AccelerationDTO>(acceleration);

                return Ok(retorno);

            }
            else
                return NotFound();
        }

        // GET api/acceleration
        [HttpGet]
        public ActionResult<IEnumerable<AccelerationDTO>> GetAll(int? companyId = null)
        {
            if (companyId.HasValue)
            {
                var acceleration = _accelerationService.FindByCompanyId(companyId.Value).ToList();
                var retorno = _mapper.Map<List<AccelerationDTO>>(acceleration);

                return Ok(retorno);
            }

            else
                return NoContent();
        }

        //POST api/acceleration
        [HttpPost]
        public ActionResult<AccelerationDTO> Post([FromBody]AccelerationDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var acceleration = _mapper.Map<Models.Acceleration>(value);

                var retorno = _accelerationService.Save(acceleration);

                return Ok(_mapper.Map<AccelerationDTO>(retorno));
            }
        }

        //PUT api/acceleration/{id_candidate}
        [HttpPut]
        public ActionResult<CandidateDTO> Put([FromBody]CandidateDTO value)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            var acceleration = _mapper.Map<Acceleration>(value);

            var retorno = _accelerationService.Save(acceleration);

            return Ok(_mapper.Map<AccelerationDTO>(retorno));

        }

    }
}
