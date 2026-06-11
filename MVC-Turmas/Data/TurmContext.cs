using Microsoft.EntityFrameworkCore;
using MVC_Turmas.Data.Turma;
using ClasseTurma = MVC_Turmas.Data.Turma.Turma;

namespace MVC_Turmas.Data
{
    public class TurmContext : DbContext
    {
        public TurmContext(DbContextOptions<TurmContext> options) : base(options)
        {
        }

        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<ClasseTurma> Turmas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ClasseTurma>()
                .HasOne(t => t.DirTurma)
                .WithMany(p => p.TurmasADirigir)
                .HasForeignKey(t => t.ProfessorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}