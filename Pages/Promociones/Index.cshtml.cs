using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Promociones
{
    public class IndexModel : PageModel
    {
        public List<Promocion> Promociones { get; set; } = new List<Promocion>();

        public void OnGet()
        {
            string filePath = "wwwroot/promociones.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new[] { "Nombre: ", " Tratamiento: ", " Descripcion: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Promociones.Add(new Promocion
                        {
                            Id = id++,
                            Nombre = data[0],
                            Tratamiento = data[1],
                            Descripcion = data[2],
                            Estatus = data[3]
                        });
                    }
                }
            }
        }
        public IActionResult OnPostEliminar(int id)
        {
            string filePath = "wwwroot/promociones.txt";
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
