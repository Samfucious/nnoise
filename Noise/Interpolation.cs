using System;

namespace ProceduralContent.Noise
{
    public delegate double Interpolator(double v1, double v2, double t);

    public static class Interpolation
    {
        public static double LinearInterpolate(double v1, double v2, double t)
        {
            return (v1 * (1 - t) + v2 * t);
        }

        public static double CosineInterpolation(double v1, double v2, double t)
        {
            double t2 = (1.0 - Math.Cos(t * Math.PI)) / 2.0;
            return ((v1 * (1 - t2)) + v2 * t2);
        }
    }
}