using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Conformidade
{
    public class CausaRaizHasConformidadeViewModel
    {
        public int Id { get; set; }
        public int ConformidadeId { get; set; }
        public int CausaRaizConformidadeId { get; set; }
        public string Ocorreu { get; set; }
        public string Quais { get; set; }
    }
}
