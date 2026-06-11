using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Turmas.Data;
using MVC_Turmas.Data.Turma;
using MVC_Turmas.ViewModels.Professor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Turmas.Controllers
{
    public class ProfessorsController : Controller
    {
        private readonly TurmContext _context;

        public ProfessorsController(TurmContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Professores.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistoProfessorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var professor = new Professor
                {
                    Nome = vm.Nome,
                    DataNascimento = vm.DataNascimento,
                    AreaEnsino = vm.AreaEnsino
                };

                _context.Add(professor);
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

            var professor = await _context.Professores.FindAsync(id);
            if (professor == null)
            {
                return NotFound();
            }
            return View(professor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DataNascimento,AreaEnsino")] Professor professor)
        {
            if (id != professor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(professor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(professor);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var professor = await _context.Professores
                .Include(p => p.TurmasADirigir)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (professor == null) return NotFound();

            return View(professor);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var professor = await _context.Professores
                .FirstOrDefaultAsync(m => m.Id == id);
            if (professor == null)
            {
                return NotFound();
            }

            return View(professor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var professor = await _context.Professores.FindAsync(id);
            if (professor != null)
            {
                _context.Professores.Remove(professor);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
