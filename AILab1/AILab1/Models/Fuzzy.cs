using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AILab1.Models
{
    class Fuzzy
    {
        float _x;
        public float X
        {
            get
            {
                return _x;
            }
            set
            {
                if (value < 0f)
                {
                    _x = 0f;
                    Debug.WriteLine("WARNING! Value less then 0.0f - trimmed!");
                }
                else if (value > 1f)
                {
                    _x = 1f;
                    Debug.WriteLine("WARNING! Value greater then 1.0f - trimmed!");
                }
                else
                {
                    _x = value;
                }
            }
        }

        public Fuzzy()
        {
            _x = 0.0f;
        }

        public Fuzzy(float x)
        {
            if (x < 0f)
            {
                _x = 0f;
                Debug.WriteLine("WARNING! Value less then 0.0f - trimmed!");
            }
            else if (x > 1f)
            {
                _x = 1f;
                Debug.WriteLine("WARNING! Value greater then 1.0f - trimmed!");
            }
            else
            {
                _x = x;
            }
        }

        public static Fuzzy Triangle(float x, float a, float b, float c)
        {
            float val = Trianglef(x, a, b, c);
            return new Fuzzy(val);
        }

        public static float Trianglef(float x, float a, float b, float c)
        {
            return Math.Max(Math.Min((x - a) / (b - a), (c - x) / (c - b)), 0f);
        }

        public static Fuzzy Trapezoid(float x, float a, float b, float c, float d)
        {
            float val = Math.Max(Math.Min((x - a) / (b - a), Math.Min(1, (d - x) / (d - c))), 0f);
            return new Fuzzy(val);
        }

        public static Fuzzy Gaussian(float x, float sigma, float mi)
        {
            float val = (float)Math.Exp(-Math.Pow((x - mi)/sigma, 2));
            return new Fuzzy(val);
        }

        public static Fuzzy operator&(Fuzzy x, Fuzzy y)
        {
            return And(x, y);
        }

        public static Fuzzy operator|(Fuzzy x, Fuzzy y)
        {
            return Or(x, y);
        }

        public static Fuzzy operator~(Fuzzy x)
        {
            return Not(x);
        }

        public static Fuzzy And(Fuzzy x, Fuzzy y)
        {
            return new Fuzzy(Andf(x.X, y.X));
        }

        public static Fuzzy Or(Fuzzy x, Fuzzy y)
        {
            return new Fuzzy(Orf(x.X, y.X));
        }

        public static Fuzzy Not(Fuzzy x)
        {
            return new Fuzzy(Notf(x.X));
        }

        private static float Andf(float x, float y)
        {
            return Math.Min(x, y);
        }

        private static float Orf(float x, float y)
        {
            return Math.Max(x, y);
        }

        private static float Notf(float x)
        {
            return 1 - x;
        }
    }
}
