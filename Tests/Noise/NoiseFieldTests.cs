using NUnit.Framework;
using ProceduralContent.Noise;
using System.Linq;
using System;

namespace Tests.Noise
{
    public class SingleValueRandom : IRandom
    {
        double _retval;

        public SingleValueRandom(double retval)
        {
            _retval = retval;
        }

        public double NextDouble(double seed)
        {
            return _retval;
        }
    }

    [TestFixture]
    public class NoiseFieldTests
    {
        [Test]
        public void GeneratesUnsetVectors()
        {
            double rand = .5;
            IRandom random = new SingleValueRandom(rand);
            NoiseField field = new NoiseField(new NoiseField.Context(2, 10, random));
            double d = field[0, 0];

            Assert.AreEqual(rand, d);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void MismatchException()
        {
            double rand = .5;
            IRandom random = new SingleValueRandom(rand);
            NoiseField field = new NoiseField(new NoiseField.Context(2, 10, random));
            double d = field[0, 0, 0];
        }

        [Test]
        public void NoiseFieldScales()
        {
            double rand = 1.0;
            double scale = 0.5;
            IRandom random = new SingleValueRandom(rand);
            NoiseField field = new NoiseField(new NoiseField.Context(2, 10, random, scale));

            Assert.AreEqual(scale, field[0, 0]);
        }

        [Test]
        public void GetsMultipleValues_2D()
        {
            double rand = .5;
            IRandom random = new SingleValueRandom(rand);
            NoiseField field = new NoiseField(new NoiseField.Context(2, 10, random));

            double[,] fieldValues = new double[byte.MaxValue, byte.MaxValue];
            int offsetx = -40;
            int offsety = -60;

            int size = 50;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {                    
                    fieldValues[x, y] = field[x + offsetx, y + offsety];
                }
                Console.Write("processing x = {0}  \r", x);
            }
            Console.WriteLine();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Assert.AreEqual(rand, fieldValues[x, y]);
                }
                Console.Write("testing x = {0}  \r", x);
            }
            Console.WriteLine();
        }

        [Test]
        public void GetsMultipleValues_3D()
        {
            double rand = .5;
            IRandom random = new SingleValueRandom(rand);
            NoiseField field = new NoiseField(new NoiseField.Context(3, 10, random));

            double[,,] fieldValues = new double[100, 100, 100];
            int offsetx = -40;
            int offsety = -60;
            int offsetz = 467;

            int size = 50;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        fieldValues[x, y, z] = field[x + offsetx, y + offsety, z + offsetz];
                    }
                }
                Console.Write("processing x = {0}   \r", x); 
            }
            Console.WriteLine();

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        Assert.AreEqual(rand, fieldValues[x, y, z]);
                    }
                }
                Console.Write("testing x = {0}   \r", x);
            }
            Console.WriteLine();
        }
    }
}