﻿#region Header

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

namespace FaceLight.Mobile.Segmentation
{
    /// <summary>
    /// Four integer values decribing a segment which is actually a square.
    /// </summary>
    public struct Segment
    {
        public Vector Max;
        public Vector Min;

        public Segment(int x1, int y1, int x2, int y2)
            : this(new Vector(x1, y1), new Vector(x2, y2))
        {
        }

        public Segment(Vector min, Vector max)
        {
            Min = min;
            Max = max;
        }

        public int Width
        {
            get { return Max.X - Min.X; }
        }

        public int Height
        {
            get { return Max.Y - Min.Y; }
        }

        public Vector Center
        {
            get { return Min.Interpolate(Max, 0.5f); }
        }

        public int Diagonal
        {
            get { return (Max - Min).Length; }
        }

        public int DiagonalSq
        {
            get { return (Max - Min).LengthSq; }
        }
    }
}