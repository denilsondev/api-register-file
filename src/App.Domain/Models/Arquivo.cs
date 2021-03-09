using System;

namespace App.Domain.Models
{
    public class Arquivo
    {
        public Guid IdArquivo { get; set; }
        public string ArquivoUpload { get; set; }
        public string ArquivoNome { get; set; }
    }
}
