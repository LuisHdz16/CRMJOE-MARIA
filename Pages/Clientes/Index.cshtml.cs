using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Clientes
{
    public class IndexModel : PageModel
    {
        public List<Cliente> Clientes { get; set; } = new List<Cliente>();
        [BindProperty(SupportsGet = true)]
        public string? SearchField { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }
        public void OnGet()
        {
            string filePath = "wwwroot/clientes.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Apellido: ", " Correo: ", " Telefono: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Clientes.Add(new Cliente
                        {
                            Id = id++,
                            Nombre = data[0],
                            Apellido = data[1],
                            Correo = data[2],
                            Telefono = data[3]
                        });
                    }
                }
            }
            if (SearchField != "Todos" && !string.IsNullOrEmpty(SearchField) && !string.IsNullOrEmpty(SearchTerm))
            {
                // Filtrar los clientes según el campo de búsqueda y el término de búsqueda
                Clientes = Clientes.Where(c =>
                    (SearchField == "Nombre" && c.Nombre.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (SearchField == "Apellido" && c.Apellido.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (SearchField == "Correo" && c.Correo.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase)) ||
                    (SearchField == "Telefono" && c.Telefono.Contains(SearchTerm))
                ).ToList();
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            string filePath = "wwwroot/clientes.txt";
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
