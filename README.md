# Discount
Vinted homework

Requires .NET Core 2.0 or later.
Download and install SDK or Runtime from https://www.microsoft.com/net/download

Input file location: Discount\Discount\Resources\input.txt

To start via command line interface:
  1. Go to solution folder, \Discount subfolder (where Discount.csproj project file exists)
  2. dotnet run
    
To run tests:
  1. Go to solution folder, \Discount.Tests subfolder (where Discount.Tests.csproj project file exists)
  2. dotnet test

For code reading purposes entry point is Program.cs

To modify constants (shipping prices, providers, sizes, separators, etc.): Discount\Configuration\Constants.cs

To add new calculation rules:
  1. Define them in Discount\Domain\Utilities\CalculationExtensions.cs
  2. Call them from Discount\Domain\DiscountCalculator.cs line 50.
