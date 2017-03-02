using System;
using System.Collections.Generic;

namespace ProceduralContent.Noise
{
    public class NVectorSpaceAxis : INVector
    {
        public int Dimensions
        {
            get;
            private set;
        }

        public int[] Coordinates
        {
            get { throw new NotImplementedException(); }
        }

        Dictionary<int, INVector> axis = new Dictionary<int, INVector>();

        public INVector this[int depth, int[] coordinates]
        {
            get
            {
                int coordinate = coordinates[depth];
                INVector result;

                if (!axis.TryGetValue(coordinate, out result))
                {
                    return null;
                }
                else
                {
                    if (depth < coordinates.Length - 1)
                    {
                        return ((NVectorSpaceAxis)axis[coordinate])[depth + 1, coordinates];
                    }
                    else 
                    {
                        return axis[coordinate];
                    }
                }
            }
            set
            {
                int coordinate = coordinates[depth];
                if (depth < coordinates.Length - 1)
                {
                    INVector result;
                    if (!axis.TryGetValue(coordinate, out result))
                    {
                        axis[coordinate] = result = new NVectorSpaceAxis(Dimensions - 1);
                    }
                    ((NVectorSpaceAxis)result)[depth + 1, coordinates] = value;
                }
                else
                {
                    axis[coordinate] = value;
                }
            }
        }

        public NVectorSpaceAxis(int dimensions)
        {
            if (dimensions < 1)
            {
                throw new ArgumentOutOfRangeException("There is a minimum requrement of 1 dimension.");
            }
            Dimensions = dimensions;
        }

        public double NoiseHash
        {
            get { return this.GetHashCode(); }
        }

        public void Clear()
        {
            axis.Clear();
        }
    }
}
