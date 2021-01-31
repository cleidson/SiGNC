using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class ImplantarConformidade
    {
        public int Id { get; set; }
        public int? ConformidadeId { get; set; }
        public int? StatusConformidadeId { get; set; }
        public string Descricao { get; set; }
        public string ResponsavelId { get; set; }

        public virtual Conformidade Conformidade { get; set; }
        public virtual ApplicationUser Responsavel { get; set; }
        //public virtual StatusConformidade StatusConformidade { get; set; }
    }
}
