using System;

namespace ProceduralContent.Noise
{
    public interface IRandom
    {
        double NextDouble(double seed);
    }

    public class DefaultRandom : IRandom
    {
        int mMask;

        public DefaultRandom() : this(0)
        {
        }

        public DefaultRandom(int mask)
        {
            mMask = mask;
        }

        public double NextDouble(double seed)
        {
            Random random = new Random(seed.GetHashCode() ^ mMask);
            return random.NextDouble();
        }
    }
}