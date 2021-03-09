using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Data.Interfaces
{
    public interface IAwsRepository
    {
        Task<string> SalvaArquivo(Arquivo arquivo);
        void EnviaMensagemAsync(MensagemArquivo mensagem);
    }
}
