using SiGNC.Application.Web.NET5.Models.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiGNC.Application.Web.NET5.Models.Tables
{
    public class ConformidadeTableViewModel
    {
        public int Id { get; set; }
        public string NumeroConformidade { get; set; }
        public string DataEmissao { get; set; }
        public string  UsuarioEmitente { get; set; }
        public int IdStatusConformidade { get; set; }
        public string DescricaoStatusConformidade { get; set; }
    }
} 
