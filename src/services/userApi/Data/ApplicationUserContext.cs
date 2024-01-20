using buildingBlocksCore.Models.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Core.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using userApi.Models;

namespace userApi.Data
{
    public class ApplicationUserContext : IdentityDbContext
    {
      
        public ApplicationUserContext(DbContextOptions<ApplicationUserContext> options) : base(options) { }

       

    
    }
}
