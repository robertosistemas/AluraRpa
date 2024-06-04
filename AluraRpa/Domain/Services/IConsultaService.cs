using AluraRpa.Domain.Models;

namespace AluraRpa.Domain.Services
{
    public interface IConsultaService
    {
        Result<Consulta, Exception> GetConsulta(string url);
    }
}
