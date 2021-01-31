using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary1.Models
{
    public partial class CausaRaizConformidade
    {
        public CausaRaizConformidade()
        {
            ConformidadeHasCausaRaizs = new HashSet<ConformidadeHasCausaRaiz>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<ConformidadeHasCausaRaiz> ConformidadeHasCausaRaizs { get; set; }
    }
}
