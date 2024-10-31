using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Tratamientos
{
    public class EditModel : PageModel
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
        public int Id { get; set; }

        public void OnGet(int id)
        {
            Id = id;
            string filePath = "wwwroot/tratamientos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Nombre: ", " Descripcion: ", " DuracionEstimada: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {

                        Nombre = data[0];
                        Descripcion = data[1];
                        DuracionEstimada = data[2];
                    }
                }
            }
        }
        public IActionResult OnPost(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/tratamientos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Nombre: {Nombre} Descripcion: {Descripcion} DuracionEstimada: {DuracionEstimada}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
