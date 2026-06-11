using Microsoft.EntityFrameworkCore;
using MVC_Veterinario.Data.Vet;

namespace MVC_Veterinario.Data
{
    public class VetContext : DbContext
    {
        public VetContext(DbContextOptions<VetContext> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Animal> Animais { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
    }
}
