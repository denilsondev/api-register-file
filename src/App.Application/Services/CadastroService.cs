using App.Application.Interfaces;
using App.Data.Interfaces;
using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class CadastroService : ICadastroService
    {
        private readonly IAwsRepository _awsRepository;
        public CadastroService(IAwsRepository awsRepository)
        {
            _awsRepository = awsRepository;
        }

        public async Task<Result> SalvaImagem(Arquivo arquivo)
        {
            arquivo.IdArquivo = Guid.NewGuid();

            var retorno = await _awsRepository.SalvaArquivo(arquivo);

            Result result = new Result()
            {
                IdArquivo = arquivo.IdArquivo,
                NomeArquivo = arquivo.ArquivoNome,
                Descricao = retorno
            };

            return result;
        }
    }
}
