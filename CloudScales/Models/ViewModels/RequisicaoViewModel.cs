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
            public string type { get; set; } = "Boolean";
            public bool value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }

        public class IdA
        {
            public string type { get; set; } = "Number";
            public int value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }

        public class IdB
        {
            public string type { get; set; } = "Number";
            public int value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }

        public class PesoA
        {
            public string type { get; set; } = "Number";
            public float value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }

        public class PesoB
        {
            public string type { get; set; } = "Number";
            public float value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }
        public class Sinc
        {
            public string type { get; set; } = "Number";
            public int value { get; set; }
            public Metadata metadata { get; set; } = new Metadata();
        }

        public class Root
        {
            public string id { get; set; }
            public string type { get; set; } = "iot";
            public AtrasPesoMaior AtrasPesoMaior { get; set; } = new AtrasPesoMaior { value = false };
            public IdA idA { get; set; } = new IdA();
            public IdB idB { get; set; } = new IdB();
            public PesoA pesoA { get; set; } = new PesoA();
            public PesoB pesoB { get; set; } = new PesoB();
            public Sinc sinc { get; set; } = new Sinc();
        }
    }
}
