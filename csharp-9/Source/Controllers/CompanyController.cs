using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*Deve utilizar o serviço registrado para ICompanyService
Rota api/company/{id}: deve apontar para o método FindById e retornar um CompanyDTO
Rota api/company: essa rota deve ter dois parâmetros opcionais. Quando não informados ou quando ambos informados, deve retornar status 204
parâmetro accelerationId: deve apontar para o método FindByAccelerationIde retornar uma lista de CompanyDTO
parâmetro userId: deve apontar para o método FindByUserId e retornar uma lista de CompanyDTO
Rota api/company com POST: deve receber um CompanyDTO, apontar para o método Save e retornar um CompanyDTO*/
namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {

        private ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> Get(int id)
        {
            var company = _companyService.FindById(id);

            if (company != null)
            {
                var retorno = _mapper.Map<CompanyDTO>(company);

                return Ok(retorno);
            }

            else
                return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CompanyDTO>> GetAll(int? accelerationId = null, int? userId = null)
        {

            if (accelerationId.HasValue && userId == null)
            {
                var acceleration = _companyService.FindByAccelerationId(accelerationId.Value).ToList();
                return Ok(_mapper.Map<List<CompanyDTO>>(acceleration));
            }

            else if (userId.HasValue && accelerationId == null)
            {
                return Ok(_companyService.FindByUserId(userId.Value)
                                         .Select(x => _mapper.Map<CompanyDTO>(x))
                                         .ToList());
            }

            else
            {
                return NoContent();
            }
                
        }

        [HttpPost]
        public ActionResult<CompanyDTO> Post([FromBody]CompanyDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var companies = _mapper.Map<Company>(value);

                var retorno = _companyService.Save(companies);

                return Ok(_mapper.Map<CompanyDTO>(retorno));
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
                var company = _mapper.Map<Company>(value);

                var retorno = _companyService.Save(company);

                return Ok(_mapper.Map<CompanyDTO>(retorno));
            }
        }
    }
}

