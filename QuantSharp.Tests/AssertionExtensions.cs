namespace QuantSharp.Tests
{
    using System;
    using FluentAssertions;
    using FluentAssertions.Numeric;

    public static class AssertionExtensions
    {
        /// <summary>
        /// Asserts a double value approximates another value to within a given number of significant decimal digits.
        /// </summary>
        /// <param name="parent">The <see cref="NumericAssertions{T}"/> object that is being extended</param>
        /// <param name="expectedValue">The expected value to compare with</param>
        /// <param name="significantDigits">Number of significant decimal digits for comparison</param>
        public static AndConstraint<NumericAssertions<double>> BeApproximatelyS(
            this NumericAssertions<double> parent,
            double expectedValue,
            int significantDigits)
        {
            var size = (int) Math.Floor(Math.Log10(expectedValue));
            return parent.BeApproximately(expectedValue, Math.Pow(10, size - significantDigits));
        }
    }
}