using Microsoft.EntityFrameworkCore;
using Softmad.Services.Models;

namespace Softmad.Services.LeadGeneration.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> opt) : base(opt)
        {
                
        }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<Visit> Visits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
