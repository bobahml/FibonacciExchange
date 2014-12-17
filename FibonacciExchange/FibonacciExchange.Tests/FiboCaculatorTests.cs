using System;
using System.Numerics;
using FibonacciExchangeCommon.Calculator;
using NUnit.Framework;

namespace FibonacciExchange.Tests
{
    [TestFixture]
    internal class FiboCaculatorTests
    {
        [Test]
        public void ZeroInput()
        {
            var fc = new SlowFiboCaculator();
            var res = fc.CalcNext(BigInteger.Zero.ToByteArray());

            Assert.AreEqual(BigInteger.One, new BigInteger(res));
        }

        [Test]
        public void ValidInput()
        {
            var fc = new SlowFiboCaculator();
            var res = fc.CalcNext(new BigInteger(233).ToByteArray());

            Assert.AreEqual(new BigInteger(377), new BigInteger(res));
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidInput()
        {
            var fc = new SlowFiboCaculator();
            fc.CalcNext(new BigInteger(232).ToByteArray());
        }
    }
}
