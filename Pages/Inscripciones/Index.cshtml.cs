using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Inscripciones
{
    public class IndexModel : PageModel
    {
        public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();

        public void OnGet()
        {
            string filePath = "wwwroot/inscripciones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", "Curso: ", "FechaInicio: ", "FechaFin: ", "Duracion: ", "Precio: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 7)
                    {
                        Inscripciones.Add(new Inscripcion
                        {
                            Id = id++,
                            Cliente = data[0],
                            Curso = data[1],
                            FechaInicio = data[2],
                            FechaFin = data[3],
                            Duracion = data[4],
                            Precio = decimal.Parse(data[5]),
                            Estatus = data[6]
                        });
                    }
                }
            }
        }
        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/inscripciones.txt";
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
