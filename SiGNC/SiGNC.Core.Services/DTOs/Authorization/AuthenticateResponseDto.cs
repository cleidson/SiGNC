using SiGNC.Infra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.DTOs.Authorization
{

    public class AuthenticateResponseDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationToken { get; set; }
        public AuthenticateResponseDto(Usuario user, string token, DateTime expirationToken)
        {
            Id = user.Id;
            Nome = user.Nome;
            SobreNome = user.SobreNome;
            Email = user.Email;
            Token = token;
            ExpirationToken = expirationToken;
        }
    }
}
