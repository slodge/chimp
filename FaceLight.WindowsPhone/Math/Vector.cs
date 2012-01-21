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

using System;
using System.Windows;

namespace FaceLight.Mobile.Math
{
    /// <summary>
    /// Integer vector.
    /// </summary>
    public struct Vector
    {
        public int X;
        public int Y;

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point point)
            : this((int) point.X, (int) point.Y)
        {
        }


        public static Vector Zero
        {
            get { return new Vector(0, 0); }
        }

        public static Vector One
        {
            get { return new Vector(1, 1); }
        }

        public static Vector UnitX
        {
            get { return new Vector(1, 0); }
        }

        public static Vector UnitY
        {
            get { return new Vector(0, 1); }
        }

        public int Length
        {
            get { return (int) System.Math.Sqrt(X*X + Y*Y); }
        }

        public int LengthSq
        {
            get { return X*X + Y*Y; }
        }


        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator *(Vector p, int s)
        {
            return new Vector(p.X*s, p.Y*s);
        }

        public static Vector operator *(int s, Vector p)
        {
            return new Vector(p.X*s, p.Y*s);
        }

        public static Vector operator *(Vector p, float s)
        {
            return new Vector((int) (p.X*s), (int) (p.Y*s));
        }

        public static Vector operator *(float s, Vector p)
        {
            return new Vector((int) (p.X*s), (int) (p.Y*s));
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return v1.X != v2.X || v1.Y != v2.Y;
        }


        public Vector Interpolate(Vector v2, float amount)
        {
            return new Vector((int) (X + ((v2.X - X)*amount)), (int) (Y + ((v2.Y - Y)*amount)));
        }

        public int Dot(Vector v2)
        {
            return X*v2.X + Y*v2.Y;
        }

        public int AngleDeg(Vector v2)
        {
            // Normalize this
            int len = Length;
            double x1 = 0, y1 = 0, x2 = 0, y2 = 0;
            if (len != 0)
            {
                double s1 = 1.0f/len;
                x1 = X*s1;
                y1 = Y*s1;
            }

            // Normalize v2
            len = v2.Length;
            if (len != 0)
            {
                double s2 = 1.0f/len;
                x2 = v2.X*s2;
                y2 = v2.Y*s2;
            }

            // Acos of the dot product would only return degrees between 0° and 180° without a sign
            double rad = System.Math.Atan2(y2, x2) - System.Math.Atan2(y1, x1);

            // return the angle in degrees
            return (int) (MathHelper.ToDegrees(rad));
        }


        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                return ((Vector) obj) == this;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("({0}, {1})", X, Y);
            ;
        }
    }
}