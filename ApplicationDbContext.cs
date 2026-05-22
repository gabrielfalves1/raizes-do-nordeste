using Microsoft.EntityFrameworkCore;

namespace raizes_do_nordeste
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produto>(x => x.ToTable("Produtos"));
            modelBuilder.Entity<Produto>().HasKey(p => p.Id);
            modelBuilder.Entity<Produto>().Property(p => p.Nome).HasMaxLength(200).IsRequired();
            modelBuilder.Entity<Produto>().Property(p => p.Valor).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
