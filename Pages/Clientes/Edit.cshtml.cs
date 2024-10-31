using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;

namespace crmEmpresa.Pages.Clientes
{
    public class EditModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Nombre es requerido.")]
        public string? Nombre { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Apellido es requerido.")]
        public string? Apellido { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Correo es requerido.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Correo { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Telefono es requerido.")]
        public string? Telefono { get; set; }
        public int Id { get; set; }
        public void OnGet(int id)
        {
            Id = id;
            string filePath = "wwwroot/clientes.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Nombre: ", " Apellido: ", " Correo: ", " Telefono: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {

                        Nombre = data[0];
                        Apellido = data[1];
                        Correo = data[2];
                        Telefono = data[3];
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

            string filePath = "wwwroot/clientes.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Nombre: {Nombre} Apellido: {Apellido} Correo: {Correo} Telefono: {Telefono}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
