using crmEmpresa.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace crmEmpresa.Pages.Inscripciones
{
    public class EditModel : PageModel
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
        public decimal Precio { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Estatus is required.")]
        public string? Estatus { get; set; }
        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> Cursos { get; set; } = new List<string>();
        public int Id { get; set; }
        public void OnGet(int id)
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

            Id = id;
            string filePathCitas = "wwwroot/inscripciones.txt";
            if (System.IO.File.Exists(filePathCitas))
            {
                var lines = System.IO.File.ReadAllLines(filePathCitas).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Cliente: ", "Curso: ", "FechaInicio: ", "FechaFin: ", "Duracion: ", "Precio: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 7)
                    {

                        Cliente = data[0];
                        Curso = data[1];
                        FechaInicio = data[2];
                        FechaFin = data[3];
                        Duracion = data[4];
                        Precio = decimal.Parse(data[5]);
                        Estatus = data[6];
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

            string filePath = "wwwroot/inscripciones.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Cliente: {Cliente} Curso: {Curso} FechaInicio: {FechaInicio} FechaFin: {FechaFin} Duracion: {Duracion} Precio: {Precio} Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}
