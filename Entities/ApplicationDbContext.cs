using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoTurma> AlunoTurmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AlunoTurma>()
                .HasKey(at => at.Id);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Aluno)
                .WithMany(a => a.AlunoTurmas)
                .HasForeignKey(at => at.AlunoId);

            modelBuilder.Entity<AlunoTurma>()
                .HasOne(at => at.Turma)
                .WithMany(t => t.AlunoTurmas)
                .HasForeignKey(at => at.TurmaId);

            modelBuilder.Entity<Turma>()
                .HasIndex(t => t.Nome)
                .IsUnique();
        }
    }
}
