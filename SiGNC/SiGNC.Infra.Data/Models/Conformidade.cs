using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class Conformidade
    {
        public Conformidade()
        {
            AcaoCorretivaConformidades = new HashSet<AcaoCorretivaConformidade>();
            ConformidadeHasCausaRaizs = new HashSet<ConformidadeHasCausaRaiz>();
            DetalhaConformidades = new HashSet<DetalhaConformidade>();
            DocumentoConformidades = new HashSet<DocumentoConformidade>();
            ImplantarConformidades = new HashSet<ImplantarConformidade>();
            InverseReincidenciaConformidadePai = new HashSet<Conformidade>();
        }

        public int Id { get; set; }
        public string NumeroConformidade { get; set; }
        public string UsuarioSolicitanteId { get; set; }  
        public string UsuarioGestorId { get; set; }
        public int? OrigemConformidadeId { get; set; }
        public int? TipoConformidadeId { get; set; }
        public int StatusConformidadeId { get; set; }
        public string Reincidente { get; set; } 
        public int? ReincidenciaConformidadePaiId { get; set; }
        public string Requisito { get; set; }
        public DateTime? DataCadastro { get; set; } 
        public virtual OrigemConformidade OrigemConformidade { get; set; }
        public virtual Conformidade ReincidenciaConformidadePai { get; set; }
        public virtual TipoConformidade TipoConformidade { get; set; }
 
        public virtual ApplicationUser UsuarioGestor { get; set; }
        public virtual StatusConformidade StatusConformidade { get; set; }
       
        public virtual ApplicationUser UsuarioSolicitante { get; set; }
        public virtual ICollection<AcaoCorretivaConformidade> AcaoCorretivaConformidades { get; set; }
        public virtual ICollection<ConformidadeHasCausaRaiz> ConformidadeHasCausaRaizs { get; set; }
        public virtual ICollection<DetalhaConformidade> DetalhaConformidades { get; set; }
        public virtual ICollection<DocumentoConformidade> DocumentoConformidades { get; set; }
        public virtual ICollection<ImplantarConformidade> ImplantarConformidades { get; set; }
        public virtual ICollection<Conformidade> InverseReincidenciaConformidadePai { get; set; } 
    }
}
