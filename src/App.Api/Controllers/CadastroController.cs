using App.Application.Interfaces;
using App.Domain.Models;
using App.Domain.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace App.Api.Controllers
{
    [ApiController]
    [Route("api/cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _cadastroService;
        private readonly IMapper _mapper;
        public CadastroController(ICadastroService cadastroService, IMapper mapper)
        {
            _cadastroService = cadastroService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<CadastroViewModel>> Adicionar([FromBody] CadastroViewModel cadastroViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                    return StatusCode((int)HttpStatusCode.PreconditionFailed, messages);
                }

                var result = await _cadastroService.SalvaImagem(_mapper.Map<Arquivo>(cadastroViewModel));

                return StatusCode((int)HttpStatusCode.Created, result);
            }

            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
