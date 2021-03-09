using System;
using System.Collections.Generic;
using System.Text;

namespace App.Domain.Models
{
    public class Result
    {
        public Guid IdArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string Descricao { get; set; }
    }
}
