using System;

namespace ProceduralContent.Noise
{
    public class TurbulenceField : IField
    {
        public class Context
        {
            public int Dimensions { get; private set; }
            public NoiseField.Context[] LayerContexts
            {
                get;
                private set;
            }

            public Context(int dimensions, params NoiseField.Context[] layerContexts)
            {
                Dimensions = dimensions;
                LayerContexts = layerContexts;
            }
        }

        Context _context;
        NoiseField[] _noiseFields;

        public TurbulenceField(Context context)
        {
            _context = context;
            InitNoiseFields();
        }

        private void InitNoiseFields()
        {
            _noiseFields = new NoiseField[_context.LayerContexts.Length];
            for (int i = 0; i < _noiseFields.Length; i++)
            {
                if (_context.LayerContexts[i].Dimensions != _context.Dimensions)
                {
                    throw new ArgumentException("Dimension mismatch between TurbulenceField and NoiseField.Context.");
                }
                _noiseFields[i] = new NoiseField(_context.LayerContexts[i]);
            }
        }

        public double this[params int[] coordinates]
        {
            get
            {
                double retval = 0.0;
                foreach (NoiseField field in _noiseFields)
                {
                    retval += field[coordinates];
                }
                return retval;
            }
        }
    }
}