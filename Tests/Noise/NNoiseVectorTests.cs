using NUnit.Framework;
using System;
using ProceduralContent.Noise;

namespace Tests.Noise
{
    [TestFixture]
    public class NNoiseVectorTests
    {
        [Test]
        public void VariationProducesDifferentResults()
        {
            INoisable n1 = NNoiseVector.New(3, 4, 5);
            INoisable n2 = NNoiseVector.New(5, 4, 3);

            Console.Write("Testing that {0} != {1}", n1.NoiseHash, n2.NoiseHash);
            Assert.AreNotEqual(n1.NoiseHash, n2.NoiseHash);
        }

        [Test]
        public void VariableInitialization()
        { 
            Random random = new Random(Environment.TickCount);

            int[] d = new int[10];
            for (int i = 0; i < d.Length; i++ )
            {
                d[i] = random.Next();
            }

            INoisable n = NNoiseVector.New(d);
            Console.WriteLine("NoiseHash is {0}", n.NoiseHash);

            Assert.IsNotNull(n);
        }

        [Test]
        public void DifferentNoiseHashesHaveDifferentHashes()
        {
            INoisable n1 = NNoiseVector.New(3, 4, 5);
            INoisable n2 = NNoiseVector.New(5, 4, 3);

            Console.Write("Testing that {0} != {1}", n1.NoiseHash.GetHashCode(), n2.NoiseHash.GetHashCode());
            Assert.AreNotEqual(n1.NoiseHash.GetHashCode(), n2.NoiseHash.GetHashCode());
        }

        [Test]
        public void SameNoiseHashGeneratesConsistentHash()
        {
            INoisable n1 = NNoiseVector.New(3, 4, 5);
            INoisable n2 = NNoiseVector.New(3, 4, 5);

            Console.Write("Testing that {0} == {1}", n1.NoiseHash.GetHashCode(), n2.NoiseHash.GetHashCode());
            Assert.AreEqual(n1.NoiseHash.GetHashCode(), n2.NoiseHash.GetHashCode());
        }

        [Test]
        public void RemembersDimensions()
        {
            INVector noisable = NVector.New(0, 1, 2, 3, 4);

            Assert.AreEqual(5, noisable.Dimensions);
        }
    }
}
