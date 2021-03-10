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
    public class CadastroController : ControllerBase
    {
        private readonly ICadastroService _cadastroService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public CadastroController(ICadastroService cadastroService, IMapper mapper, ILogger<CadastroController> logger )
        {
            _cadastroService = cadastroService;
            _mapper = mapper;
            _logger = logger;
        }

        [Route(""), HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public RedirectResult RedirectToSwaggerUi()
        {
            return Redirect("/swagger/");
        }


        [Route("api/cadastro")]
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

                _logger.LogInformation($"Arquivo {result.IdArquivo} salvo com sucesso.");
                return StatusCode((int)HttpStatusCode.Created, result);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}
