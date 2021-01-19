using SiGNC.Infra.Data.Dtos.Authorization;
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
        int GetById(int userId);
    }
}
