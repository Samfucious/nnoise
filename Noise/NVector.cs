using System;

namespace ProceduralContent.Noise
{
    public class NVector : INVector
    {
        public int[] _coordinates;
        public int[] Coordinates
        {
            get
            {
                return _coordinates;
            }
            protected set
            {
                _coordinates = value;
                ProcessCoordinates();
            }
        }

        protected virtual void ProcessCoordinates()
        {            
        }

        public int Dimensions
        {
            get 
            {
                return Coordinates.Length; 
            }
        }

        protected NVector()
        {
        }

        public static NVector New(params int[] coordinates)
        {
            NVector vector = new NVector();
            vector.Coordinates = coordinates;
            return vector;
        }

        public void Clear()
        {
        }
    }

    public class NVector<TData> : NVector
    {
        public TData Data
        {
            get;
            set;
        }

        public static new NVector<TData> New(int[] coordinates)
        {
            NVector<TData> vector = new NVector<TData>();
            vector.Coordinates = coordinates;
            return vector;
        }
    }
}
