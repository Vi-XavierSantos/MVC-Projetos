using System.ComponentModel.DataAnnotations;

namespace MVC_Turmas.Data.Turma
{
    public class Turma
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nome da Turma")]
        public string? Descricao { get; set; } = string.Empty;

        [Display(Name = "Diretor de Turma")]
        public int ProfessorId { get; set; }
        public virtual Professor? DirTurma { get; set; }
        public virtual ICollection<Aluno>? Alunos { get; set; }
    }
}
