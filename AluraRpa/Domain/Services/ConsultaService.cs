using AluraRpa.Domain.Exceptions;
using AluraRpa.Domain.Models;
using AluraRpa.Infra.Configurations;
using OpenQA.Selenium;
using System.Text;

namespace AluraRpa.Domain.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IAppSettings _appSettings;
        private readonly IWebDriver _driver;

        public ConsultaService(IAppSettings appSettings, IWebDriver driver)
        {
            _appSettings = appSettings;
            _driver = driver;
        }

        public Result<Consulta, DomainException> GetConsulta(string url)
        {
            try
            {
                _driver.Navigate().GoToUrl(url);

                var curso = new Consulta();

                var elementTitulo = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.Titulo));
                var elementSubTitulo = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.SubTitulo));

                curso.Titulo = string.Concat(elementTitulo.Text, Environment.NewLine, elementSubTitulo.Text);

                curso.Professores = GetProfessores();

                var elementCargaHoraria = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.CargaHoraria));
                curso.CargaHoraria = elementCargaHoraria.Text;

                curso.Descricao = GetDescricao();

                return curso;
            }
            catch (Exception ex)
            {
                return new DomainException("Erro ao tentar extrar informações da página.", ex);
            }
        }

        private string GetProfessores()
        {
            var professores = new StringBuilder();

            var elementProfessor = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.NomeProfessores));
            professores.AppendLine(elementProfessor.Text);

            return professores.ToString();
        }

        private string GetDescricao()
        {
            var descricao = new StringBuilder();

            var hrElement = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.DescricaoTitulo));

            descricao.AppendLine(hrElement.Text);

            var ulElement = _driver.FindElement(By.XPath(_appSettings.Alura.CursoXPath.DescricaoListaAssuntos));
            var liElements = ulElement.FindElements(By.TagName("li"));

            var descricaoLinhas = from item in liElements
                                  select item.Text;

            descricao.AppendJoin(Environment.NewLine, descricaoLinhas.ToArray());

            return descricao.ToString();
        }
    }
}
