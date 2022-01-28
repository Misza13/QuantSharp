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
            putPrice.Should().BeApproximatelyS(values.Put, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallDelta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callDelta = BlackScholesOptionsFunctions.DeltaEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callDelta.Should().BeApproximatelyS(values.CallDelta, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutDelta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putDelta = BlackScholesOptionsFunctions.DeltaEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putDelta.Should().BeApproximatelyS(values.PutDelta, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void Gamma_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var gamma = BlackScholesOptionsFunctions.GammaEuro(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            gamma.Should().BeApproximatelyS(values.Gamma, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallTheta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callTheta = BlackScholesOptionsFunctions.ThetaEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callTheta.Should().BeApproximatelyS(values.CallTheta, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutTheta_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putTheta = BlackScholesOptionsFunctions.ThetaEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putTheta.Should().BeApproximatelyS(values.PutTheta, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void Vega_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var vega = BlackScholesOptionsFunctions.VegaEuro(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            vega.Should().BeApproximatelyS(values.Vega, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void Vomma_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var vomma = BlackScholesOptionsFunctions.VommaEuro(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            vomma.Should().BeApproximatelyS(values.Vomma, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallRho_ShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var callRho = BlackScholesOptionsFunctions.RhoEuroCall(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            callRho.Should().BeApproximatelyS(values.CallRho, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutRhoShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var putRho = BlackScholesOptionsFunctions.RhoEuroPut(parameters.S, parameters.K, parameters.T, parameters.sigma, parameters.r);
            
            //Assert
            putRho.Should().BeApproximatelyS(values.PutRho, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void CallImpliedVolatilityShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var impVol = BlackScholesOptionsFunctions.ImpliedVolatilityEuroCall(parameters.S, parameters.K, parameters.T, parameters.r, values.Call);
            
            //Assert
            impVol.Should().BeApproximatelyS(parameters.sigma, 4);
        }

        [TestCaseSource(nameof(OptionTestCaseGenerator))]
        public void PutImpliedVolatilityShouldHaveCorrectValue(OptionParameters parameters, OptionValues values)
        {
            //Act
            var impVol = BlackScholesOptionsFunctions.ImpliedVolatilityEuroPut(parameters.S, parameters.K, parameters.T, parameters.r, values.Put);
            
            //Assert
            impVol.Should().BeApproximatelyS(parameters.sigma, 4);
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
                    Call = 12041.09,
                    Put = 9460.930,
                    CallDelta = 0.620807,
                    PutDelta = -0.37919,
                    Gamma = 1.379794e-5,
                    CallTheta = 11908.9,
                    PutTheta = 8787.91,
                    Vega = 17488.89,
                    Vomma = -1045.99,
                    CallRho = 14155.68,
                    PutRho = -17054.2,
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
                    Call = 9.852414,
                    Put = 9.053777,
                    CallDelta = 0.592313,
                    PutDelta = -0.40768,
                    Gamma = 0.016175,
                    CallTheta = 5.00276,
                    PutTheta = 4.20946,
                    Vega = 23.29281,
                    Vomma = -2.26393,
                    CallRho = 25.68639,
                    PutRho = -33.5149,
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
                    Call = 2138.271,
                    Put = 26.79995,
                    CallDelta = 0.966690,
                    PutDelta = -0.03330,
                    Gamma = 4.215222e-5,
                    CallTheta = 303.444,
                    PutTheta = 257.674,
                    Vega = 163.2134,
                    Vomma = 536.7141,
                    CallRho = 528.7919,
                    PutRho = -43.3402,
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
                    Call = 0.559660,
                    Put = 129.3112,
                    CallDelta = 0.027701,
                    PutDelta = -0.97229,
                    Gamma = 0.001147378,
                    CallTheta = 7.16532,
                    PutTheta = 2.17780,
                    Vega = 11.78070,
                    Vomma = 155.3978,
                    CallRho = 2.422483,
                    PutRho = -122.265,
                });
            
            yield return new TestCaseData(
                new OptionParameters
                {
                    S = 103,
                    K = 100,
                    T = 0.5,
                    sigma = 0.15,
                    r = 0.24
                },
                new OptionValues
                {
                    Call = 14.67116,
                    Put = 0.363211,
                    CallDelta = 0.928278,
                    PutDelta = -0.07172,
                    Gamma = 0.012521,
                    CallTheta = 20.9204,
                    PutTheta = -0.365643,
                    Vega = 9.963268,
                    Vomma = 131.8764,
                    CallRho = 40.47074,
                    PutRho = -3.87527,
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
            public double Vomma { get; set; }
            public double CallRho { get; set; }
            public double PutRho { get; set; }
        }
    }
}