using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Conformidade
{
    public class ImplantacaoConformidadeViewModel
    {
        public int ConformidadeId { get; set; }
        public int StatusConformidadeId { get; set; }
        public string Descricao { get; set; }
        public int ResponsavelId { get; set; }
        public ConformidadeViewModel Conformidade { get; set; }
        public StatusConformidadeViewModel StatusConformidade { get; set; }
        public UsuarioViewModel Responsavel { get; set; }  
    }
}
