using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Autenticacao
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
