using System;

namespace ProceduralContent.Noise
{
    public interface IRandom
    {
        double NextDouble(double seed);
    }

    public class DefaultRandom : IRandom
    {        
        public double NextDouble(double seed)
        {
            Random random = new Random(seed.GetHashCode());
            return random.NextDouble();
        }
    }
}