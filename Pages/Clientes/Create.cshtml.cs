using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace crmEmpresa.Pages.Clientes
{
    public class CreateModel : PageModel
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
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/clientes.txt";
            string formData = $"Nombre: {Nombre} Apellido: {Apellido} Correo: {Correo} Telefono: {Telefono}\n";
            System.IO.File.AppendAllTextAsync(filePath, formData);

            // Process form submission (e.g., save to database, send an email)
            TempData["SuccessMessage"] = "Your message has been sent successfully!";
            return RedirectToPage("Index");
        }
    }
}
