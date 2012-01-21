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

using FaceLight.Mobile.Filters;
using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile.Visualization
{
    /// <summary>
    /// A visualizer for YCbCr color space that interpolates between Cb and Cr min / max and uses a constant Y.
    /// </summary>
    public class CbCrColorSpaceVisualizer : IVisualizer
    {
        public CbCrColorSpaceVisualizer()
        {
            Min = YCbCrColor.Min;
            Max = YCbCrColor.Max;
            YFactor = 0.5f;
        }

        public YCbCrColor Min { get; set; }
        public YCbCrColor Max { get; set; }
        public float YFactor { get; set; }

        #region IVisualizer Members

        public void Visualize(ISimpleBitmap surface)
        {
            int w = surface.PixelWidth;
            int h = surface.PixelHeight;
            int[] pixels = surface.Pixels;
            YCbCrColor min = Min;
            YCbCrColor max = Max;
            int i;
            float xf, yf, cb, cr;

            // Use constant Y
            float v = min.Y + (max.Y - min.Y)*YFactor;

            // Interpolate between min and max and set pixel
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    i = y*w + x;
                    xf = (float) x/w;
                    yf = (float) y/h;
                    cb = min.Cb + (max.Cb - min.Cb)*xf;
                    cr = min.Cr + (max.Cr - min.Cr)*yf;
                    pixels[y*w + x] = new YCbCrColor(v, cb, cr).ToArgbColori();
                }
            }
        }

        #endregion
    }
}