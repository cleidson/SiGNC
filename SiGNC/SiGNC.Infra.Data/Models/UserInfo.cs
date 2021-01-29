using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Infra.Data.Models
{
    public class UserInfo
    {
        public string Name { get; set; } 
        public string Email { get; set; }
        public string Password { get; set; }
        public string NormalizedUserName { get; set; }
        public string Role { get; set; }
    }
}
