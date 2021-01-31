using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.DTOs.Conformidade
{
    public class AcaoCorretivaConformidadeDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string RiscoOportunidade { get; set; }
        public string DataImplantacao { get; set; }
        public UsuarioDto Responsavel { get; set; }
    }
}
