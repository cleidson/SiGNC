using System;
using System.Collections.Generic;

#nullable disable

namespace SiGNC.Infra.Data.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AcaoCorretivaConformidades = new HashSet<AcaoCorretivaConformidade>();
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserRoles = new HashSet<AspNetUserRole>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            ConformidadeUsuarioGestors = new HashSet<Conformidade>();
            ConformidadeUsuarioSolicitantes = new HashSet<Conformidade>();
            ImplantarConformidades = new HashSet<ImplantarConformidade>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public string Nome { get; set; }
        public string Sobrenome { get; set; }

        public virtual ICollection<AcaoCorretivaConformidade> AcaoCorretivaConformidades { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserRole> AspNetUserRoles { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Conformidade> ConformidadeUsuarioGestors { get; set; }
        public virtual ICollection<Conformidade> ConformidadeUsuarioSolicitantes { get; set; }
        public virtual ICollection<ImplantarConformidade> ImplantarConformidades { get; set; }
    }
}
