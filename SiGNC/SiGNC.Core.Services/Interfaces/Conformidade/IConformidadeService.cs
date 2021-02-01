﻿using SiGNC.Core.Services.DTOs.Conformidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.Interfaces.Conformidade
{
    public interface IConformidadeService
    {
        Task<bool> SalvarConformidadeSync(ConformidadeDto conformidade);
        bool EditarConformidade(ConformidadeDto conformidade);
        Task<List<ConformidadeDto>> GetConformidadesSync();
        Task<List<ConformidadeDto>> GetConformidadesSync(int statusConformidadeId);
        Task<ConformidadeDto> GetConformidadeSync(int id); 
        Task<bool> EditarConformidadeSync(ConformidadeDto conformidade);
        Task<bool> GetNumConformidade(string numConformidade);
    }
}
