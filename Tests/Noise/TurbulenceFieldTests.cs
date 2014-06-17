using NUnit.Framework;
using System;
using ProceduralContent.Noise;

namespace Tests.Noise
{
    [TestFixture]
    public class TurbulenceFieldTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void DimensionMismatchThrows()
        {
            double randomRetval = .5;
            SingleValueRandom random = new SingleValueRandom(randomRetval);
            NoiseField.Context c1 = new NoiseField.Context(2, 5, random);
            NoiseField.Context c2 = new NoiseField.Context(3, 5, random);
            TurbulenceField.Context context = new TurbulenceField.Context(2, c1, c2);
            TurbulenceField field = new TurbulenceField(context);
        }

        [Test]
        public void TwoLayersAddedProperly()
        {
            double randomRetval = .5;
            SingleValueRandom random = new SingleValueRandom(randomRetval);
            NoiseField.Context c1 = new NoiseField.Context(2, 5, random);
            NoiseField.Context c2 = new NoiseField.Context(2, 5, random);
            TurbulenceField.Context context = new TurbulenceField.Context(2, c1, c2);
            TurbulenceField field = new TurbulenceField(context);

            int size = 100;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Assert.AreEqual(randomRetval * 2.0, field[x, y]);
                }
            }
        }

        [Test]
        public void TwoLayersAddedProperlyWithDifferentScales()
        {
            double randomRetval = .5;
            SingleValueRandom random = new SingleValueRandom(randomRetval);
            NoiseField.Context c1 = new NoiseField.Context(2, 10, random);
            NoiseField.Context c2 = new NoiseField.Context(2, 5, random, 0.5);
            TurbulenceField.Context context = new TurbulenceField.Context(2, c1, c2);
            TurbulenceField field = new TurbulenceField(context);

            int size = 100;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Assert.AreEqual(randomRetval * 1.5, field[x, y]);
                }
            }
        }
    }
}