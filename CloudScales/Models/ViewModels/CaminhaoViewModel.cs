using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.ViewModels
{
    public class CaminhaoViewModel : PadraoViewModel
    {
        public int ClienteID { get; set; }

        public string Placa { get; set; }

        public string Carreta { get; set; }
    }
}
