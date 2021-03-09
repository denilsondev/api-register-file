using App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Interfaces
{
    public interface ICadastroService
    {
        Task<Result> SalvaImagem(Arquivo arquivo);
    }
}
