using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class RequisicaoViewModel
{
    public class Metadata
    {
    }

    public class AtrasPesoMaior
    {
        public string Type { get; set; }
        public bool Value { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class IdA
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class IdB
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class PesoA
    {
        public string Type { get; set; }
        public float Value { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class PesoB
    {
        public string Type { get; set; }
        public float Value { get; set; }
        public Metadata Metadata { get; set; }
    }
    public class Sinc
    {
        public string Type { get; set; }
        public int Value { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class Root
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public AtrasPesoMaior AtrasPesoMaior { get; set; }
        public IdA IdA { get; set; }
        public IdB IdB { get; set; }
        public PesoA PesoA { get; set; }
        public PesoB PesoB { get; set; }
        public Sinc Sinc { get; set; }

    }
}

