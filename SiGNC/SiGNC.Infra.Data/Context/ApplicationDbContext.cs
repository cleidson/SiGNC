using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiGNC.Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Infra.Data.Context
{ 

    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public virtual DbSet<AcaoCorretivaConformidade> AcaoCorretivaConformidades { get; set; }
        public virtual DbSet<ApplicationUser> AspNetUsers { get; set; }

        public virtual DbSet<CausaRaizConformidade> CausaRaizConformidades { get; set; }
        public virtual DbSet<Conformidade> Conformidades { get; set; }
        public virtual DbSet<ConformidadeHasCausaRaiz> ConformidadeHasCausaRaizs { get; set; }
        public virtual DbSet<DetalhaConformidade> DetalhaConformidades { get; set; }
        public virtual DbSet<DocumentoConformidade> DocumentoConformidades { get; set; }
        public virtual DbSet<ImplantarConformidade> ImplantarConformidades { get; set; }
        public virtual DbSet<OrigemConformidade> OrigemConformidades { get; set; }
        public virtual DbSet<StatusConformidade> StatusConformidades { get; set; }
        public virtual DbSet<TipoAcao> TipoAcaos { get; set; }
        public virtual DbSet<TipoConformidade> TipoConformidades { get; set; }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-LCF2NM6;Database=SiGNC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");
 


            modelBuilder.Entity<AcaoCorretivaConformidade>(entity =>
            { 
                entity.ToTable("AcaoCorretivaConformidade");

                entity.Property(e => e.DataLimite).HasColumnType("datetime");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsavelId).HasMaxLength(450);

                entity.Property(e => e.RiscoOportunidade)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Conformidade)
                    .WithMany(p => p.AcaoCorretivaConformidades)
                    .HasForeignKey(d => d.ConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AcaoCorretivaConformidade_Conformidade");

                entity.HasOne(d => d.Responsavel)
                    .WithMany(p => p.AcaoCorretivaConformidades)
                    .HasForeignKey(d => d.ResponsavelId)
                    .HasConstraintName("FK_AcaoCorretivaConformidade_AspNetUsers");

                entity.HasOne(d => d.TipoAcao)
                    .WithMany(p => p.AcaoCorretivaConformidades)
                    .HasForeignKey(d => d.TipoAcaoId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_AcaoCorretivaConformidade_TipoAcao");
            });

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                 

                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId, "IX_AspNetUserRoles_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<CausaRaizConformidade>(entity =>
            {
                entity.ToTable("CausaRaizConformidade");

                entity.Property(e => e.Descricao) 
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome) 
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Conformidade>(entity =>
            {
                entity.ToTable("Conformidade");

                entity.Property(e => e.DataCadastro).HasColumnType("datetime");

                entity.Property(e => e.Reincidente)
                     .HasMaxLength(255)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Requisito)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UsuarioGestorId).HasMaxLength(450);

                entity.Property(e => e.UsuarioSolicitanteId).HasMaxLength(450);
                entity.Property(e => e.NumeroConformidade).HasMaxLength(255);

                entity.HasOne(d => d.OrigemConformidade)
                    .WithMany(p => p.Conformidades)
                    .HasForeignKey(d => d.OrigemConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Conformidade_OrigemConformidade");

                entity.HasOne(d => d.ReincidenciaConformidadePai)
                    .WithMany(p => p.InverseReincidenciaConformidadePai)
                    .HasForeignKey(d => d.ReincidenciaConformidadePaiId)
                    .HasConstraintName("FK_Conformidade_Conformidade");

                entity.HasOne(d => d.TipoConformidade)
                    .WithMany(p => p.Conformidades)
                    .HasForeignKey(d => d.TipoConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Conformidade_TipoConformidade");

                entity.HasOne(d => d.UsuarioGestor)
                    .WithMany(p => p.ConformidadeUsuarioGestors)
                    .HasForeignKey(d => d.UsuarioGestorId)
                    .HasConstraintName("FK_Conformidade_AspNetUsers1");

                entity.HasOne(d => d.UsuarioSolicitante)
                    .WithMany(p => p.ConformidadeUsuarioSolicitantes)
                    .HasForeignKey(d => d.UsuarioSolicitanteId)
                    .HasConstraintName("FK_Conformidade_AspNetUsers");

                    entity.HasOne(d => d.StatusConformidade)
                    .WithMany(p => p.Conformidades)
                    .HasForeignKey(d => d.StatusConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_Conformidade_StatusConformidade");
            });

            modelBuilder.Entity<ConformidadeHasCausaRaiz>(entity =>
            {
                entity.ToTable("ConformidadeHasCausaRaiz");

                entity.Property(e => e.Quais)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.CausaRaizConformidade)
                    .WithMany(p => p.ConformidadeHasCausaRaizs)
                    .HasForeignKey(d => d.CausaRaizConformidadeId)
                    .HasConstraintName("FK_ConformidadeHasCausaRaiz_CausaRaizConformidade");

                entity.HasOne(d => d.Conformidade)
                    .WithMany(p => p.ConformidadeHasCausaRaizs)
                    .HasForeignKey(d => d.ConformidadeId)
                    .HasConstraintName("FK_ConformidadeHasCausaRaiz_Conformidade");
            });

            modelBuilder.Entity<DetalhaConformidade>(entity =>
            {
                entity.ToTable("DetalhaConformidade");

                entity.Property(e => e.Abrangencia)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Descricao)
                    .HasMaxLength(155)
                    .IsUnicode(false);

                entity.HasOne(d => d.Conformidade)
                    .WithMany(p => p.DetalhaConformidades)
                    .HasForeignKey(d => d.ConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DetalhaConformidade_Conformidade");
            });

            modelBuilder.Entity<DocumentoConformidade>(entity =>
            {
                entity.ToTable("DocumentoConformidade");

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PathDocumento)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Conformidade)
                    .WithMany(p => p.DocumentoConformidades)
                    .HasForeignKey(d => d.ConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_DocumentoConformidade_Conformidade");
            });

            modelBuilder.Entity<ImplantarConformidade>(entity =>
            {
                entity.ToTable("ImplantarConformidade");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ResponsavelId).HasMaxLength(450);

                entity.HasOne(d => d.Conformidade)
                    .WithMany(p => p.ImplantarConformidades)
                    .HasForeignKey(d => d.ConformidadeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_ImplantacaoConformidade_Conformidade");

                entity.HasOne(d => d.Responsavel)
                    .WithMany(p => p.ImplantarConformidades)
                    .HasForeignKey(d => d.ResponsavelId)
                    .HasConstraintName("FK_ImplantarConformidade_AspNetUsers");

                //entity.HasOne(d => d.StatusConformidade)
                //    .WithMany(p => p.ImplantarConformidades)
                //    .HasForeignKey(d => d.StatusConformidadeId)
                //    .OnDelete(DeleteBehavior.Cascade)
                //    .HasConstraintName("FK_ImplantacaoConformidade_StatusConformidade");
            });

            modelBuilder.Entity<OrigemConformidade>(entity =>
            {
                entity.ToTable("OrigemConformidade");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StatusConformidade>(entity =>
            {
                entity.ToTable("StatusConformidade");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoAcao>(entity =>
            {
                entity.ToTable("TipoAcao");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });



            modelBuilder.Entity<TipoConformidade>(entity =>
            {
                entity.ToTable("TipoConformidade");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Nome)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });



            base.OnModelCreating(modelBuilder);
        } 
    }

     
}
