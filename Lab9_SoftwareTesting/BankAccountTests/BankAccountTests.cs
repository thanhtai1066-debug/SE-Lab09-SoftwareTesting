using BankAccountApp;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
namespace BankAccountTests;
[TestClass]
public class BankAccountTests
{
    [DataTestMethod]
    [DataRow(1000,200,800)]
    [DataRow(500,500,0)]
    [DataRow(1,1,0)]
    public void Debit_Valid(int start, int amount, int expected)
    {
        var acc=new BankAccount("Test",start);
        acc.Debit(amount);
        Assert.AreEqual((decimal)expected,acc.Balance);
    }
    [TestMethod]
    public void Debit_ExceedBalance_Throws()
        => Assert.ThrowsException<InvalidOperationException>(() => new BankAccount("Test",100m).Debit(101m));
    [TestMethod]
    public void Debit_ZeroOrNegative_Throws()
    {
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BankAccount("Test",100m).Debit(0));
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BankAccount("Test",100m).Debit(-1));
    }
    [DataTestMethod]
    [DataRow(100,20,120)]
    [DataRow(0,1,1)]
    public void Credit_Valid(int start,int amount,int expected)
    {
        var acc=new BankAccount("Test",start);
        acc.Credit(amount);
        Assert.AreEqual((decimal)expected,acc.Balance);
    }
    [TestMethod]
    public void Credit_Zero_Throws()
        => Assert.ThrowsException<ArgumentOutOfRangeException>(() => new BankAccount("Test",100m).Credit(0));
    [TestMethod]
    public void Withdraw_DelegatesToDebit()
    {
        var acc=new BankAccount("Test",100m);
        acc.Withdraw(30m);
        Assert.AreEqual(70m, acc.Balance);
    }
    [TestMethod]
    public void Withdraw_OverBalance_Throws()
        => Assert.ThrowsException<InvalidOperationException>(() => new BankAccount("Test",100m).Withdraw(101m));

    public class BankCsvRow
    {
        [Name("customerName")] public string CustomerName {get;set;} = "";
        [Name("initialBalance")] public decimal InitialBalance {get;set;}
        [Name("debitAmount")] public decimal DebitAmount {get;set;}
        [Name("expectedBalance")] public string ExpectedBalance {get;set;} = "";
    }
    public static IEnumerable<object[]> CsvCases()
    {
        using var reader=new StreamReader(Path.Combine(AppContext.BaseDirectory,"BankAccountTestData.csv"));
        using var csv=new CsvReader(reader,CultureInfo.InvariantCulture);
        foreach(var row in csv.GetRecords<BankCsvRow>())
            yield return new object[] {row.CustomerName,row.InitialBalance,row.DebitAmount,row.ExpectedBalance};
    }
    [DataTestMethod]
    [DynamicData(nameof(CsvCases), DynamicDataSourceType.Method)]
    public void Debit_FromCsv(string name,decimal initial,decimal amount,string expected)
    {
        var account=new BankAccount(name,initial);
        if (expected=="Insufficient funds")
            Assert.ThrowsException<InvalidOperationException>(()=>account.Debit(amount));
        else
        {
            account.Debit(amount);
            Assert.AreEqual(decimal.Parse(expected,CultureInfo.InvariantCulture),account.Balance);
        }
    }
}
