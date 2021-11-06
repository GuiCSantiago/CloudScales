using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.ViewModels
{
    public class CelulaViewModel : PadraoViewModel
    {
        public int EquipamentoID { get; set; }

        public double Peso { get; set; }

        public int Posicao { get; set; }

    }
}
