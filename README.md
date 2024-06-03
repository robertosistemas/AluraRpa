# AluraRpa
Desafio de automação com Selenium

O desafio consiste no desenvolvimento de um RPA simples que realiza uma busca
automaizada no site da Alura (https://www.alura.com.br/) e grava os resultados em um
banco de dados.

# Como foi estruturado o projeto

* Procurei utilizar uma arquitetura limpa separando as responsabilidades das camadas de Aplicação, Domínio e Infraestrutura.
* Empreguei os princípios SOLID, para criar um projeto orientado a objetos que preza por código limpo e desaclopado.
* Utilizei Injeção de dependência tornando o código memos aclopado facilitando também o criação de testes.

# Bibliotecas e Frameworks Utilizados

* **Selenium.WebDriver** - Para a automação dos Web Browrsers
* **Selenium.WebDriver.ChromeDriver - Versão 125.0.6422.7800** - Para executar a automação no Google Chrome
* **Selenium.WebDriver.MSEdgeDriver - Versão 123.0.2420.65** - Para executar a automação no Microsoft Edge
* **Microsoft.Extensions.DependencyInjection** - Para injeção de dependência
* **Microsoft.Extensions.Configuration.Json** - Para dar suporte a configuração utilizando arquivos Json AppSettings.json
* **Microsoft.Extensions.Configuration.Binder** - Para o carregamento e viculação das configurações do arquivo AppSettings.json em uma classe de Configuração
* **FluentValidation** - Para validar as regras de negócio aplicadas
* **Microsoft.EntityFrameworkCore** - Para mapeamento objeto relacional
* **Microsoft.EntityFrameworkCore.SqlServer** - Para integração do EntityFrameworkCore com o SqlServer
* **Microsoft.EntityFrameworkCore.Tools** - Para implementação das Migrations de Banco de dados
* **Serilog.Sinks.Console** - Para fazer a integração do Serilog permitindo a exibição das mensagens de log na console da aplicação
* **Serilog.Sinks.File** - Para fazer a integração do Serilog permitindo salvar as mensagens de log em um arquivo de logo na pasta da aplicação, sendo um arquivo por dia.

# Execução da aplicação

* Para executar a aplicação basta abrir a solução AluraRpa no Visual Studio e executar o Start.
* Na inicialização da aplicação será aplicado as Migrations de Banco de dados para a criação do banco Sql Server de nome: **Alura-6d509556**.
* Será apresentado uma tela pedindo para entrar com o texto a pesquisar no site da Alura (https://www.alura.com.br/) e se quiser pode simplemente pressionar ENTER para pesquisar o palavra padrão sugerida: **RPA**
* Após feito isso será inicializado uma automação feita com o Selenium WebDriver que abrirá o site e preencherá o campo de pesquisa com o texto informado realizando a busca pela informação.
* Após o retorno da consulta a automação fará uma busca por todos os links retornados e em seguida consultará os links que fazem referência a cursos.
* Em seguida a abertura das páginas dos links, será feita a leitura dos campos: (Titulo, Professores, Carga Horária e Descrição).
* Será aplicado a validação das informações lidas e estando válidas será salva na tabela **Consulta** do banco de dados SqlServer.
* No final será aguardado o pressionamento de alguma tecla para finalizar a aplicação.
