using CsvHelper;
using CsvHelper.Configuration.Attributes;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Globalization;
namespace LoginAutomation;
[TestFixture]
public class LoginTests
{
    public class LoginRow
    {
        [Name("username")] public string Username {get;set;}="";
        [Name("password")] public string Password {get;set;}="";
        [Name("expected")] public string Expected {get;set;}="";
    }
    public static IEnumerable<TestCaseData> LoginCases()
    {
        var file=Path.Combine(AppContext.BaseDirectory,"Data","LoginTestData.csv");
        using var reader=new StreamReader(file);
        using var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        foreach(var row in csv.GetRecords<LoginRow>())
            yield return new TestCaseData(row.Username,row.Password,row.Expected)
                .SetName($"Login_{row.Username}_{row.Expected.Replace(' ','_')}");
    }
    [TestCaseSource(nameof(LoginCases))]
    public void Login_FromCsv_ShowsExpectedMessage(string username,string password,string expected)
    {
        var options=new ChromeOptions();
        options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1280,900");
        using IWebDriver driver=new ChromeDriver(options);
        var html=Path.Combine(AppContext.BaseDirectory,"Data","login.html");
        driver.Navigate().GoToUrl(new Uri(html).AbsoluteUri);
        driver.FindElement(By.Id("username")).SendKeys(username);
        driver.FindElement(By.Id("password")).SendKeys(password);
        driver.FindElement(By.Id("loginButton")).Click();
        Assert.That(driver.FindElement(By.Id("status")).Text,Is.EqualTo(expected));
    }
}
