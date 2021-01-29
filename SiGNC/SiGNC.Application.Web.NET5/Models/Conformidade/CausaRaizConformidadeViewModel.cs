using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Conformidade
{
    public class CausaRaizConformidadeViewModel
    {
        public int Id { get; set; }
        public bool Ocorreu { get; set; }
        public string Quais { get; set; }
    }
}
