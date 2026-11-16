using BankAccountApp;
var account = new BankAccount("Demo Customer", 1000m);
account.Credit(100m);
account.Withdraw(250m);
Console.WriteLine($"Remaining balance: {account.Balance}");
