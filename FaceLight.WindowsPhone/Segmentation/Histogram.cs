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

using FaceLight.Mobile.Math;
using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile.Segmentation
{
    /// <summary>
    /// A Histogram for columns and rows.
    /// </summary>
    public class Histogram
    {
        static Histogram()
        {
            CompareEmptyColor = 0;
        }

        public Histogram(int[] histX, int[] histY)
        {
            X = histX;
            Y = histY;

            // Find maximum value and index (coordinate) for x, y
            int ix = 0, iy = 0, mx = 0, my = 01;
            for (int i = 0; i < histX.Length; i++)
            {
                if (histX[i] > mx)
                {
                    mx = histX[i];
                    ix = i;
                }
            }
            for (int i = 0; i < histY.Length; i++)
            {
                if (histY[i] > my)
                {
                    my = histY[i];
                    iy = i;
                }
            }

            Max = new Vector(mx, my);
            MaxIndex = new Vector(ix, iy);
        }

        public int[] X { get; private set; }
        public int[] Y { get; private set; }
        public Vector Max { get; private set; }
        public Vector MaxIndex { get; private set; }
        public static int CompareEmptyColor { get; set; }

        public static Histogram FromWriteableBitmap(ISimpleBitmap input)
        {
            int[] p = input.Pixels;
            int w = input.PixelWidth;
            int h = input.PixelHeight;
            var histX = new int[w];
            var histY = new int[h];
            int empty = CompareEmptyColor; // = 0

            // Create row and column statistics / histogram
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    if (p[y*w + x] != empty)
                    {
                        histX[x]++;
                        histY[y]++;
                    }
                }
            }

            return new Histogram(histX, histY);
        }
    }
}