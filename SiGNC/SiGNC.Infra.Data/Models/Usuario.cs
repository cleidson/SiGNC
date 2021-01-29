using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Infra.Data.Models
{
    public class Usuario
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; internal set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }

        public Perfil Perfil { get; set; }
    }
}
