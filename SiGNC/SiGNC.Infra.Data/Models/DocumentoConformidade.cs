using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class DocumentoConformidade
    {
        public int Id { get; set; }
        public int? ConformidadeId { get; set; }
        public string Nome { get; set; }
        public string PathDocumento { get; set; }
        public string DocumentoBase { get; set; }

        public virtual Conformidade Conformidade { get; set; }
    }
}
