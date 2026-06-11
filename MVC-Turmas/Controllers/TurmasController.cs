using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Turmas.Data;
using MVC_Turmas.Data.Turma;
using MVC_Turmas.ViewModels.Turma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Turmas.Controllers
{
    public class TurmasController : Controller
    {
        private readonly TurmContext _context;

        public TurmasController(TurmContext context)
        {
            _context = context;
        }
        // 1. LISTAR TURMAS
        public async Task<IActionResult> Index()
        {
            var turmas = await _context.Turmas
                .Include(t => t.DirTurma)
                .Include(t => t.Alunos)
                .ToListAsync();

            return View(turmas);
        }
        // 2. CRIAR TURMA
        public IActionResult Create()
        {
            var vm = new RegistoTurmaViewModel
            {
                ProfessoresDisponiveis = _context.Professores
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
                    .ToList()
            };
            return View(vm);
        }

        // 3. CRIAR TURMA E 10 ALUNOS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistoTurmaViewModel vm)
        {
            // 1. Validar se os dados do formulário (Nome e Professor) estão OK
            if (!ModelState.IsValid)
            {
                vm.ProfessoresDisponiveis = _context.Professores
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
                    .ToList();
                return View(vm);
            }

            // 2. Procurar os 10 alunos "livres"
            var alunosLivres = await _context.Alunos
                .Where(a => a.TurmaId == null)
                .OrderByDescending(a => a.TemExperiencia)
                .Take(10)
                .ToListAsync();

            // 3. Validar a regra de negócio (mínimo 10 alunos)
            if (alunosLivres.Count < 10)
            {
                ModelState.AddModelError("", "Operação cancelada: São necessários pelo menos 10 alunos sem turma.");
                vm.ProfessoresDisponiveis = _context.Professores
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nome })
                    .ToList();
                return View(vm);
            }


            // 4. Criar a entidade Turma a partir da ViewModel
            var novaTurma = new Turma
            {
                Descricao = vm.Descricao,
                ProfessorId = vm.ProfessorId
            };

            _context.Turmas.Add(novaTurma);
            await _context.SaveChangesAsync(); // Salva para gerar o ID da turma

            // 5. Vincular os alunos escolhidos à nova turma
            foreach (var aluno in alunosLivres)
            {
                aluno.TurmaId = novaTurma.Id;
            }

            await _context.SaveChangesAsync(); // Atualiza os alunos no banco

            return RedirectToAction(nameof(Index));
        }

        // 4. APAGAR TURMA
        public async Task<IActionResult> Delete(int id)
        {
            var turma = await _context.Turmas
                .Include(t => t.Alunos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (turma != null)
            {
                if (turma.Alunos != null)
                {
                    foreach (var aluno in turma.Alunos)
                    {
                        aluno.TurmaId = null;
                    }
                }
                _context.Turmas.Remove(turma);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // 5. DETALHES DA TURMA (VER ALUNOS)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var turma = await _context.Turmas
                .Include(t => t.DirTurma)
                .Include(t => t.Alunos)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (turma == null) return NotFound();

            return View(turma);
        }
    }
}
