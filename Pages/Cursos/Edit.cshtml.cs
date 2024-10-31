using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Cursos
{
    public class EditModel : PageModel
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
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            string filePath = "wwwroot/cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Nombre: ", " Descripcion: ", " Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Nombre = data[0];
                        Descripcion = data[1];
                        Estatus = data[2];
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

            string filePath = "wwwroot/cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Nombre: {Nombre} Descripcion: {Descripcion} Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
