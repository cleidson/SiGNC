using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.DTOs.Conformidade
{
    public class ConformidadeHasCausaRaizDto
    {
        public int Id { get; set; }
        public int ConformidadeId { get; set; }
        public int CausaRaizConformidadeId { get; set; }
        public string CausaRaizDescricao { get; set; }
        public int CausaRaizId { get; set; }
        public bool Ocorreu { get; set; }
        public string OcorreuFormated { get; set; }
        public string Quais { get; set; }
    }
}
