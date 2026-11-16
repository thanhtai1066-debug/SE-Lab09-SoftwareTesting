# Lab 9 - Software Testing

Source projects for Exercise 1-4 based on SELab9_2025_SW_Testing.pdf.

## Projects

- `BasicMath`: arithmetic console application (Add, Subtract, Multiply, Divide).
- `BasicMathTests`: MSTest EP/BVA data rows, including divide-by-zero.
- `BasicMathCsvTests`: xUnit/CsvHelper four operation datasets.
- `BankAccountApp`: Debit, Credit, Withdraw with input validation.
- `BankAccountTests`: MSTest EP/BVA and CSV-driven tests.
- `LoginAutomation`: Selenium Chrome + NUnit + CSV on local `login.html` test fixture.

## Run

1. Install .NET 8 SDK, Visual Studio with .NET workload, Google Chrome.
2. Open `Lab9_SoftwareTesting.sln` in Visual Studio.
3. Restore packages and build the solution.
4. Open Test Explorer and choose Run All, or execute `dotnet test Lab9_SoftwareTesting.sln`.
5. For a .trx summary: `dotnet test --logger "trx;LogFileName=Lab9Results.trx"`.
6. Chrome is needed for Selenium. Browser driver resolution is handled by Selenium Manager on supported systems; network access may be required initially.

## Important implementation clarifications

- The handout shows MSTest, xUnit and NUnit examples; the projects deliberately keep a consistent framework within each test project.
- CSV overflow/underflow expected values are mathematical `double` values because `BasicMaths` operates on `double`; unchecked `int` wraparound from an example is not applicable.
- Local HTML had no success/failure feedback in the source exercise, so the fixture now adds a small JavaScript demo status for meaningful assertions. It is **not a production authentication service**.
- Bank account now rejects zero/negative transfer amounts. This is a defensible extra input validation beyond the minimal sample in the handout.
- All tests are provided as runnable examples but were **not executed on the authoring environment** (no installed .NET SDK / Chrome).
- Add genuine Visual Studio Test Explorer screenshots and GitHub URL to the report before submission.

## GitHub

Repository suggestion: `SE-Lab09-Software-Testing`. Do not commit secrets or `TestResults/`.
