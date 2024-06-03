namespace AluraRpa.Domain.Models
{
    public class Consulta
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Professores { get; set; } = string.Empty;
        public string CargaHoraria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
    }
}
