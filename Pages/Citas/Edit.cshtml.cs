using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace crmEmpresa.Pages.Citas
{
    public class EditModel : PageModel
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
        public int Id { get; set; }
        public List<string> Clientes { get; set; } = new List<string>();
        public List<string> Tratamientos { get; set; } = new List<string>();
        public List<string> Promociones { get; set; } = new List<string>();

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

            Id = id;
            string filePathCitas = "wwwroot/citas.txt";
            if (System.IO.File.Exists(filePathCitas))
            {
                var lines = System.IO.File.ReadAllLines(filePathCitas).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    var data = lines[id - 1].Split(new string[] { "Cliente: ", " Tratamiento: ", " Promocion: ", " Fecha: ", " Precio: ", " Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 6)
                    {

                        Cliente = data[0];
                        Tratamiento = data[1];
                        Promocion = data[2];
                        Fecha = data[3];
                        Precio = decimal.Parse(data[4]);
                        Estatus = data[5];
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

            string filePath = "wwwroot/citas.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath).ToList();
                if (id >= 1 && id <= lines.Count)
                {
                    lines[id - 1] = $"Cliente: {Cliente} Tratamiento: {Tratamiento} Promocion: {Promocion} Fecha: {Fecha} Precio: {Precio} Estatus: {Estatus}";
                    System.IO.File.WriteAllLines(filePath, lines);
                }
            }

            return RedirectToPage("Index");
        }
    }
}