using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class DetalhaConformidade
    {
        public int Id { get; set; }
        public int? ConformidadeId { get; set; }
        public string Descricao { get; set; }
        public string Abrangencia { get; set; }

        public virtual Conformidade Conformidade { get; set; }
    }
}
