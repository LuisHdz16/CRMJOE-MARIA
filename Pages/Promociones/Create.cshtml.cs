using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Promociones
{
    public class CreateModel : PageModel
    {
        [BindProperty, Required(ErrorMessage = "El nombre es requerido.")]
        public string? Nombre { get; set; }

        [BindProperty, Required(ErrorMessage = "El tratamiento es requerido.")]
        public string? Tratamiento { get; set; }

        [BindProperty, Required(ErrorMessage = "La descripción es requerida.")]
        public string? Descripcion { get; set; }

        [BindProperty, Required(ErrorMessage = "El estatus es requerido.")]
        public string? Estatus { get; set; }

        public List<string> Tratamientos { get; set; } = new List<string>();

        public void OnGet()
        {
            // Cargar tratamientos desde archivo
            string filePathTratamientos = "wwwroot/tratamientos.txt";

            if (System.IO.File.Exists(filePathTratamientos))
            {
                var lines = System.IO.File.ReadAllLines(filePathTratamientos);

                foreach (var line in lines)
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
            // Validación para evitar que "Seleccione un tratamiento" sea guardado como valor
            if (string.IsNullOrEmpty(Tratamiento) || Tratamiento == "Seleccione un tratamiento")
            {
                ModelState.AddModelError("Tratamiento", "Por favor, seleccione una opción válida.");
                return Page();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Guardar la promoción en un archivo de texto
            string filePath = "wwwroot/promociones.txt";
            string nuevaPromocion = $"Nombre: {Nombre} Tratamiento: {Tratamiento} Descripcion: {Descripcion} Estatus: {Estatus}\n";
            System.IO.File.AppendAllText(filePath, nuevaPromocion);

            TempData["SuccessMessage"] = "La promoción se ha agregado correctamente.";
            return RedirectToPage("Index");
        }
    }
}