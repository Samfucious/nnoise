using System;

namespace ProceduralContent.Noise
{
    public class NNoiseVector : NVector<double>, INoisable
    {
        public double Noise
        {
            get;
            set;
        }

        protected override void ProcessCoordinates()
        {
            NoiseHash = GetNoiseHash();
        }

        private double GetNoiseHash()
        {
            double noisehash = 0.0;
            int direction = 1;

            double modifier = 1.0;
            double phi = (1.0 - Math.Sqrt(5.0)) / 2.0;

            foreach (int i in _coordinates)
            {
                noisehash += i * direction * modifier;
                modifier *= phi;
                direction = -direction;
            }

            return noisehash;
        }

        public double NoiseHash
        {
            get;
            private set;
        }

        public new static NNoiseVector New(params int[] coordinates)
        {
            NNoiseVector vector = new NNoiseVector();
            vector.Coordinates = coordinates;
            return vector;
        }
    }
}
