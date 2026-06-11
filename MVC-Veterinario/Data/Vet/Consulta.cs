namespace MVC_Veterinario.Data.Vet
{
    public class Consulta
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int IdAnimal { get; set; }
        public int IdFuncionario { get; set; }
    }
}
