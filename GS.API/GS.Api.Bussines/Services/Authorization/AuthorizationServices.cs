using GS.Api.Bussines.Interfaces.Repositorys.Authorization;
using GS.Api.Bussines.Interfaces.Services.Authorization;
using GS.Api.Cross.Querys.Authorization;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Services.Authorization
{
    public class AuthorizationServices : IAuthorizationServices
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AuthorizationServices(IAuthorizationRepository authorizationRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        public async Task<UserLoginQuery> RecuperarListaUsuarioAsync(string email)
        {
            return await _authorizationRepository.RecuperarListaUsuario(email);
        }
    }
}
