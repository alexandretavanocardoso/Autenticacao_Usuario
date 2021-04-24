using GS.Api.Bussines.Interfaces.Repositorys.Authorization;
using GS.Api.Bussines.Models;
using GS.Api.Cross.Querys.Authorization;
using GS.Bussines.Models.Entities;
using GS.Data.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GS.Api.Bussines.Repositorys.Authorization
{
    public class AuthorizationRepository : Conexao, IAuthorizationRepository
    {
        public AuthorizationRepository(IConfiguration configuration) : base(configuration)
        {}

        public async Task<UserLoginQuery> RecuperarListaUsuario(string email)
        {
            OpenConnection();

            Cmd = new SqlCommand("SELECT * FROM ASPNETUSERS WHERE USERNAME = @email", Con);
            Cmd.Parameters.AddWithValue("@email", email);
            Dr = Cmd.ExecuteReader();

            //verificar se o DataReader obteve algum registro..
            if (Dr.Read())
            {
                UserLoginQuery usuario = new UserLoginQuery();
                usuario.Email = Convert.ToString(Dr["Email"]);

                return usuario;
            }

            CloseConnection();

            return null;
        }

        public bool ValidarSeExisteEmail(string email) {
            
            OpenConnection();

            Cmd = new SqlCommand("SELECT * FROM ASPNETUSERS WHERE USERNAME = @email", Con);
            Cmd.Parameters.AddWithValue("@email", email);
            Dr = Cmd.ExecuteReader();

            if (Dr.Read())
            {
                UserLoginQuery usuario = new UserLoginQuery();
                usuario.Email = Convert.ToString(Dr["Email"]);

                return true;
            }

            CloseConnection();

            return false;
        }
    }
}
