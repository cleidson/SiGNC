using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
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
