# Welcome to QuantSharp documentation

Please refer to the quickstart guide below or dive straight into the [API documentation](api/QuantSharp.html).

## Quick start

### Installation

Install QuantSharp like you would any ordinary NuGet package:

```powershell
Install-Package QuantSharp
```

or

```cmd
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