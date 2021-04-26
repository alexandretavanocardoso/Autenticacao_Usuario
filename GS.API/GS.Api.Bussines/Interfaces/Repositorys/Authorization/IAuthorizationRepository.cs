using GS.Api.Cross.Querys.Authorization;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Interfaces.Repositorys.Authorization
{
    public interface IAuthorizationRepository
    {
        Task<UserLoginQuery> RecuperarListaUsuario(string email);
        bool ValidarSeExisteEmail(string email);
        Task<bool> DeletarContaUsuario(string email);
    }
}
