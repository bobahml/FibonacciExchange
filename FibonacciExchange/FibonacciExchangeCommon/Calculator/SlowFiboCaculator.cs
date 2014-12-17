using System;
using System.Numerics;

namespace FibonacciExchangeCommon.Calculator
{
    public interface IFiboCaculator
    {
        byte[] CalcNext(byte[] current);

        byte[] CalcNext(byte[] prev, byte[] current);
    }

    public class SlowFiboCaculator : IFiboCaculator
    {
        public byte[] CalcNext(byte[] currentData)
        {
            var current = ToBigInteger(currentData);
     
            var a = BigInteger.Zero;
            var b = BigInteger.One;

            while (true)
            {
                var temp = a+b;

                var cmp = temp.CompareTo(current);
                if (cmp >= 0)
                {
                    if (cmp != 0)
                    {
                        if (current.Equals(BigInteger.Zero))
                            return BigInteger.One.ToByteArray();
                        throw new ArgumentException("Number is not a member of sequence.");
                    }
                       
                    return (temp + b).ToByteArray();
                }

                a = b;
                b = temp;
            }
        }

        public byte[] CalcNext(byte[] prevData, byte[] currentData)
        {
            var prev = ToBigInteger(prevData);
            var cur = ToBigInteger(currentData);

            return (cur + prev).ToByteArray();
        }

        private static BigInteger ToBigInteger(byte[] current)
        {
            var cur = new BigInteger(current);
            if (cur.Sign < 0)
                throw new ArgumentOutOfRangeException("currentData");
            return cur;
        }
    }
}