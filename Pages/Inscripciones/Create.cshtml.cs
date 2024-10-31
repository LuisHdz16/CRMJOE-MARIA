using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using crmEmpresa.Models;

namespace crmEmpresa.Pages.Inscripciones
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        [Required(ErrorMessage = "Cliente is required.")]
        public string? Cliente { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Curso is required.")]
        public string? Curso { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "FechaInicio is required.")]
        public string? FechaInicio { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "FechaFin is required.")]
        public string? FechaFin { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Duracion is required.")]
        public string? Duracion { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Precio is required.")]
        public double Precio { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Estatus is required.")]
        public string? Estatus { get; set; }
        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> Cursos { get; set; } = new List<string>();

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

            string filePathCursos = "wwwroot/cursos.txt";
            if (System.IO.File.Exists(filePathCursos))
            {
                var lines = System.IO.File.ReadAllLines(filePathCursos);

                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Nombre: ", "Descripcion: ", "Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 3)
                    {
                        Cursos.Add($"{data[0]}");
                    }
                }
            }
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string filePath = "wwwroot/inscripciones.txt";
            string Cursos = $"Cliente: {Cliente} Curso: {Curso} FechaInicio: {FechaInicio} FechaFin: {FechaFin} Duracion: {Duracion} Precio: {Precio} Estatus: {Estatus}";
            System.IO.File.AppendAllTextAsync(filePath, Cursos + "\n");

            TempData["SuccessMessage"] = "La inscripción ha sido registrada con éxito.";
            return RedirectToPage("Index");
        }
    }
}
