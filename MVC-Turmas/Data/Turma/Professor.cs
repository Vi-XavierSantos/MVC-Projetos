namespace MVC_Turmas.Data.Turma
{
    public class Professor
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string AreaEnsino { get; set; } = string.Empty;
        public virtual ICollection<Turma>? TurmasADirigir { get; set; }
    }
}
