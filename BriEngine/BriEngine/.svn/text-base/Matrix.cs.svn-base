using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Matrix
    {
        double m11, m12, m13;
        double m21, m22, m23;
        double m31, m32, m33;
        double m41, m42, m43;

        public static readonly Matrix Identity =
            new Matrix(new Vector(1, 0, 0),
                       new Vector(0, 1, 0),
                       new Vector(0, 0, 1),
                       new Vector(0, 0, 1));

        public Matrix() : this(Identity) { }

        public Matrix(Matrix m)
        {
            m11 = m.m11;
            m12 = m.m12;
            m13 = m.m13;

            m21 = m.m21;
            m22 = m.m22;
            m23 = m.m23;

            m31 = m.m31;
            m32 = m.m32;
            m33 = m.m33;

            m41 = m.m41;
            m42 = m.m42;
            m43 = m.m43;
        }

        public Matrix(Vector x, Vector y, Vector z, Vector o)
        {
            m11 = x.X; m12 = x.Y; m13 = x.Z;
            m21 = y.X; m22 = y.Y; m23 = y.Z;
            m31 = z.X; m32 = z.Y; m33 = z.Z;
            m41 = o.X; m42 = o.Y; m43 = o.Z;
        }

        public static Matrix operator *(Matrix mA, Matrix mB)
        {
            Matrix result = new Matrix();

            result.m11 = mA.m11 * mB.m11 + mA.m12 * mB.m21 + mA.m13 * mB.m31;
            result.m12 = mA.m11 * mB.m12 + mA.m12 * mB.m22 + mA.m13 * mB.m32;
            result.m13 = mA.m11 * mB.m13 + mA.m12 * mB.m23 + mA.m13 * mB.m33;
           
            result.m21 = mA.m21 * mB.m11 + mA.m22 * mB.m21 + mA.m23 * mB.m31;
            result.m22 = mA.m21 * mB.m12 + mA.m22 * mB.m22 + mA.m23 * mB.m32;
            result.m23 = mA.m21 * mB.m13 + mA.m22 * mB.m23 + mA.m23 * mB.m33;

            result.m31 = mA.m31 * mB.m11 + mA.m32 * mB.m21 + mA.m33 * mB.m31;
            result.m32 = mA.m31 * mB.m12 + mA.m32 * mB.m22 + mA.m33 * mB.m32;
            result.m33 = mA.m31 * mB.m13 + mA.m32 * mB.m23 + mA.m33 * mB.m33;

            result.m41 = mA.m41 * mB.m11 + mA.m42 * mB.m21 + mA.m43 * mB.m31;
            result.m42 = mA.m41 * mB.m12 + mA.m42 * mB.m22 + mA.m43 * mB.m32;
            result.m43 = mA.m41 * mB.m13 + mA.m42 * mB.m23 + mA.m43 * mB.m33;

            return result;
        }

        public static Vector operator *(Vector v, Matrix m)
        {
            return new Vector(v.X * m.m11 + v.Y * m.m21 + v.Z * m.m31 + m.m41,
                v.X * m.m12 + v.Y * m.m22 + v.Z * m.m32 + m.m42,
                v.X * m.m13 + v.Y * m.m23 + v.Z * m.m33 + m.m43);
        }

        public void SetTranslation(Vector translation)
        {
            m41 = translation.X;
            m42 = translation.Y;
            m43 = translation.Z;
        }

        public Vector GetTranslation()
        {
            return new Vector(m41, m42, m43);
        }

        public void SetScale(Vector scale)
        {
            m11 = scale.X;
            m22 = scale.Y;
            m33 = scale.Z;
        }

        public Vector GetScale()
        {
            Vector result = new Vector();
            result.X = (new Vector(m11, m12, m13)).Length();
            result.Y = (new Vector(m21, m22, m23)).Length();
            result.Z = (new Vector(m31, m32, m33)).Length();
            return result;
        }

        public void SetRotate(Vector axis, double angle)
        {
            double angleSin = Math.Sin(angle);
            double angleCos = Math.Cos(angle);
            double a = 1.0 - angleCos;
            double ax = a * axis.X;
            double ay = a * axis.Y;
            double az = a * axis.Z;

            m11 = ax * axis.X + angleCos;
            m12 = ax * axis.Y + axis.Z * angleSin;
            m13 = ax * axis.Z - axis.Y * angleSin;

            m21 = ay * axis.X - axis.Z * angleSin;
            m22 = ay * axis.Y + angleCos;
            m23 = ay * axis.Z + axis.X * angleSin;

            m31 = az * axis.X + axis.Y * angleSin;
            m32 = az * axis.Y - axis.X * angleSin;
            m33 = az * axis.Z + angleCos;
        }

        public double Determinate()
        {
            return m11 * (m22 * m33 - m23 * m32) +
                   m12 * (m23 * m31 - m21 * m33) +
                   m13 * (m21 * m32 - m22 * m31);
        }

        public Matrix Inverse()
        {
            double determinate = Determinate();

            double oneOverDet = 1.0 / determinate;

            Matrix result = new Matrix();
            result.m11 = (m22 * m33 - m23 * m32) * oneOverDet;
            result.m12 = (m13 * m32 - m12 * m33) * oneOverDet;
            result.m13 = (m12 * m23 - m13 * m22) * oneOverDet;

            result.m21 = (m23 * m31 - m21 * m33) * oneOverDet;
            result.m22 = (m11 * m33 - m13 * m31) * oneOverDet;
            result.m23 = (m13 * m21 - m11 * m23) * oneOverDet;

            result.m31 = (m21 * m32 - m22 * m31) * oneOverDet;
            result.m32 = (m12 * m31 - m11 * m32) * oneOverDet;
            result.m33 = (m11 * m22 - m12 * m21) * oneOverDet;

            result.m41 = -(m41 * result.m11 + m42 * result.m21 + m43 *
                result.m31);
            result.m42 = -(m41 * result.m12 + m42 * result.m22 + m43 *
                result.m32);
            result.m43 = -(m41 * result.m13 + m42 * result.m23 + m43 *
                result.m33);

            return result;
        }
    }
}
