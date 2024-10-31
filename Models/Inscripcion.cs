namespace crmEmpresa.Models
{
    public class Inscripcion
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public string? Curso { get; set; }
        public string? FechaInicio { get; set; }
        public string? FechaFin { get; set; }
        public string? Duracion { get; set; }
        public decimal Precio { get; set; }
        public string? Estatus { get; set; }
    }
}
