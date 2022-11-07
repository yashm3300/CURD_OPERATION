using CategoryMaster.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CategoryMaster.Data
{
    public class MVCCategoryDbcontex : DbContext
    {
        public MVCCategoryDbcontex(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Category> Categorys { get; set; }
    }


}
 