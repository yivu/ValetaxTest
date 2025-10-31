using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ValetaxTest.Model
{
    public class ValetaxDBContext : DbContext
    {
        public DbSet<Tree> Trees { get; set; }
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<MJournal> MJournals { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=localhost;Initial Catalog=ValetaxDB;User Id=sa;Password=MySaPassword123;TrustServerCertificate=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
