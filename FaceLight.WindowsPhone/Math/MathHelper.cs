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
//   Copyright © 2010 Rene Schulte
//
//   This Software is weak copyleft open source. Please read the License.txt for details.
//

#endregion

namespace FaceLight.Mobile.Math
{
    /// <summary>
    /// Math helper methods.
    /// </summary>
    public static class MathHelper
    {
        #region Angle conversion

        /// <summary>
        /// Converts a radian into a degreee value.
        /// </summary>
        /// <param name="radians">An angle in rad.</param>
        /// <returns>The angle converted to degress.</returns>
        public static double ToDegrees(double radians)
        {
            return radians*57.295779513082320876798154814105;
        }

        /// <summary>
        /// Converts a degree into a radian value.
        /// </summary>
        /// <param name="degrees">A angle in deg.</param>
        /// <returns>The angle converted to radians.</returns>
        public static double ToRadians(double degrees)
        {
            return degrees*0.017453292519943295769236907684886;
        }

        #endregion
    }
}