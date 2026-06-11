using Microsoft.EntityFrameworkCore;
using MVC_Turmas.Data;
using MVC_Turmas.Data.Turma; // Ou o caminho onde está a classe Aluno

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<TurmContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// --- ADICIONA ESTE BLOCO AQUI PARA OS ALUNOS ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<TurmContext>();
        context.Database.Migrate(); // Garante que as tabelas são criadas

        if (!context.Alunos.Any())
        {
            Console.WriteLine("--> A semear alunos...");
            for (int i = 1; i <= 20; i++)
            {
                context.Alunos.Add(new MVC_Turmas.Data.Turma.Aluno
                {
                    Nome = "Aluno " + i.ToString("D2"),
                    DataNascimento = DateTime.Now.AddYears(-15),
                    TemExperiencia = (i <= 5)
                });
            }
            context.SaveChanges();
            Console.WriteLine("--> 20 Alunos inseridos com sucesso!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("--> ERRO AO INSERIR: " + ex.Message);
    }
}
// ----------------------------------------------

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();