using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.ViewModels
{
    public class ClienteViewModel : PadraoViewModel
    {
        public string Nome { get; set; }

        public int CNPJ { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }
    }
}
