using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary1.Models
{
    public partial class ConformidadeHasCausaRaiz
    {
        public int Id { get; set; }
        public int? ConformidadeId { get; set; }
        public int? CausaRaizConformidadeId { get; set; }
        public string Quais { get; set; }
        public bool? Ocorreu { get; set; }

        public virtual CausaRaizConformidade CausaRaizConformidade { get; set; }
        public virtual Conformidade Conformidade { get; set; }
    }
}
