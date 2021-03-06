**QuantSharp** is a .NET library for financial quants.

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

**Note**: QuantSharp is currently in pre-release status and APIs can be expected to break without regard for semantic versioning
(a bump in minor version usually signifies an actually major change).

## Features

### Current

* Calculations for European vanilla options
  * Option price
  * Common greeks: delta, gamma, theta, vega, rho
  * Uncommon greeks: Vomma
  * Implied volatility

To read full API documentation, see: https://quantsharp-docs.github.io/

### Roadmap for 1.0 release

These features are strongly considered "necessary" for a proper 1.0 release:
* Pricing for American options
* More uncommon greeks
* Fluent API for creating options (e.g. creating a partially parametrized option and performing multiple calculations against one of the free params)
* Figure out approach to mathematical understanding of greeks (as raw derivative values) vs conventional (e.g. "per 1% change ", "decay per 1 day")
* Plug-in pricing models (to allow other models in the future, e.g. binomial)

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

var callOptionPrice = BlackScholesEuropeanOptionsFunctions.CallPrice(
    234.56, //Price of underlying
    210.00, //Strike price
    0.25, //Time to expiration (in years, i.e. 3 months here)
    0.15, //Annualized volatility (15% here)
    0.03 //Risk-free interest rate (3% here)
);
```