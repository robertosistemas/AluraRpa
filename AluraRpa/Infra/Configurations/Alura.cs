namespace AluraRpa.Infra.Configurations
{
    public class Alura
    {
        public string DriverType { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string SearchButton { get; set; } = string.Empty;
        public string ListResult { get; set; } = string.Empty;

        public ConsultaXPath CursoXPath { get; set; } = default!;
    }
}
