using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using crmEmpresa.Models;
using System.Net.NetworkInformation;

namespace crmEmpresa.Pages
{
    public class IndexModel : PageModel
    {
        public List<Cita> Citas { get; set; } = new List<Cita>();
        public List<Inscripcion> Inscripciones { get; set; } = new List<Inscripcion>();
        public List<Promocion> Promociones { get; set; } = new List<Promocion>();

        public void OnGet()
        {
            string filePath = "wwwroot/citas.txt";
            if (System.IO.File.Exists(filePath))
            {
                var lines = System.IO.File.ReadAllLines(filePath);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", " Tratamiento: ", " Promocion: ", " Fecha: ", " Precio: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 6 && DateTime.Parse(data[3]) == DateTime.Today && data[5] == "Próximamente")
                    {
                        Citas.Add(new Cita
                        {
                            Id = id++,
                            Cliente = data[0],
                            Tratamiento = data[1],
                            Promocion = data[2],
                            Fecha = DateTime.Parse(data[3]),
                            Precio = decimal.Parse(data[4]),
                            Estatus = data[5]
                        });
                    }
                }
            }

            string filePathIns = "wwwroot/inscripciones.txt";
            if (System.IO.File.Exists(filePathIns))
            {
                var lines = System.IO.File.ReadAllLines(filePathIns);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new string[] { "Cliente: ", "Curso: ", "FechaInicio: ", "FechaFin: ", "Duracion: ", "Precio: ", "Estatus: " }, System.StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 7 && DateTime.Parse(data[2]) == DateTime.Today)
                    {
                        Inscripciones.Add(new Inscripcion
                        {
                            Id = id++,
                            Cliente = data[0],
                            Curso = data[1],
                            FechaInicio = DateTime.Parse(data[2]),
                            FechaFin = DateTime.Parse(data[3]),
                            Duracion = data[4],
                            Precio = decimal.Parse(data[5]),
                            Estatus = data[6]
                        });
                    }
                }
            }

            string filePathPromo = "wwwroot/promociones.txt";

            if (System.IO.File.Exists(filePathPromo))
            {
                var lines = System.IO.File.ReadAllLines(filePathPromo);
                int id = 1;
                foreach (var line in lines)
                {
                    var data = line.Split(new[] { "Nombre: ", " Tratamiento: ", " Descripcion: ", " Estatus: " }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 4 && data[3] == "Disponible")
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
    }
}
