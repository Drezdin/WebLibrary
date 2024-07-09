
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using WebLibrary.DataAccess.Congigurations;
using WebLibrary.DataAccess.Entites;
using WebLibrary.DataAccess.Repositories;



namespace WebLibrary.DataAccess
{

    public class WebLibraryDbContext : IdentityDbContext<IdentityUser> 
    {
        public WebLibraryDbContext(DbContextOptions<WebLibraryDbContext> options) : base(options) {  }
        public DbSet<BookEntity> Books { get; set; }
        
    }
}

