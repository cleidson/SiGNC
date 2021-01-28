using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class AcaoCorretivaConformidade
    {
        public int Id { get; set; }
        public int? TipoAcaoId { get; set; }
        public string ResponsavelId { get; set; }
        public int? ConformidadeId { get; set; }
        public string Descricao { get; set; }
        public string RiscoOportunidade { get; set; }
        public DateTime? DataLimite { get; set; }

        public virtual Conformidade Conformidade { get; set; }
        public virtual AspNetUser Responsavel { get; set; }
        public virtual TipoAcao TipoAcao { get; set; }
    }
}
