using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace GS.Data.Data.Context
{
    public class GameStoreContext : IdentityDbContext 
    {
        public IDbConnection Connection { get; set; }

        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options)
        { }
    }
}
