using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Infra.Data.Dtos.Authorization
{
    public class AuthenticateRequestDto
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Senha { get; set; }
    }
}
