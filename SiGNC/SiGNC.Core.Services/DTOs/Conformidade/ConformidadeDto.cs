using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.DTOs.Conformidade
{
    public class ConformidadeDto
    {
        public int Id { get; set; }
        public string NumeroConformidade { get; set; }
        public string UsuarioSolicitanteId { get; set; }
        public string UsuarioGestorId { get; set; }
        public string TipoConformidadeId { get; set; }
        public string ReincidenciaConformidadePaiId { get; set; }
        public string Reincidente { get; set; }
        public string Requisito { get; set; }
        public string DataEmissao { get; set; }
        public string StatusConformidadeId { get; set; }
        public string OrigemConformidadeId { get; set; }
        public ConformidadeDto ReincidenciaConformidadePai { get; set; }
        public UsuarioDto UsuarioSolicitante { get; set; }
        public UsuarioDto UsuarioGestor { get; set; }
        public StatusConformidadeDto StatusConformidade { get; set; }
        public OrigemDto OrigemConformidade { get; set; } 
        public List<DetalhaConformidadeDto> Detalhamentos { get; set; }
        public AcaoCorretivaConformidadeDto AcaoCorretiva { get; set; }
        public  List<CausaRaizConformidadeDto> CausaRaizes { get; set; }
    }
}
