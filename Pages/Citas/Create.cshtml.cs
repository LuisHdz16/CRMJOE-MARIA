using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Citas
{
    public class CreateModel : PageModel
    {
        [BindProperty, Required(ErrorMessage = "Cliente es requerido.")]
        public string? Cliente { get; set; }

        [BindProperty, Required(ErrorMessage = "Tratamiento es requerido.")]
        public string? Tratamiento { get; set; }

        [BindProperty]
        public string? Promocion { get; set; }

        [BindProperty, Required(ErrorMessage = "Precio es requerido.")]
        public decimal Precio { get; set; }

        [BindProperty, Required(ErrorMessage = "Fecha es requerida."), DataType(DataType.Date)]
        public string? Fecha { get; set; }

        [BindProperty, Required(ErrorMessage = "Estatus es requerido.")]
        public string? Estatus { get; set; }

        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> Tratamientos { get; set; } = new List<string>();
        public List<string> Promociones { get; set; } = new List<string>();

        public void OnGet()
        {
            // Cargar clientes desde archivo
            string filePath = "wwwroot/clientes.txt";

            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Apellido: ", " Correo: ", " Telefono: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Clientes.Add($"{data[0]} {data[1]}");
                    }
                }
            }

            // Cargar tratamientos desde archivo
            string filePathTratamientos = "wwwroot/tratamientos.txt";
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

            // Cargar promociones disponibles
            string filePathPromociones = "wwwroot/promociones.txt";
            if (System.IO.File.Exists(filePathPromociones))
            {
                var lines_ = System.IO.File.ReadAllLines(filePathPromociones);

                foreach (var line in lines_)
                {
                    var data = line.Split(new string[] { "Nombre: ", " Tratamiento: ", " Descripcion: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4)
                    {
                        Promociones.Add($"{data[0]}");
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            // Validación para asegurarse de que no se envíe "Seleccione una promoción" como valor
            if (string.IsNullOrEmpty(Promocion) || Promocion == "Seleccione una promoción")
            {
                ModelState.AddModelError("Promocion", "Por favor, seleccione una opción válida.");
                return Page(); // Devuelve la página en caso de error
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Guardar la cita en un archivo de texto
            string filePath = "wwwroot/citas.txt";
            string citaData = $"Cliente: {Cliente} Tratamiento: {Tratamiento} Promocion: {Promocion} Fecha: {Fecha} Precio: {Precio} Estatus: {Estatus}\n";
            System.IO.File.AppendAllTextAsync(filePath, citaData);

            TempData["SuccessMessage"] = "Cita registrada exitosamente!";
            return RedirectToPage("Index");
        }
    }
}