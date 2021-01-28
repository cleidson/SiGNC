using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class TipoAcao
    {
        public TipoAcao()
        {
            AcaoCorretivaConformidades = new HashSet<AcaoCorretivaConformidade>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<AcaoCorretivaConformidade> AcaoCorretivaConformidades { get; set; }
    }
}
