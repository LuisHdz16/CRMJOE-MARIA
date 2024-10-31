using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Promociones
{
    public class EditModel : PageModel
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
        public int Id { get; set; }
        public List<string> Tratamientos { get; set; } = new List<string>();

        string filePathTratamientos = "wwwroot/tratamientos.txt";

        public void OnGet(int id)
        {
            Id = id;

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
            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new[] { "Nombre: ", " Tratamiento: ", " Descripcion: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Id = id++;
                        Nombre = data[0];
                        Tratamiento = data[1];
                        Descripcion = data[2];
                        Estatus = data[3];
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

            string filePath = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    // Actualizar la línea correspondiente a la promoción
                    lines[id - 1] = $"Nombre: {Nombre} Tratamiento: {Tratamiento} Descripcion: {Descripcion}  Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
