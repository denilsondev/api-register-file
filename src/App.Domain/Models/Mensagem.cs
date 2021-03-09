using System;

namespace App.Domain.Models
{
    public class MensagemArquivo
    {
        public Guid IdArquivo { get; set; }
        public string NomeArquivo { get; set; }
        public string Descricao { get; set; }

        public string CriaDescricaoMensagem()
        {
            return $"{IdArquivo} - {NomeArquivo} - gravado com sucesso.";
        }
    }
}