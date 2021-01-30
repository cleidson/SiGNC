using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Conformidade
{
    public class ConformidadeViewModel
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
        public ConformidadePaiViewModel ReincidenciaConformidadePai { get; set; }
        /// <summary>
        /// UsuarioSolicitante
        /// </summary>
        public UsuarioViewModel Eminente { get; set; }
        public UsuarioViewModel UsuarioGestor { get; set; }
        public StatusConformidadeViewModel StatusConformidade { get; set; }
        public OrigemConformidadeViewModel OrigemConformidade { get; set; }
        public List<DetalhaConformidadeViewModel> Detalhamentos { get; set; }
        public AcaoCorretivaViewModel AcaoCorretiva { get; set; }
        public List<CausaRaizHasConformidadeViewModel> CausaRaizes { get; set; } 
    }
}
