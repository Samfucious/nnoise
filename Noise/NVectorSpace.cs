using System.Collections.Generic;
using System;

namespace ProceduralContent.Noise
{
    public class NVectorSpace
    {
        NVectorSpaceAxis rootAxis;

        public int Dimensions
        {
            get;
            private set;
        }

        public INVector this[int[] coordinates]
        {
            get 
            {
                if (coordinates.Length != Dimensions)
                {
                    throw new ArgumentException("Dimension and Ordinal Count mismatch.");
                }
                return rootAxis[0, coordinates];
            }
            set 
            {
                if (coordinates.Length != Dimensions)
                {
                    throw new ArgumentException("Dimension and Ordinal Count mismatch.");
                }
                rootAxis[0, coordinates] = value;
            }
        }

        private NVectorSpace(int dimensions)
        {
            rootAxis = new NVectorSpaceAxis(dimensions);
        }

        public static NVectorSpace New(int dimensions)
        {
            return new NVectorSpace(dimensions) { Dimensions = dimensions };
        }

        public void Clear()
        {
            rootAxis.Clear();
        }
    }
}