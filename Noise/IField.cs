namespace ProceduralContent.Noise
{
	public interface IField
	{
		double this[params int[] coordinates] { get; }
        void Clear();
	}
}

