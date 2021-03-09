using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Domain.ViewModels
{
    public class CadastroViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ArquivoUpload  { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ArquivoNome { get; set; }
    }
}
