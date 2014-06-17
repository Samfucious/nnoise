using NUnit.Framework;
using ProceduralContent.Noise;
using System;

namespace Tests.Noise
{
    using InterpolationFunc = System.Func<double, double, double, double>;
    
    public abstract class AbstractInterpolationTests
    {
        static double EPSILON = 1E-15;

        protected InterpolationFunc _func
        {
            get;
            set;
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            VirtualSetup();
        }

        protected abstract void VirtualSetup();

        [Test]
        public void MidpointIsAsAverage()
        {
            double r = _func(0.0, 1.0, 0.5);
            Console.WriteLine("Result  = {0:R} ", r);
            Console.WriteLine("Epsilon = {0:R}", EPSILON);
            
            double expected = 0.5;
            double diff = expected - r;
            Console.WriteLine("diff    = {0:R}", diff);
            Assert.LessOrEqual(diff, EPSILON);
        }

        [Test]
        public void IdenticalResults()
        {
            double r1 = Interpolation.LinearInterpolate(0.0, 1.0, .5);
            double r2 = Interpolation.CosineInterpolation(0.0, 1.0, .5);

            Console.WriteLine("r1 = {0:R} ", r1);
            Console.WriteLine("r2 = {0:R} ", r2);

            Assert.AreEqual(r1, r2, EPSILON);
        }
    }

    public class LinearInterpolationTests : AbstractInterpolationTests
    {
        protected override void VirtualSetup()
        {
            _func = Interpolation.LinearInterpolate;
        }
    }

    public class CosineInterpolationTests : AbstractInterpolationTests
    {
        protected override void VirtualSetup()
        {
            _func = Interpolation.CosineInterpolation;
        }
    }
}