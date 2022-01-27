namespace QuantSharp.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;
    using QuantSharp;

    [TestFixture]
    public class BlackScholesOptionsFunctionsTests
    {
        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallPrice_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callPrice = BlackScholesOptionsFunctions.EuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);

            //Test
            callPrice.Should().BeApproximatelyS(values.Call, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutPrice_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putPrice = BlackScholesOptionsFunctions.EuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putPrice.Should().BeApproximately(values.Put, 0.01);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallDelta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callDelta = BlackScholesOptionsFunctions.DeltaEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callDelta.Should().BeApproximately(values.CallDelta, 0.0001);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutDelta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putDelta = BlackScholesOptionsFunctions.DeltaEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putDelta.Should().BeApproximately(values.PutDelta, 0.0001);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void Gamma_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var gamma = BlackScholesOptionsFunctions.GammaEuro(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            gamma.Should().BeApproximately(values.Gamma, 0.0001);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallTheta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callTheta = BlackScholesOptionsFunctions.ThetaEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callTheta.Should().BeApproximately(values.CallTheta, 0.01);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutTheta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putTheta = BlackScholesOptionsFunctions.ThetaEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putTheta.Should().BeApproximately(values.PutTheta, 0.01);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void Vega_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var vega = BlackScholesOptionsFunctions.VegaEuro(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            vega.Should().BeApproximately(values.Vega, 0.01);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallRho_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callRho = BlackScholesOptionsFunctions.RhoEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callRho.Should().BeApproximately(values.CallRho, 0.01);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutRhoShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putRho = BlackScholesOptionsFunctions.RhoEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putRho.Should().BeApproximately(values.PutRho, 0.01);
        }

        public static IEnumerable<TestCaseData> OptionTestCaseGenerator()
        {
            // Reference values calculated using https://option-price.com/index.php
            yield return new TestCaseData(
                new OptionParameters
                {
                    S = 65000,
                    K = 64000,
                    T = 0.5,
                    sigma = 0.6,
                    r = 0.05
                },
                new OptionValues
                {
                    Call = 12041.0948,
                    Put = 9460.9291,
                    CallDelta = 0.6208,
                    PutDelta = -0.3792,
                    Gamma = 0,
                    CallTheta = -32.6271,
                    PutTheta = -24.0765,
                    Vega = 174.8889,
                    CallRho = 141.5568,
                    PutRho = -170.5423
                });

            yield return new TestCaseData(
                new OptionParameters
                {
                    S = 60,
                    K = 60,
                    T = 1,
                    sigma = 0.4,
                    r = 0.0134
                },
                new OptionValues
                {
                    Call = 9.8524,
                    Put = 9.0538,
                    CallDelta = 0.5923,
                    PutDelta = -0.4077,
                    Gamma = 0.0162,
                    CallTheta = -0.0137,
                    PutTheta = -0.0115,
                    Vega = 0.2329,
                    CallRho = 0.2569,
                    PutRho = -0.3351
                });

            yield return new TestCaseData(
                new OptionParameters
                {
                    S = 4400,
                    K = 2300,
                    T = 0.25,
                    sigma = 0.8,
                    r = 0.02
                },
                new OptionValues
                {
                    Call = 2138.2716,
                    Put = 26.8003,
                    CallDelta = 0.9667,
                    PutDelta = -0.0333,
                    Gamma = 0,
                    CallTheta = -0.8314,
                    PutTheta = -0.706,
                    Vega = 1.6321,
                    CallRho = 5.2879,
                    PutRho = -0.4334
                });
            
            yield return new TestCaseData(
                new OptionParameters
                {
                    S = 370,
                    K = 500,
                    T = 0.25,
                    sigma = 0.3,
                    r = 0.01
                },
                new OptionValues
                {
                    Call = 0.559668,
                    Put = 129.3112,
                    CallDelta = 0.0277,
                    PutDelta = -0.9723,
                    Gamma = 0.0011,
                    CallTheta = -0.019631,
                    PutTheta = -0.005967,
                    Vega = 0.1178,
                    CallRho = 0.0242,
                    PutRho = -1.2227
                });
        }
        
        public class OptionParameters
        {
            public double S { get; set; }
            public double K { get; set; }
            public double T { get; set; }
            public double sigma { get; set; }
            public double r { get; set; }
        }
        
        public class OptionValues
        {
            public double Call { get; set; }
            public double Put { get; set; }
            public double CallDelta { get; set; }
            public double PutDelta { get; set; }
            public double Gamma { get; set; }
            public double CallTheta { get; set; }
            public double PutTheta { get; set; }
            public double Vega { get; set; }
            public double CallRho { get; set; }
            public double PutRho { get; set; }
        }
    }
}