using Microsoft.EntityFrameworkCore;

namespace Travel.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
        {
            
        }




    }
}
