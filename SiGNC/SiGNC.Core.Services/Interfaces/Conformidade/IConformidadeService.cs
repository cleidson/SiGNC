using SiGNC.Core.Services.DTOs.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.Interfaces.Conformidade
{ 
    public interface IConformidadeService
    {
        bool SalvarConformidade(ConformidadeDto conformidade);
        bool EditarConformidade(ConformidadeDto conformidade);
        Task<List<ConformidadeDto>> ListarConformidades(); 
    }
}
