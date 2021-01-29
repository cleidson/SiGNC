
using SiGNC.Core.Services.DTOs.Authorization;
using SiGNC.Infra.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiGNC.Core.Services.Interfaces
{
    public interface IAuthorizationSiGncService
    {
        AuthenticateResponseDto Authenticate(AuthenticateRequestDto request);
        Task<AuthenticateResponseDto> AuthenticateSync(AuthenticateRequestDto request);
        int GetById(int userId);
    }
}
