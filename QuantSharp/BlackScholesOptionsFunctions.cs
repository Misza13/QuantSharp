// ReSharper disable InconsistentNaming
namespace QuantSharp
{
    using System;
    using MathNet.Numerics.Distributions;

    public static class BlackScholesOptionsFunctions
    {
        private static readonly Normal Normal01 = new Normal(0, 1);
        
        /// <summary>
        /// Price of an european-style CALL option.
        /// </summary>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double EuroCall(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return + S * Phi(dPlus)
                   - K * Math.Exp(-r * T) * Phi(dMinus);
        }
        
        /// <summary>
        /// Price of an european-style PUT option.
        /// </summary>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double EuroPut(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return - S * Phi(-dPlus)
                   + K * Math.Exp(-r * T) * Phi(-dMinus);
        }

        /// <summary>
        /// Delta of an european-style CALL option,
        /// i.e. the derivative of the option's price with respect to price of the underlying instrument.
        /// </summary>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double DeltaEuroCall(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, _) = DPlusMinus(S, K, T, sigma, r);
            return Phi(dPlus);
        }
        
        /// <summary>
        /// Delta of an european-style PUT option,
        /// i.e. the derivative of the option's price with respect to price of the underlying instrument.
        /// </summary>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double DeltaEuroPut(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, _) = DPlusMinus(S, K, T, sigma, r);
            return -Phi(-dPlus);
        }
        
        /// <summary>
        /// Gamma of an european-style option (same for CALL and PUT),
        /// i.e. the second derivative of the option's price with respect to price of the underlying instrument.
        /// It reflects the convexity of Delta.
        /// </summary>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double GammaEuro(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, _) = DPlusMinus(S, K, T, sigma, r);
            return Norm(dPlus) / (S * sigma * Math.Sqrt(T));
        }
        
        /// <summary>
        /// Theta of an european-style CALL option,
        /// i.e. the derivative of the option's price with respect to passing of time.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the derivative of option price
        /// with respect to time. To get Theta value per the conventional understanding
        /// ("per 1 day"), divide the result by 365.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double ThetaEuroCall(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return -S * Norm(dPlus) * sigma / (2 * Math.Sqrt(T))
                   - r * K * Math.Exp(-r * T) * Phi(dMinus);
        }
        
        /// <summary>
        /// Theta of an european-style PUT option,
        /// i.e. the derivative of the option's price with respect to passing of time.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the derivative of option price
        /// with respect to time. To get Theta value per the conventional understanding
        /// ("per 1 day"), divide the result by 365.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double ThetaEuroPut(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return - S * Norm(dPlus) * sigma / 2 / Math.Sqrt(T)
                   + r * K * Math.Exp(-r * T) * Phi(-dMinus);
        }
        
        /// <summary>
        /// Vega of an european-style option (same for CALL and PUT),
        /// i.e. the derivative of option price with respect to volatility.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the derivative of option price
        /// with respect to volatility. To get Vega value per the conventional understanding
        /// ("per 1% of volatility change"), divide the result by 100.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double VegaEuro(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, _) = DPlusMinus(S, K, T, sigma, r);
            return S * Norm(dPlus) * Math.Sqrt(T);
        }

        /// <summary>
        /// Vomma (also known as "Volga") of an european-style option (same for CALL and PUT),
        /// i.e. the second-order derivative of option price with respect to volatility.
        /// It reflects the convexity of Vega.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the second-order derivative of option price
        /// with respect to volatility. To get Vomma value per the conventional understanding
        /// ("per 1% of volatility change"), divide the result by 100.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double VommaEuro(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (dPlus, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return S * Norm(dPlus) * Math.Sqrt(T) * dPlus * dMinus / sigma;
        }
        
        /// <summary>
        /// Rho of an european-style CALL option,
        /// i.e. the derivative of option price with respect to risk-free interest rate.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the derivative of option price
        /// with respect to risk-free rate. To get Rho value per the conventional understanding
        /// ("per 1% of rate change"), divide the result by 100.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double RhoEuroCall(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (_, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return K * T * Math.Exp(-r * T) * Phi(dMinus);
        }
        
        /// <summary>
        /// Rho of an european-style PUT option,
        /// i.e. the derivative of option price with respect to risk-free interest rate.
        /// </summary>
        /// <remarks>
        /// <b>Important:</b> The value returned is the proper value of the derivative of option price
        /// with respect to risk-free rate. To get Rho value per the conventional understanding
        /// ("per 1% of rate change"), divide the result by 100.
        /// </remarks>
        /// <param name="S">Price of the underlying instrument</param>
        /// <param name="K">Strike price of the option</param>
        /// <param name="T">Time to expiration (in years)</param>
        /// <param name="sigma">Annual volatility of the underlying instrument (as fraction, e.g. 20% is 0.2)</param>
        /// <param name="r">Risk-free interest rate (as fraction, e.g. 5% is 0.05)</param>
        public static double RhoEuroPut(
            double S,
            double K,
            double T,
            double sigma,
            double r)
        {
            var (_, dMinus) = DPlusMinus(S, K, T, sigma, r);
            return -K * T * Math.Exp(-r * T) * Phi(-dMinus);
        }

        private static (double, double) DPlusMinus(double S, double K, double T, double sigma, double r)
        {
            var dPlus = (Math.Log(S / K) + (r + sigma * sigma / 2) * T) / (sigma * Math.Sqrt(T));
            var dMinus = dPlus - sigma * Math.Sqrt(T);
            return (dPlus, dMinus);
        }

        private static double Norm(double x) => Normal01.Density(x);

        private static double Phi(double x) => Normal01.CumulativeDistribution(x);
    }
}
