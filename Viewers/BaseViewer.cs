using System;
using Gtk;
using Gdk;
using ProceduralContent.Noise;

namespace Viewers
{
	public abstract partial class BaseViewer : Gtk.Window
	{
		IField field;
		protected abstract IField InitField();

		int xScale;
		int yScale;

		public BaseViewer() : this(1, 1){}

		public BaseViewer (int xScale, int yScale) : 
				base(Gtk.WindowType.Toplevel)
		{
			this.xScale = xScale;
			this.yScale = yScale;

			this.field = InitField();
			
			this.Shown += (sender, e) => {
				image1.Pixmap = GetPixmap(512, 512);
			};

			this.DeleteEvent += (o, args) => {
				Application.Quit();
			};

			this.Build ();
		}

		private Pixmap GetPixmap(int width, int height)
		{			
			Pixmap pixmap = new Pixmap(this.GdkWindow, width, height);
			Gdk.GC gc = new Gdk.GC(this.GdkWindow);	

			gc.RgbBgColor = new Gdk.Color(0,0,0);
			for(int x = 0; x < width; x++)
			{
				for(int y = 0; y < height; y++)
				{
					gc.RgbFgColor = ColorFromValue(field[x * xScale, y * yScale]);
					pixmap.DrawPoint(gc, x, y);
				}
			}

			return pixmap;
		}

		protected virtual Gdk.Color ColorFromValue(double noise)
		{
			double min = Math.Min (byte.MaxValue, byte.MaxValue * noise);
			byte final = (byte) (byte.MaxValue * Math.Max(byte.MinValue, min));

			return new Gdk.Color(final, final, final);
		}
	}
}
