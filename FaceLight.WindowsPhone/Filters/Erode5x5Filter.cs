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

using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile.Filters
{
    /// <summary>
    /// Erosion filter with a fixed 5x5 kernel.
    /// </summary>
    public class Erode5x5Filter : BaseFilter
    {
        public Erode5x5Filter(ISimpleBitmapFactory factory)
            : base(factory)
        {
            ResultColor = 0xFFFFFF;
            CompareEmptyColor = 0;
        }

        public int ResultColor { get; set; }
        public int CompareEmptyColor { get; set; }

        public override ISimpleBitmap Process(ISimpleBitmap input)
        {
            int[] p = input.Pixels;
            int w = input.PixelWidth;
            int h = input.PixelHeight;
            ISimpleBitmap result = Factory.Create(w, h);
            int[] rp = result.Pixels;
            int empty = CompareEmptyColor;
            int c, cm;
            int i = 0;

            // Erode every pixel
            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++, i++)
                {
                    // Middle pixel
                    cm = p[y*w + x];
                    if (cm == empty)
                    {
                        continue;
                    }

                    // Row 0
                    // Left pixel
                    if (x - 2 > 0 && y - 2 > 0)
                    {
                        c = p[(y - 2)*w + (x - 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    // Middle left pixel
                    if (x - 1 > 0 && y - 2 > 0)
                    {
                        c = p[(y - 2)*w + (x - 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (y - 2 > 0)
                    {
                        c = p[(y - 2)*w + x];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 1 < w && y - 2 > 0)
                    {
                        c = p[(y - 2)*w + (x + 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 2 < w && y - 2 > 0)
                    {
                        c = p[(y - 2)*w + (x + 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }

                    // Row 1
                    // Left pixel
                    if (x - 2 > 0 && y - 1 > 0)
                    {
                        c = p[(y - 1)*w + (x - 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x - 1 > 0 && y - 1 > 0)
                    {
                        c = p[(y - 1)*w + (x - 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (y - 1 > 0)
                    {
                        c = p[(y - 1)*w + x];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 1 < w && y - 1 > 0)
                    {
                        c = p[(y - 1)*w + (x + 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 2 < w && y - 1 > 0)
                    {
                        c = p[(y - 1)*w + (x + 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }

                    // Row 2
                    if (x - 2 > 0)
                    {
                        c = p[y*w + (x - 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x - 1 > 0)
                    {
                        c = p[y*w + (x - 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 1 < w)
                    {
                        c = p[y*w + (x + 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 2 < w)
                    {
                        c = p[y*w + (x + 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }

                    // Row 3
                    if (x - 2 > 0 && y + 1 < h)
                    {
                        c = p[(y + 1)*w + (x - 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x - 1 > 0 && y + 1 < h)
                    {
                        c = p[(y + 1)*w + (x - 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (y + 1 < h)
                    {
                        c = p[(y + 1)*w + x];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 1 < w && y + 1 < h)
                    {
                        c = p[(y + 1)*w + (x + 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 2 < w && y + 1 < h)
                    {
                        c = p[(y + 1)*w + (x + 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }

                    // Row 4
                    if (x - 2 > 0 && y + 2 < h)
                    {
                        c = p[(y + 2)*w + (x - 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x - 1 > 0 && y + 2 < h)
                    {
                        c = p[(y + 2)*w + (x - 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (y + 2 < h)
                    {
                        c = p[(y + 2)*w + x];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 1 < w && y + 2 < h)
                    {
                        c = p[(y + 2)*w + (x + 1)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }
                    if (x + 2 < w && y + 2 < h)
                    {
                        c = p[(y + 2)*w + (x + 2)];
                        if (c == empty)
                        {
                            continue;
                        }
                    }

                    // If all neighboring pixels are processed 
                    // it's clear that the current pixel is not a boundary pixel.
                    rp[i] = cm;
                }
            }

            return result;
        }
    }
}