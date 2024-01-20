using buildingBlocksCore.Models.Dto;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;

namespace userApi.Data
{
    public class ApplicationUserRefreshSecurityContext : DbContext, ISecurityKeyContext
    {
        public DbSet<KeyMaterial> SecurityKeys { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApplicationUserRefreshSecurityContext(DbContextOptions<ApplicationUserRefreshSecurityContext> options) : base(options) { }



    }
}
