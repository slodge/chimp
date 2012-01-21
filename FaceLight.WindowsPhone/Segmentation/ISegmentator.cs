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

using System.Collections.Generic;
using FaceLight.Mobile.SimpleBitmap;

namespace FaceLight.Mobile.Segmentation
{
    /// <summary>
    /// Interface of an image segmentator.
    /// </summary>
    internal interface ISegmentator
    {
        IEnumerable<Segment> Process(ISimpleBitmap input);
    }
}