using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceduralContent.Noise
{
    public class NoiseField : IField
    {
        NVectorSpace _vectorSpace;
        NVectorSpace VectorSpace
        {
            get 
            {
                lock (this)
                {
                    return _vectorSpace = _vectorSpace ?? NVectorSpace.New(_context.Dimensions);
                }
            }
        }

        int Wavelength
        {
            get 
            {
                return _context.Wavelength;
            }
        }

        IRandom Rand
        {
            get 
            {
                return _context.Random;
            }
        }

        Func<double, double, double, double> InterpolationFunc
        {
            get 
            {
                return _context.InterpolationFunc ?? Interpolation.CosineInterpolation;
            }
        }

        public class Context
        {
            public int Dimensions { get; private set; }
            public int Wavelength { get; private set; }
            public IRandom Random { get; private set; }
            public double Scale { get; private set; }
            public Func<double, double, double, double> InterpolationFunc { get; private set; }

            public Context(int dimensions, int wavelength, IRandom random, double scale = 1.0, Func<double, double, double, double> interpolationFunc = null)   
            {
                Dimensions = dimensions;
                Wavelength = wavelength;
                Random = random;
                Scale = scale;
                InterpolationFunc = interpolationFunc;
            }            
        }

        Context _context;

        public NoiseField(Context context)
        {
            _context = context;  
        }

        public double this[params int[] coordinates]
        {
            get 
            {
                NNoiseVector nvector = (NNoiseVector)VectorSpace[coordinates];
                if (nvector == null)
                {
                    List<double> dependencies = new List<double>();
                    for (int i = 0; i < coordinates.Length; i++)
                    {
                        int remainder = coordinates[i] % Wavelength;
                        if (remainder != 0)
                        {
                            int[] copyLow = new int[coordinates.Length];
                            coordinates.CopyTo(copyLow, 0);
                            int waveLow = coordinates[i] - remainder;
                            copyLow[i] = waveLow;

                            int[] copyHigh = new int[coordinates.Length];
                            coordinates.CopyTo(copyHigh, 0);
                            int waveHigh = coordinates[i] + (Wavelength - remainder);
                            copyHigh[i] = waveHigh;

                            double t = ((double)remainder) / ((double)Wavelength);
                            double interpolation = InterpolationFunc(this[copyLow], this[copyHigh], t);
                            dependencies.Add(interpolation);
                        }
                    }

                    nvector = NNoiseVector.New(coordinates);
                    nvector.Data = dependencies.Count > 0 ? dependencies.Sum() / dependencies.Count : Rand.NextDouble(nvector.NoiseHash) * _context.Scale;
                    VectorSpace[coordinates] = nvector;
                }
                return ((NVector<double>)nvector).Data;
            }
        }
    }
}