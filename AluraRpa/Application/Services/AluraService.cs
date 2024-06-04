using AluraRpa.Domain.Models;
using AluraRpa.Domain.Services;
using AluraRpa.Infra.Configurations;
using AluraRpa.Infra.Database;
using AluraRpa.Infra.Database.Models;
using FluentValidation;
using OpenQA.Selenium;
using Serilog;

namespace AluraRpa.Application.Services
{
    public class AluraService : IAluraService
    {
        private readonly IAppSettings _appSettings;
        private readonly IWebDriver _driver;
        private readonly AluraDbContext _aluraDbContext;
        private readonly IConsultaService _consultaService;
        private readonly IValidator<Consulta> _validator;

        public AluraService(IAppSettings appSettings, IWebDriver driver, AluraDbContext aluraDbContext, IConsultaService consultaService, IValidator<Consulta> validator)
        {
            _appSettings = appSettings;
            _driver = driver;
            _aluraDbContext = aluraDbContext;
            _consultaService = consultaService;
            _validator = validator;
        }

        private List<string> GetLinks(string textToSearch)
        {
            _driver.Navigate().GoToUrl(_appSettings.Alura.Url);
            _driver.FindElement(By.Name("query")).SendKeys(textToSearch);
            _driver.FindElement(By.XPath(_appSettings.Alura.SearchButton)).Click();

            var ulElement = _driver.FindElement(By.XPath(_appSettings.Alura.ListResult));
            var liElements = ulElement.FindElements(By.TagName("li"));

            var consultaLinks = (from item in liElements
                                 let element = item.FindElement(By.TagName("a"))
                                 select element.GetAttribute("href")).ToList();
            return consultaLinks;
        }

        public void Consulta(string textToSearch)
        {
            var consultaLinks = GetLinks(textToSearch);

            foreach (var urlItem in consultaLinks)
            {
                var segmentPosition = 1;

                var uriItem = new Uri(urlItem);

                if (!uriItem.Segments[segmentPosition].StartsWith("curso", StringComparison.CurrentCultureIgnoreCase))
                {
                    Log.Warning("Tipo de página do link: {link} não foi mapeado! Somento o tipo Curso pode ser processado pela automação.", urlItem);
                    continue;
                }

                var result = _consultaService.GetConsulta(urlItem);

                if (!result.IsOk)
                {
                    Log.Error(result.ErrorValue, "Não foi possível obter os dados consultados para o link: {link}", urlItem);
                    continue;
                }

                var validationResult = _validator.Validate(result.Value);

                if (!validationResult.IsValid)
                {
                    Log.Error("O preenchimento dos campos então inválidos: {erros}", validationResult.ToString(Environment.NewLine));
                    continue;
                }

                var cursoModel = ConsultaModel.MapFrom(result.Value!);
                _aluraDbContext.Consulta.Add(cursoModel);
                _aluraDbContext.SaveChanges();
            }

            _driver.Quit();
        }
    }
}
