using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.DTOs.Conformidade
{
    public class CausaRaizConformidadeDto
    {
        public int Id { get; set; }
        public bool Ocorreu { get; set; }
        public string Quais { get; set; }
    }
}
