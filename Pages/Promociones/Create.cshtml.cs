using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Promociones
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required]
        public string? Nombre { get; set; }
        [BindProperty]
        [Required]
        public string? Tratamiento { get; set; }

        [BindProperty]
        [Required]
        public string? Descripcion { get; set; }

        [BindProperty]
        [Required]
        public string? Estatus { get; set; }

        public List<string> Tratamientos { get; set; } = new List<string>();

        string filePathTratamientos = "wwwroot/tratamientos.txt";
        public void OnGet()
        {
            if (System.IO.File.Exists(filePathTratamientos))
            {
                var lines_ = System.IO.File.ReadAllLines(filePathTratamientos);

                foreach (var line in lines_)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Descripcion: ", " DuracionEstimada: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Tratamientos.Add($"{data[0]}");
                    }
                }
            }
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/promociones.txt";
            string nuevaPromocion = $"Nombre: {Nombre} Tratamiento: {Tratamiento} Descripcion: {Descripcion}  Estatus: {Estatus}\n";
            System.IO.File.AppendAllText(filePath, nuevaPromocion);

            TempData["SuccessMessage"] = "La promoción se ha agregado correctamente.";
            return RedirectToPage("Index");
        }
    }
}
