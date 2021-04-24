using GS.Api.Bussines.Interfaces.Services.Authorization;
using GS.Api.Cross.Querys.Authorization;
using GS.Bussines.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GS.Api.Controllers.Identity
{
    [Route("api/Authorization")]
    public class AuthorizationController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAuthorizationServices _authorizationServices;

        public AuthorizationController(SignInManager<IdentityUser> signInManager,
                                    UserManager<IdentityUser> userManager,
                                    IConfiguration configuration,
                                    IAuthorizationServices authorizationServices)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _authorizationServices = authorizationServices;
        }

        [HttpPost("createAuthorization")]
        public async Task<ActionResult> CriarAutenticacao(UserRegister userRegistro)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var userIdentity = new IdentityUser()
            {
                Email = userRegistro.Email,
                UserName = userRegistro.Email,
                EmailConfirmed = true
            };

            var criandouser = await _userManager.CreateAsync(userIdentity, userRegistro.Senha);

            if (criandouser.Succeeded)
            {
                return CustomResponse(await GerarJwt(userRegistro.Email));
            }

            foreach (var erro in criandouser.Errors)
            {
                AdicionarErroProcessamento(erro.Description);
            }

            return CustomResponse();
        }

        [HttpPost("loginAuthorization")]
        public async Task<ActionResult<UserResultLogin>> Autenticacao(UserLogin user)
        {
            if(!ModelState.IsValid)
                return CustomResponse(ModelState);

            var logarUsuario = await _signInManager.PasswordSignInAsync(user.Email, user.Senha, false, true);

            if (logarUsuario.Succeeded)
            {
                return CustomResponse(await GerarJwt(user.Email));
            }

            if (logarUsuario.IsLockedOut)
            {
                AdicionarErroProcessamento("Usuário bloqueado !");
            }

            AdicionarErroProcessamento("Usuário ou Senha Incorretas");
            return CustomResponse();
        }

        [HttpGet("recuperarListaUsuario")]
        public async Task<ActionResult<UserLoginQuery>> RecuperarListaUsuario(string email)
        {
            return CustomResponse(_authorizationServices.RecuperarListaUsuarioAsync(email));
        }

        private async Task<UserResultLogin> GerarJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claims = await _userManager.GetClaimsAsync(user);

            var identityClaims = await ObterClaimsUsuario(claims, user);

            var encodeToken = CodificarToken(identityClaims);

            return ObterRespostaToken(encodeToken, user, claims);
        }

        private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string CodificarToken(ClaimsIdentity claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("AppSettings:Secret"));

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Issuer = _configuration.GetValue<string>("AppSettings:Emissor"),
                Audience = _configuration.GetValue<string>("AppSettings:ValidoEm"),
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(_configuration.GetValue<int>("AppSettings:ExpiracaoHoras")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private UserResultLogin ObterRespostaToken(string encodeToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserResultLogin()
            {
                AccessToken = encodeToken,
                ExpiresIn = TimeSpan.FromHours(_configuration.GetValue<int>("AppSettings:ExpiracaoHoras")).TotalSeconds,
                UserToken = new UsuarioToken()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(claim => new UsuarioClaim()
                    {
                        Type = claim.Type,
                        Value = claim.Value
                    })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
