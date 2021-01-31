using System;
using System.Collections.Generic;

#nullable disable

namespace ClassLibrary1.Models
{
    public partial class TipoConformidade
    {
        public TipoConformidade()
        {
            Conformidades = new HashSet<Conformidade>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Conformidade> Conformidades { get; set; }
    }
}
