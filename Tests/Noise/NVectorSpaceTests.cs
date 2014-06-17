using NUnit.Framework;
using System;
using ProceduralContent.Noise;

namespace Tests.Noise
{
    public abstract class NVectorSpaceTests
    {
        protected int Dimensions
        {
            get;
            set;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void NoZeroDimensionalVectorSpace()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(0);
        }

        [Test]
        public void NewVectorSpace()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(Dimensions);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NVectorDimensionMismatch()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(Dimensions);
            int[] coordinates = new int[Dimensions - 1];
            for (int i = 0; i < Dimensions - 1; i++)
            {
                coordinates[i] = i;
            }
            INVector noisable = NVector.New(coordinates);
            vectorSpace[noisable.Coordinates] = noisable;
        }

        [Test]
        public void AddVectorToVectorSpace()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(Dimensions);
            int[] coordinates = new int[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                coordinates[i] = i;
            }
            INVector noisable = NVector.New(coordinates);
            vectorSpace[noisable.Coordinates] = noisable;
        }

        [Test]
        public void RetrieveVectorFromVectorSpace()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(Dimensions);
            int[] coordinates = new int[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                coordinates[i] = i;
            }

            NVector<int> noisable = NVector<int>.New(coordinates);
            noisable.Data = 04812;
            vectorSpace[noisable.Coordinates] = noisable;

            NVector<int> retrived = (NVector<int>)vectorSpace[coordinates];
            Assert.AreEqual(noisable.Data, retrived.Data);
        }

        [Test]
        public void FetchesNullWhenNoVector()
        {
            NVectorSpace vectorSpace = NVectorSpace.New(Dimensions);
            int[] coordinates = new int[Dimensions];
            for (int i = 0; i < Dimensions; i++)
            {
                coordinates[i] = i;
            }
            NVector<int> retrived = (NVector<int>)vectorSpace[coordinates];
            Assert.IsNull(retrived);
        }
    }

    [TestFixture]
    public class NVectorSpaceTests_OneDimensional : NVectorSpaceTests
    {
        public NVectorSpaceTests_OneDimensional()
        {
            Dimensions = 1;
        }
    }

    [TestFixture]
    public class NVectorSpaceTests_TwoDimensional : NVectorSpaceTests
    {
        public NVectorSpaceTests_TwoDimensional()
        {
            Dimensions = 2;
        }
    }

    [TestFixture]
    public class NVectorSpaceTests_ThreeDimensional : NVectorSpaceTests
    {
        public NVectorSpaceTests_ThreeDimensional()
        {
            Dimensions = 3;
        }
    }

    [TestFixture]
    public class NVectorSpaceTests_FourDimensional : NVectorSpaceTests
    {
        public NVectorSpaceTests_FourDimensional()
        {
            Dimensions = 4;
        }
    }
}