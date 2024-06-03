using AluraRpa.Domain.Models;

namespace AluraRpa.Infra.Database.Models
{
    public class ConsultaModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Professores { get; set; } = string.Empty;
        public string CargaHoraria { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;

        public static ConsultaModel MapFrom(Consulta consulta)
        {
            return new ConsultaModel()
            {
                Id = consulta.Id,
                Titulo = consulta.Titulo,
                Professores = consulta.Professores,
                CargaHoraria = consulta.CargaHoraria,
                Descricao = consulta.Descricao
            };
        }

        public static IEnumerable<ConsultaModel> MapFrom(IEnumerable<Consulta> consultas)
        {
            return (from item in consultas
                    select MapFrom(item)).ToList();
        }
    }
}
