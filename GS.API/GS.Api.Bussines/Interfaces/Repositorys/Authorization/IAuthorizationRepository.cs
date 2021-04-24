using GS.Api.Cross.Querys.Authorization;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Interfaces.Repositorys.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<UserLoginQuery> RecuperarListaUsuario(string email);
    }
}
