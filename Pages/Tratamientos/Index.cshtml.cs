using crmEmpresa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace crmEmpresa.Pages.Tratamientos
{
    public class IndexModel : PageModel
    {
        public List<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();

        public void OnGet()
        {
            string filePath = "wwwroot/tratamientos.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Descripcion: ", " DuracionEstimada: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Tratamientos.Add(new Tratamiento
                        {
                            Id = id++,
                            Nombre = data[0],
                            Descripcion = data[1],
                            DuracionEstimada = data[2]
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/tratamientos.txt";
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
