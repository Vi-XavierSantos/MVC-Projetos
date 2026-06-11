using System.ComponentModel.DataAnnotations;

namespace MVC_Turmas.ViewModels.Professor
{
    public class RegistoProfessorViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatorio!")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "OBRIGATÓRIO!")]
        [DataType(DataType.Date, ErrorMessage = "A data de nascimento deve ser uma data válida.")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "OBRIGATÓRIO!")]
        public string AreaEnsino { get; set; } = string.Empty;
    }
}
