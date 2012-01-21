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

namespace FaceLight.Mobile.Visualization
{
    /// <summary>
    /// An interface for a visualizer that draws something into a SimpleBitmap.
    /// </summary>
    internal interface IVisualizer
    {
        void Visualize(ISimpleBitmap surface);
    }
}