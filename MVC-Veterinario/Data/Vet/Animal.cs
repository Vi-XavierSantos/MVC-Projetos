namespace MVC_Veterinario.Data.Vet
{
    public class Animal
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Especie { get; set; } = string.Empty;
        public string Raca { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string NumeroChip { get; set; } = string.Empty;
        public int IdCliente { get; set; }
        public float Peso { get; set; }

    }
}
