**QuantSharp** is a .NET library for financial quants.

Eventually, it will contain a plethora of functions for calculating various aspect of financial instruments.
For start, we will implement calculations for vanilla european-style options using the Black-Scholes model.

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=bugs)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=vulnerabilities)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=code_smells)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=sqale_index)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=coverage)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=Misza13_QuantSharp&metric=duplicated_lines_density)](https://sonarcloud.io/summary/new_code?id=Misza13_QuantSharp)

## Quick start

### Installation

Install QuantSharp like you would any ordinary NuGet package:

```powershell
Install-Package QuantSharp
```

or

```
dotnet add package QuantSharp
```

### Using static functions

```cs
using QuantSharp;

//...

var callOptionPrice = BlackScholesOptionsFunctions.EuroCall(
    234.56, //Price of underlying
    210.00, //Strike price
    0.25, //Time to expiration (in years, i.e. 3 months here)
    0.15, //Annualized volatility (15% here)
    0.03 //Risk-free interest rate (3% here)
);
```