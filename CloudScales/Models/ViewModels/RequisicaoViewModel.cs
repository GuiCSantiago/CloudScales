using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudScales.Models.ViewModels
{
    public class RequisicaoViewModel
    {
        public class Metadata
        {
        }

        public class AtrasPesoMaior
        {
            public string type { get; set; }
            public bool value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class IdA
        {
            public string type { get; set; }
            public int value { get; set; }
            public Metadata metadata { get; set; } 
        }

        public class IdB
        {
            public string type { get; set; } 
            public int value { get; set; }
            public Metadata metadata { get; set; } 
        }

        public class PesoA
        {
            public string type { get; set; } 
            public float value { get; set; }
            public Metadata metadata { get; set; } 
        }

        public class PesoB
        {
            public string type { get; set; }
            public float value { get; set; }
            public Metadata metadata { get; set; }
        }
        public class Sinc
        {
            public string type { get; set; }
            public int value { get; set; }
            public Metadata metadata { get; set; }
        }

        public class Root
        {
            public string id { get; set; }
            public string type { get; set; } 
            public AtrasPesoMaior AtrasPesoMaior { get; set; } 
            public IdA idA { get; set; } 
            public IdB idB { get; set; }
            public PesoA pesoA { get; set; } 
            public PesoB pesoB { get; set; } 
            public Sinc sinc { get; set; } 
        }
    }
}
