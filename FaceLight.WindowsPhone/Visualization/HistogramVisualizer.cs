#region Header

//
//   Project:           FaceLight - Simple Silverlight Real Time Face Detection.
//
//   Changed by:        $Author$
//   Changed on:        $Date$
//   Changed in:        $Revision$
//   Project:           $URL$
//   Id:                $Id$
//
//
//   Copyright (c) 2010 Rene Schulte
//
//   This program is open source software. Please read the License.txt.
//

#endregion

namespace FaceLight
{
#warning Not needed
#if false
    /// <summary>
    /// A visualizer for the column / row Histogram.
    /// </summary>
   public class HistogramVisualizer : IVisualizer
   {
      public Histogram Histogram { get; set; }
      public int Scale { get; set; }
      public bool ShowMax { get; set; }

      public HistogramVisualizer()
      {
         this.Scale = 32;
         this.ShowMax = true;
      }

      public void Visualize(ISimpleBitmap surface)
      {
         var w = surface.PixelWidth;
         var h = surface.PixelHeight;
         var scale = this.Scale;
         var histogram = this.Histogram;
         var histX = histogram.X;
         var histY = histogram.Y;

         // Histogram X
         for (int x = 0; x < w; x++)
         {
            var hx = histX[x];
            if (hx != 0)
            {
               var norm = (int)(((float)hx / histogram.Max.X) * scale);
               surface.DrawLine(x, h - 1, x, h - norm, Colors.Green);
            }
         }
         // Draw 3 times due to resizing a one pixel line might not be visible
         if (this.ShowMax)
         {
            surface.DrawLine(histogram.MaxIndex.X - 1, h - 1, histogram.MaxIndex.X - 1, 0, Colors.Yellow);
         surface.DrawLine(histogram.MaxIndex.X, h - 1, histogram.MaxIndex.X, 0, Colors.Yellow);
            surface.DrawLine(histogram.MaxIndex.X + 1, h - 1, histogram.MaxIndex.X + 1, 0, Colors.Yellow);
         }

         // Histogram Y
         for (int y = 0; y < h; y++)
         {
            var hy = histY[y];
            if (hy != 0)
            {
               var norm = (int)(((float)hy / histogram.Max.Y) * scale);
               surface.DrawLine(w - 1, y, w - norm, y, Colors.Blue);
            }
         }
         // Draw 3 times due to resizing a one pixel line might not be visible
         if (this.ShowMax)
         {
            surface.DrawLine(w - 1, histogram.MaxIndex.Y - 1, 0, histogram.MaxIndex.Y - 1, Colors.Yellow);
         surface.DrawLine(w - 1, histogram.MaxIndex.Y, 0, histogram.MaxIndex.Y, Colors.Yellow);
            surface.DrawLine(w - 1, histogram.MaxIndex.Y + 1, 0, histogram.MaxIndex.Y + 1, Colors.Yellow);
         }
      }
   }
#endif
}