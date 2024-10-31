using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Win32;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Citas
{
    public class IndexModel : PageModel
    {

        public List<Cita> Citas { get; set; } = new List<Cita>();

        public void OnGet()
        {
            // Leer los registros de tratamientos desde el archivo de texto
            string filePath = "wwwroot/citas.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", " Tratamiento: ", " Promocion: ", " Fecha: ", " Precio: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 6)
                    {
                        Citas.Add(new Cita
                        {
                            Id = id++,
                            Cliente = data[0],
                            Tratamiento = data[1],
                            Promocion = data[2],
                            Fecha = data[3],
                            Precio = decimal.Parse(data[4]),
                            Estatus = data[5]
                        });
                    }
                }
            }
        }
        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/citas.txt";
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
