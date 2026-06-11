using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVC_Turmas.ViewModels.Turma
{
    public class RegistoTurmaViewModel
    {
        [Required(ErrorMessage = "Dê um nome à turma (ex: 10º A)")]
        [Display(Name = "Nome da Turma")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Selecione um Diretor de Turma")]
        [Display(Name = "Diretor de Turma")]
        public int ProfessorId { get; set; }

        public List<SelectListItem>? ProfessoresDisponiveis { get; set; }
    }
}
