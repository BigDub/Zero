using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zero
{
    class Calc
    {
        public static float Sign(float f)
        {
            if (f == 0)
                return 1;
            return Math.Sign(f);
        }
        public static float Angle(Vector2 p1, Vector2 p2)
        {
            if (p1.X == p2.X)
                if (p1.Y <= p2.Y)
                    return (float)Math.PI / 2;
                else
                    return (float)Math.PI * 1.5f;
            float res = (float)Math.Atan((p2.Y - p1.Y)/(p2.X - p1.X));
            if (p1.X > p2.X)
                res += (float)Math.PI;
            return WrapAngle(res);
        }
        public static float Direction(Vector2 v)
        {
            return Angle(Vector2.Zero, v);
        }
        public static float WrapAngle(float angle)
        {
            while (angle > MathHelper.TwoPi)
            {
                angle -= MathHelper.TwoPi;
            }
            while (angle < 0)
            {
                angle += MathHelper.TwoPi;
            }
            return angle;
        }
    }
}
