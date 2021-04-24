using GS.Api.Cross.Querys.Authorization;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Interfaces.Services.Authorization
{
    public interface IAuthorizationServices
    {
        Task<UserLoginQuery> RecuperarListaUsuarioAsync(string email);
        bool ValidarSeExisteEmailAsync(string email);
    }
}
