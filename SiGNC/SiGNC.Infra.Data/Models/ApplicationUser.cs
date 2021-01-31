 
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Infra.Data.Models
{ 
    public class ApplicationUser : IdentityUser
    { 
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public ApplicationUser()
        {
            AcaoCorretivaConformidades = new HashSet<AcaoCorretivaConformidade>(); 
            ConformidadeUsuarioGestors = new HashSet<Conformidade>();
            ConformidadeUsuarioSolicitantes = new HashSet<Conformidade>();
            ImplantarConformidades = new HashSet<ImplantarConformidade>();
        }

        public virtual ICollection<Conformidade> ConformidadeUsuarioGestors { get; set; }
        public virtual ICollection<Conformidade> ConformidadeUsuarioSolicitantes { get; set; } 
        public virtual ICollection<AcaoCorretivaConformidade> AcaoCorretivaConformidades { get; set; }
        public virtual ICollection<ImplantarConformidade> ImplantarConformidades { get; set; }


    }
}
