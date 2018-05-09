using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
 
namespace AILab1.Models
{
    class MamdaniSystem
    {

        Fuzzy stuff_bad(float staff)
        {
            //return Fuzzy.Gaussian(staff, 1.5f, 0f);
            return Fuzzy.Triangle(staff, -0.01f, 0f, 5f);
        }
        Fuzzy stuff_good(float staff)
        {
            //return Fuzzy.Gaussian(staff, 1.5f, 5f);
            return Fuzzy.Triangle(staff, 1f, 5f, 9f);
        }
        Fuzzy stuff_awesome(float staff)
        {
            //return Fuzzy.Gaussian(staff, 1.5f, 10f);
            return Fuzzy.Triangle(staff, 5f, 10f, 10.1f);
        }

        Fuzzy food_bad(float food)
        {
            return Fuzzy.Trapezoid(food, -2f, 0f, 1f, 3f);
        }
 
        Fuzzy food_good(float food)
        {
            return Fuzzy.Trapezoid(food, 7f, 9f, 10f, 12f);
        }
 
        public float ProcessRules(float staff, float food)
        {
            float[] inputs = new float[5];
            //float[] rules = new float[5];
            float[] outputs = new float[3];
 
            inputs[0] = stuff_bad(staff).X;
            inputs[1] = stuff_good(staff).X;
            inputs[2] = stuff_awesome(staff).X;
            inputs[3] = food_bad(food).X;
            inputs[4] = food_good(food).X;
 
 
            //if staff is bad then tip is small
            Debug.WriteLine(String.Format("Debug: {0} {1}", staff, food));
            //rules[0] = triangleSurface(stuff_bad(staff).X, 0f, 5f, 10f);
            //rules[1] = triangleSurface(stuff_good(staff).X, 10f, 15f, 20f);
            //rules[2] = triangleSurface(stuff_awesome(staff).X, 20f, 25f, 30f);
            //rules[3] = triangleSurface(food_bad(food).X, 0f, 5f, 10f);
            //rules[4] = triangleSurface(food_good(food).X, 20f, 25f, 30f);
            outputs[0] = Math.Max(inputs[0], inputs[3]);
            outputs[1] = inputs[1];
            outputs[2] = Math.Max(inputs[2], inputs[4]);
 
            float uppersum = 0.0001f;
            float lowersum = 0.0001f;

            double step = 0.001;


            for (double i = 0; i <= 30; i += 0.005)
            {
                float j = (float)i;
                if (i >= 0 && i < 10)
                {
                    uppersum = calculateValue(outputs[0], j, -0.01f, 0f, 0.01f) * j + uppersum;
                    lowersum = calculateValue(outputs[0], j, -0.01f, 0f, 0.01f) + lowersum;
                }
                else if (i >= 10 && i < 20)
                {
                    uppersum = calculateValue(outputs[1], j, 14.9f, 15f, 15.1f) * j + uppersum;
                    lowersum = calculateValue(outputs[1], j, 14.9f, 15f, 15.1f) + lowersum;
                }
                else
                {
                    uppersum = calculateValue(outputs[2], j, 29.99f, 30f, 30.01f) * j + uppersum;
                    lowersum = calculateValue(outputs[2], j, 29.99f, 30f, 30.01f) + lowersum;
                }
            }


            /*for (double i = 0; i < 30; i += step)
            {
                float j = (float)i;
                if (i >= 0 && i < 10)
                {
                    uppersum = (float) step * (calculateValue(outputs[0], (j + (float)step/2), -0.01f, 0f, 0.01f) * (j + (float)step / 2)) + uppersum;
                    lowersum = (float) step * (calculateValue(outputs[0], (j + (float)step / 2), -0.01f, 0f, 0.01f)) + lowersum;
                }
                else if (i >= 10 && i < 20)
                {
                    uppersum = (float)step * (calculateValue(outputs[1], (j + (float)step / 2), 0f, 15f, 30f) * (j + (float)step / 2)) + uppersum;
                    lowersum = (float)step * (calculateValue(outputs[1], (j + (float)step / 2), 0f, 15f, 30f)) + lowersum;
                }
                else
                {
                    uppersum = (float)step * (calculateValue(outputs[2], (j + (float)step / 2), 29.99f, 30f, 30.01f) * (j + (float)step / 2)) + uppersum;
                    lowersum = (float)step * (calculateValue(outputs[2], (j + (float)step / 2), 29.99f, 30f, 30.01f)) + lowersum;
                }
            }*/

            float final_output = uppersum / lowersum;
 
            Debug.WriteLine(String.Format("{0} {1} {2} {3} {4}", inputs[0], inputs[1], inputs[2], inputs[3], inputs[4]));
            Debug.WriteLine(String.Format("{0} {1} {2}", outputs[0], outputs[1], outputs[2]));
            //Debug.WriteLine(String.Format("{0} {1} {2} {3} {4}", rules[0], rules[1], rules[2], rules[3], rules[4]));

            //return Math.Max(rules[0], rules[3]) + rules[1] + Math.Max(rules[2], rules[4]);
            //return (rules[0]- rules[3]) + rules[1] + (rules[2] - rules[4]);
            return final_output;
        }
 
        private float calculateValue(float max, float x, float a, float b, float c)
        {
            if (Fuzzy.Triangle(x, a, b, c).X > max)
            {
                return max;
            }
            return Fuzzy.Triangle(x, a, b, c).X;
        }
 
        //private float triangleSurface(float input, float a, float b, float c)
        //{
        //    if(input == 1.0f)
        //    {
        //        return 0.5f * (c - a);
        //    }
        //    var d = traingleBisect(input, a, b, c, a, b);
        //    var e = traingleBisect(input, a, b, c, b, c);
        //    Debug.WriteLine(String.Format("PARAMSY: {0} {1} {2}", a, b, c));
        //    Debug.WriteLine(String.Format("IN: {0} {1} {2}", input, d, e));
        //    return 0.5f * ((c - a) + (e - d)) * input;
        //}
 
        //private float traingleBisect(float input, float a, float b, float c, float st, float en)
        //{
        //    var intervalBegin = st;
        //    var intervalEnd = en;
        //    float middle = 0.0f;
        //    if ((Fuzzy.Trianglef(intervalBegin, a, b, c)-input) * (Fuzzy.Trianglef(intervalEnd, a, b, c)-input) > 0.0f)
        //    {
        //        Console.Write("Function has same signs at ends of interval");
        //        return -99999999999f;
        //    }
        //    while (Math.Abs(intervalBegin - intervalEnd) > 0.01f)
        //    {
        //        middle = (intervalBegin + intervalEnd) / 2.0f;
        //        if ((Fuzzy.Trianglef(intervalBegin, a, b, c) - input) * (Fuzzy.Trianglef(middle, a, b, c) - input) < 0.0f)
        //        {
        //            intervalEnd = middle;
        //        }
        //        else
        //        {
        //            intervalBegin = middle;
        //        }
        //    }
        //    return middle;
        //}
    }
}