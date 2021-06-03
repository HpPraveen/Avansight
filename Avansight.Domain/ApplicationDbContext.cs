using Microsoft.EntityFrameworkCore;

namespace Avansight.Domain
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
