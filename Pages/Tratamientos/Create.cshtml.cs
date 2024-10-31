using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Tratamientos
{
    public class CreateModel : PageModel
    {

        [BindProperty]
        [Required(ErrorMessage = "Nombre del tratamiento es requerido.")]
        public string? Nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Descripcion es requerido.")]
        public string? Descripcion { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "DuracionEstimada es requerido.")]
        public string? DuracionEstimada { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/tratamientos.txt";
            string tratamientoData = $"Nombre: {Nombre} Descripcion: {Descripcion} DuracionEstimada: {DuracionEstimada}";
            System.IO.File.AppendAllTextAsync(filePath, tratamientoData + "\n");

            TempData["SuccessMessage"] = "El tratamiento ha sido registrado con éxito.";
            return RedirectToPage("Index");
        }
    }
}
