using Microsoft.Data.SqlClient;
using System.Configuration; //capturar o nome da connectionstring
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GS.Api.Bussines.Models
{
    public class Conexao
    {
        private readonly IConfiguration _configuration;

        public Conexao(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection Con;    //conexão com o banco de dados
        protected SqlCommand Cmd;       //executar comandos SQL
        protected SqlDataReader Dr;     //Ler dados de consultas 
        protected SqlTransaction Tr;    //Transações em banco de dados (commit/rollback)

        protected void OpenConnection()
        {
            Con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            Con.Open();
        }

        protected void CloseConnection()
        {
            Con.Close();
        }
    }
}
