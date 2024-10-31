namespace crmEmpresa.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public string? Cliente { get; set; }
        public string? Tratamiento { get; set; }
        public string? Promocion { get; set; }
        public string? Fecha { get; set; }
        public decimal Precio { get; set; }
        public string? Estatus { get; set; }
    }
}
