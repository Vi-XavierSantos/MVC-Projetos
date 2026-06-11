using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Turmas.Data;
using MVC_Turmas.Data.Turma;
using MVC_Turmas.ViewModels.Aluno;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Turmas.Controllers
{
    public class AlunosController : Controller
    {
        private readonly TurmContext _context;

        public AlunosController(TurmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Alunos.Include(a => a.Turma).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistoAlunoViewModel vm)
        {
            if (ModelState.IsValid)
            {

                var aluno = new Aluno
                {
                    Nome = vm.Nome,
                    DataNascimento = vm.DataNascimento,
                    TemExperiencia = vm.TemExperiencia
                };
                _context.Add(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno == null)
            {
                return NotFound();
            }
            return View(aluno);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,TemExperiencia")] Aluno aluno)
        {
            if (id != aluno.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(aluno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aluno);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var aluno = await _context.Alunos
                .Include(a => a.Turma) // IMPORTANTE: Carregar a turma para mostrar o nome
                .FirstOrDefaultAsync(m => m.Id == id);

            if (aluno == null) return NotFound();

            return View(aluno);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aluno = await _context.Alunos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            if (aluno != null)
            {
                _context.Alunos.Remove(aluno);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
