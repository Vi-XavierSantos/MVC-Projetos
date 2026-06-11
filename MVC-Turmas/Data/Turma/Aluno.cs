namespace MVC_Turmas.Data.Turma
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }

        public bool TemExperiencia { get; set; }
        public int? TurmaId { get; set; }
        public virtual Turma? Turma { get; set; }
    }
}
