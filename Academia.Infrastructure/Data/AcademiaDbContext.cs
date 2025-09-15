using Academia.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Academia.Infrastructure.Data
{
    public class AcademiaDbContext : DbContext
    {
        public AcademiaDbContext(DbContextOptions<AcademiaDbContext> options) : base(options) { }

    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Plano> Planos { get; set; }
    public DbSet<Presenca> Presencas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Permissao> Permissoes { get; set; }
    public DbSet<UsuarioPermissao> UsuariosPermissoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aluno>()
                .HasIndex(a => a.CPF)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Configuração do relacionamento N:N entre Usuario e Permissao
            modelBuilder.Entity<UsuarioPermissao>()
                .HasKey(up => new { up.UsuarioId, up.PermissaoId });

            modelBuilder.Entity<UsuarioPermissao>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuariosPermissoes)
                .HasForeignKey(up => up.UsuarioId);

            modelBuilder.Entity<UsuarioPermissao>()
                .HasOne(up => up.Permissao)
                .WithMany(p => p.UsuariosPermissoes)
                .HasForeignKey(up => up.PermissaoId);
        }
    }
}