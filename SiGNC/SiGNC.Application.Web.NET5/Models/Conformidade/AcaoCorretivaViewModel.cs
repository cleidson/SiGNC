using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Conformidade
{
    public class AcaoCorretivaViewModel
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string DataImplantacao { get; set; }
        public UsuarioViewModel Responsavel { get; set; }
    }
}
