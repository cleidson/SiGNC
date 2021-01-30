using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class StatusConformidade
    {
        public StatusConformidade()
        {
            ImplantarConformidades = new HashSet<ImplantarConformidade>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<ImplantarConformidade> ImplantarConformidades { get; set; }
    }
}
