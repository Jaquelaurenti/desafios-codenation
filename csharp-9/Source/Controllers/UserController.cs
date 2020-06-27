using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Codenation.Challenge.DTOs;
using Codenation.Challenge.Models;
using Codenation.Challenge.Services;
using Microsoft.AspNetCore.Mvc;

/*
 Deve utilizar o serviço registrado para IUserService
Rota api/user: essa rota deve ter dois parâmetros opcionais. Quando não informados ou quando ambos informados, deve retornar status 204
parâmetro accelerationName: deve apontar para o método FindByAccelerationName e retornar uma lista de UserDTO
parâmetro companyId: deve apontar para o método FindByCompanyId e retornar uma lista de UserDTO
Rota api/user/{id}: deve apontar para o método FindById e retornar um UserDTO
Rota api/user com POST: deve receber um UserDTO, apontar para o método Save e retornar um UserDTO
*/
namespace Codenation.Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _userService = service;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult<UserDTO> Get(int id)
        {
            var users = _userService.FindById(id);


            if (users != null)
            {
                var retorno = _mapper.Map<UserDTO>(users);
                return Ok(retorno);
            }
            else
            {
                return NoContent();
            }
                
        }

        // GET api/user
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetAll(string accelerationName = null, int? companyId = null)
        {
            if (accelerationName != null && companyId == null)
            {
                var acceleration = _userService.FindByAccelerationName(accelerationName)
                                               .ToList();
                var retorno = _mapper.Map<List<UserDTO>>(acceleration);

                return Ok(retorno);
            }

            else if (companyId.HasValue && accelerationName == null)
            {
                return Ok(_userService.FindByCompanyId(companyId.Value)
                                       .Select(x => _mapper.Map<UserDTO>(x))
                                       .ToList());
            }
            else
            {
                return NoContent();
            }
                
        }

        // POST api/user
        [HttpPost]
        public ActionResult<UserDTO> Post([FromBody]UserDTO value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user = _mapper.Map<User>(value);
                var retorno = _userService.Save(user);

                return Ok(_mapper.Map<UserDTO>(retorno));
            }
        }
    }
}