using System;
using ProceduralContent.Noise;
using Gtk;

namespace Viewers
{
	public class NoiseViewer : BaseViewer
	{
		protected override IField InitField()
		{
			return new NoiseField(new NoiseField.Context(2, 16, new DefaultRandom(), 1.0, Interpolation.CosineInterpolation));	
		}

		public NoiseViewer ()
		{
		}

		public static void Main(string [] args)
		{
			Application.Init();
			NoiseViewer viewer = new NoiseViewer();
			viewer.Show();
			Application.Run ();
		}
	}
}

