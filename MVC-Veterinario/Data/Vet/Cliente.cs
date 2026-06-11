namespace MVC_Veterinario.Data.Vet
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty; 
        public string Nif { get; set; } = string.Empty;
    }
}
