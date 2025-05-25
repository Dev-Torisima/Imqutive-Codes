//Math.cs
//Created by Torisima 2025
//License : https://github.com/Dev-Torisima/Imqutive-Codes/blob/main/LICENSE

namespace Imqutive
{
    /// <summary>Matrix</summary>
    public class Matrix
    {
        private double[][] _v;

        private int _i;
        private int _j;

        public Matrix(int i, int j)
        {
            _i = i;
            _j = j;

            _v = new double[i][];
            for (int q = 0; q < i; q++)
            {
                _v[q] = new double[j];
            }
        }

        public double this[int i, int j]
        {
            get
            {
                if (i >= 0 & i < _i & j >= 0 & j < _j)
                {
                    return _v[i][j];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            set
            {
                if (i >= 0 & i < _i & j >= 0 & j < _j)
                {
                    _v[i][j] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        public static Matrix Empty { get; } = new Matrix(0, 0);

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            if (m1._j == m2._i)
            {
                Matrix m = new Matrix(m1._i, m2._j);

                for (int i = 0; i < m1._i; i++)
                {
                    for (int j = 0; j < m2._j; j++)
                    {
                        m[i, j] = 0;
                        for (int q = 0; q < m1._j; q++)
                        {
                            m[i, j] += m1[i, q] * m2[q, j];
                        }
                    }
                }
                return m;
            }
            else
            {
                return Matrix.Empty;
            }
        }

        public static Matrix operator /(Matrix m, double d)
        {
            for (int i = 0; i < m._i; i++)
            {
                for (int j = 0; j < m._j; j++)
                {
                    m._v[i][j] /= d;
                }
            }
            return m;
        }

        public void Except(int i, int j)
        {
            _e_i(i);
            _e_j(j);
        }

        private void _e_i(int i)
        {
            if (_i - 1 - i == 0)
            {
            }
            else
            {
                for (int e = i; e < _i - 1 - i; e++)
                {
                    for (int f = 0; f < _j; f++)
                    {
                        _v[e][f] = _v[e + 1][f];
                    }
                }
            }


            _i -= 1;
            Array.Resize(ref _v, _i);
        }

        private void _e_j(int j)
        {
            if (_j - 1 - j == 0)
            {
            }
            else
            {
                for (int e = 0; e < _i; e++)
                {
                    for (int f = j; f < _j - 1 - j; f++)
                    {
                        _v[e][f] = _v[e][f + 1];
                    }
                }
            }


            _j -= 1;
            for (int a = 0; a < _i; a++)
            {
                Array.Resize(ref _v[a], _j);
            }
        }

        public void SetLine(double[] v, int i)
        {
            Array.Resize(ref v, _j);

            _v[i] = v;
        }

        public void SetRow(double[] v, int j)
        {
            Array.Resize(ref v, _i);

            for (int i = 0; i < _i; i++)
            {
                _v[i][j] = v[i];
            }
        }

        public double Det()
        {
            if (_i == _j)
            {

                double d = 0;
                double d1 = 1;
                double d2 = 1;
                for (int i = 0; i < _i; i++)
                {
                    d1 = 1;
                    d2 = 1;
                    for (int j = i; j < i + _i; j++)
                    {
                        for (int q = 0; q < _i; q++)
                        {
                            d1 *= _v[j % _i][q];
                        }

                        for (int q = _i - 1; q >= 0; q--)
                        {
                            d2 *= _v[j % _i][q];
                        }
                    }
                    d += d1 - d2;
                }

                return d;
            }
            else return Double.NaN;
        }

        public void Reverse()
        {
            Matrix _m1 = this;
            Matrix _m2 = this;
            double[][] _m = this._v;
            for (int i = 0; i < _i; i++)
            {
                _m1 = this;
                _m1._e_i(i);
                for (int j = 0; j < _j; j++)
                {
                    _m2 = _m1;
                    _m2._e_j(j);
                    _m[i][j] = _m2.Det();
                }
            }

            this._v = _m;
        }

        public static Matrix operator /(Matrix m1, Matrix m2)
        {
            m2.Reverse();
            return m1 * m2;
        }

        public Matrix Copy()
        {
            Matrix m = new Matrix(this._i, this._j);

            for (int i = 0; i < _i; i++)
            {
                for (int j = 0; j < _j; j++)
                {
                    m._v[i][j] = this._v[i][j];
                }
            }

            return m;
        }
    }

    /// <summary>Number Function</summary>
    public sealed class Number
    {
        /// <summary>Find the least common multiple</summary>
        public static sbyte Lcm(sbyte a, sbyte b)
        {
            return (sbyte)(a * b / Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static byte Lcm(byte a, byte b)
        {
            return (byte)((short)a * (short)b / (short)Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static ushort Lcm(ushort a, ushort b)
        {
            return (ushort)(a * b / Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static uint Lcm(uint a, uint b)
        {
            return a * b / Gcd(a, b);
        }
        /// <summary>Find the least common multiple</summary>
        public static ulong Lcm(ulong a, ulong b)
        {
            return a * b / Gcd(a, b);
        }
        /// <summary>Find the least common multiple</summary>
        public static short Lcm(short a, short b)
        {
            return (short)(a * b / Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static Half Lcm(Half a, Half b)
        {
            return (Half)((float)a * (float)b / (float)Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static int Lcm(int a, int b)
        {
            return a * b / Gcd(a, b);
        }
        /// <summary>Find the least common multiple</summary>
        public static float Lcm(float a, float b)
        {
            return (float)(a * b / Gcd(a, b));
        }
        /// <summary>Find the least common multiple</summary>
        public static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }
        /// <summary>Find the least common multiple</summary>
        public static double Lcm(double a, double b)
        {
            return a * b / Gcd(a, b);
        }
        /// <summary>Find the least common multiple</summary>
        public static decimal Lcm(decimal a, decimal b)
        {
            return a * b / Gcd(a, b);
        }



        /// <summary>Find the greatest common factor</summary>
        public static sbyte Gcd(sbyte a, sbyte b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = (sbyte)(a - (a / b) * b);
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static byte Gcd(byte a, byte b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = (byte)(a - (a / b) * b);
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static short Gcd(short a, short b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = (short)(a - (a / b) * b);
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static ushort Gcd(ushort a, ushort b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = (ushort)(a - (a / b) * b);
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static int Gcd(int a, int b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = a % b;
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static long Gcd(long a, long b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = a % b;
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static uint Gcd(uint a, uint b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = a % b;
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static ulong Gcd(ulong a, ulong b)
        {
            if (a < b) return Gcd(b, a);

            while (b != 0)
            {
                var r = a % b;
                a = b;
                b = r;
            }
            return a;
        }
        /// <summary>Find the greatest common factor</summary>
        public static Half Gcd(Half a, Half b)
        {
            short ia = TenInt(a);
            short jb = TenInt(b);
            if (ia / (float)a >= jb / (float)b)
            {
                for (int i = 0; i < ia / (float)a; i++)
                {
                    b = (Half)((float)b * 10);
                }

                return (Half)((float)Gcd((Half)ia, b) / Math.Pow(10, (ia / (float)a)));
            }
            else
            {
                for (int i = 0; i < jb / (float)b; i++)
                {
                    a = (Half)((float)a * 10);
                }

                return (Half)((float)Gcd(a, (Half)jb) / Math.Pow(10, (jb / (float)b)));
            }
        }
        /// <summary>Find the greatest common factor</summary>
        public static double Gcd(float a, float b)
        {
            int ia = TenInt(a);
            int jb = TenInt(b);
            if (ia / a >= jb / b)
            {
                for (int i = 0; i < ia / a; i++)
                {
                    b *= 10;
                }

                return (float)(Gcd(ia, b) / Math.Pow(10, (ia / a)));
            }
            else
            {
                for (int i = 0; i < jb / b; i++)
                {
                    a *= 10;
                }

                return (float)(Gcd(a, jb) / Math.Pow(10, (jb / b)));
            }
        }
        /// <summary>Find the greatest common factor</summary>
        public static double Gcd(double a, double b)
        {
            long ia = TenInt(a);
            long jb = TenInt(b);
            if (ia / a >= jb / b)
            {
                for (int i = 0; i < ia / a; i++)
                {
                    b *= 10;
                }

                return Gcd(ia, b) / Math.Pow(10, (ia / a));
            }
            else
            {
                for (int i = 0; i < jb / b; i++)
                {
                    a *= 10;
                }

                return Gcd(a, jb) / Math.Pow(10, (jb / b));
            }
        }
        /// <summary>Find the greatest common factor</summary>
        public static decimal Gcd(decimal a, decimal b)
        {
            long ia = TenInt((double)a);
            long jb = TenInt((double)b);
            if (ia / a >= jb / b)
            {
                for (int i = 0; i < ia / a; i++)
                {
                    b *= 10;
                }

                return (decimal)(Gcd(ia, b) / (decimal)Math.Pow(10, (double)(ia / a)));
            }
            else
            {
                for (int i = 0; i < jb / b; i++)
                {
                    a *= 10;
                }

                return (decimal)(Gcd(a, jb) / (decimal)Math.Pow(10, (double)(jb / b)));
            }
        }
  
  
        /// <summary>Multiply by a power of 10 to make an integer</summary>
        public static long TenInt(double v)
        {
            while ((double)(long)v != v)
            {
                v *= 10;
            }

            return (long)v;
        }
        /// <summary>Multiply by a power of 10 to make an integer</summary>
        public static int TenInt(float v)
        {
            while ((float)(int)v != v)
            {
                v *= 10;
            }

            return (int)v;
        }
        /// <summary>Multiply by a power of 10 to make an integer</summary>
        public static short TenInt(Half v)
        {
            while ((Half)(short)v != v)
            {
                v = (Half)((float)v * 10);
            }

            return (short)v;
        }


        /// <summary>Looking for a sine from an angle</summary>
        public static double Sin(double angle)
        {
            if (angle is 0 or 180) return 0;
            else if (angle is 90) return 1;
            else if (angle is 270) return -1;

            return Math.Sin(angle * Math.PI / 180);
        }
        /// <summary>Finding the cosine of an angle</summary>
        public static double Cos(double angle)
        {
            if (angle is 90 or 270) return 0;
            else if (angle is 0) return 1;
            else if (angle is 180) return -1;

            return Math.Cos(angle * Math.PI / 180);
        }
        /// <summary>Finding the tangent of an angle</summary>
        public static double Tan(double angle)
        {
            if (angle is 90 or 270) return Double.NaN;
            else if (angle is 0 or 180) return 0;

            return Math.Tan(angle * Math.PI / 180);
        }

        /// <summary>Finding an angle from a sine</summary>
        public static double Asin(double d)
        {
            return Math.Asin(d) * 180 / Math.PI;
        }
        /// <summary>Finding an angle from a cosine</summary>
        public static double Acos(double d)
        {
            return Math.Acos(d) * 180 / Math.PI;
        }
        /// <summary>Finding an angle from a tangent</summary>
        public static double Atan(double d)
        {
            return Math.Atan(d) * 180 / Math.PI;
        }
    }

    /// <summary>Formula</summary>
    public sealed class Formula
    {
        /// <summary>Calculate Simultaneous Formula</summary>
        public static double[] Simultaneous(params double[][] fact)
        {
            int hl = fact[0].Length;
            for (int i = 1; i < fact.Length;)
            {
                if (hl != fact[i].Length) return new double[0];
            }

            hl -= 1;

            if (hl <= 0) return new double[0];

            double[] r = new double[hl];
            if (hl <= fact.Length)
            {
                try
                {
                    Matrix m = new Matrix(hl, hl);
                    double[] hh = new double[hl];
                    for (int i = 0; i < hl; i++)
                    {
                        m.SetLine(fact[i].Take(hl).ToArray(), i);
                        hh[i] = fact[i][hl];
                    }
                    //double _d0 = m.Det();
                    Matrix mc;
                    for (int i = 0; i < hl; i++)
                    {
                        mc = m.Copy();
                        mc.SetRow(hh, i);
                        r[i] = mc.Det();
                    }
                }
                catch (Exception)
                {
                    return new double[0];
                }
            }
            else return new double[0];

            return r;
        }
    }
}
