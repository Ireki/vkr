using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace vkr.Model
{
    public partial class DocumentsContext : DbContext
    {
        public DocumentsContext()
        {
        }

        public DocumentsContext(DbContextOptions<DocumentsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Practical> Practical { get; set; }
        public virtual DbSet<Theses> Theses { get; set; }
        public virtual DbSet<Vkr> Vkr { get; set; }
        public virtual DbSet<OtherDocumentation> OtherDocumentation { get; set; }
        public virtual DbSet<User> Users { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-73NASB4\\SQLEXPRESS;Initial Catalog=documents;Integrated Security=True");
            }
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
