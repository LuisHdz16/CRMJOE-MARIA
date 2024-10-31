using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Cursos
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Curso is required.")]
        public string? Nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Descripcion is required.")]
        public string? Descripcion { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Estatus is required.")]
        public string? Estatus { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/cursos.txt";
            string Cursos = $"Nombre: {Nombre} Descripcion: {Descripcion} Estatus: {Estatus}";
            System.IO.File.AppendAllTextAsync(filePath, Cursos + "\n");

            TempData["SuccessMessage"] = "El cursoa ha sido registrada con éxito.";
            return RedirectToPage("Index");
        }
    }
}
