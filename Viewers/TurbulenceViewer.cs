using System;
using ProceduralContent.Noise;
using System.Collections.Generic;
using Gtk;

namespace Viewers
{
	public class TurbulenceViewer : BaseViewer
	{
		protected override IField InitField()
		{
			int wavelength = 1024;
			double scale = 1.0;
			
			List<NoiseField.Context> contexts = new List<NoiseField.Context>(); 
			
			for(int i = 0; i < 7; i++)
			{
				contexts.Add(new NoiseField.Context(2, wavelength, new DefaultRandom(), scale, Interpolation.CosineInterpolation));
				wavelength = wavelength >> 1;
				scale = scale * 0.5;
			}

			NoiseField.Context[] contextArray = contexts.ToArray();
			return new TurbulenceField(new TurbulenceField.Context(2, contextArray));
		}

		public TurbulenceViewer () : base(8, 8)
		{
		}

		protected override Gdk.Color ColorFromValue(double noise)
		{
			noise = Math.Min (1.0, noise - 1.0);
			double min = Math.Min (byte.MaxValue, byte.MaxValue * noise);
			double final = Math.Max(byte.MinValue, min);
			
			if(final > 0.0)
			{
				return new Gdk.Color((byte) final, byte.MaxValue, (byte) final);
			}
			return new Gdk.Color(0, 0, (byte) Math.Min (byte.MaxValue, Math.Abs(final) + (byte.MaxValue / 2)));
		}

		public static void Main(string [] args)
		{
			Application.Init();
			TurbulenceViewer viewer = new TurbulenceViewer();
			viewer.Show();
			Application.Run ();
		}
	}
}

