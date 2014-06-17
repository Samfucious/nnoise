using System;

namespace ProceduralContent.Noise
{   
    public class Noiser
    {
        IRandom _random;

        public Noiser()
            : this(new DefaultRandom())
        { }

        public Noiser(IRandom random)
        {
            _random = random;
        }

        public double GetValue(INoisable noisable)
        {
            return _random.NextDouble(noisable.NoiseHash);
        }
    }
}