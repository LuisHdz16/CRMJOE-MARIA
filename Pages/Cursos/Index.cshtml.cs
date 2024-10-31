using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Cursos
{
    public class IndexModel : PageModel
    {
        public List<Curso> Cursos { get; set; } = new List<Curso>();

        public void OnGet()
        {
            string filePath = "wwwroot/cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", "Descripcion: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Cursos.Add(new Curso
                        {
                            Id = id++,
                            Nombre = data[0],
                            Descripcion = data[1],
                            Estatus = data[2]
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/cursos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines.RemoveAt(id - 1); // Remover la persona por ID
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage();
        }
    }
}
