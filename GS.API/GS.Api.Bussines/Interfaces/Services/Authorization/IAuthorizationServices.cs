using GS.Api.Cross.Querys.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Interfaces.Services.Authorization
{
    public interface IAuthorizationServices
    {
        Task<UserLoginQuery> RecuperarListaUsuarioAsync(string email);
    }
}
