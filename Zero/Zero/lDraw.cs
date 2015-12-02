using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Zero
{
    class lDraw
    {
        private static GraphicsDevice gd;
        private static SpriteBatch spriteBatch;
        private static Texture2D tex;
        
        public static Color dColor { get; set; }

        public static void init(GraphicsDevice g, SpriteBatch s)
        {
            dColor = Color.Black;
            gd = g;
            spriteBatch = s;
            tex = new Texture2D(gd, 1, 1);
            tex.SetData<Color>(new Color[] { Color.White });
        }

        public static void Line(Vector2 p1, Vector2 p2)
        {
            float angle = Calc.Angle(p1, p2);
            float length = Vector2.Distance(p1, p2);
            spriteBatch.Draw(tex, p1, null, dColor, angle, Vector2.Zero, new Vector2(length, 1), SpriteEffects.None, 0);
        }

        public static void test(Vector2 center, float scale, float rot)
        {
            spriteBatch.Draw(tex, center, null, Color.White, rot, new Vector2(.5f), scale, SpriteEffects.None, 0);
        }

        public static void Circle(Vector2 center, float radius, float segments = 20)
        {
            if (segments <= 0)
                return;
            Vector2 lp = center;
            lp.X += radius;
            Vector2 cp = new Vector2();
            float angle = 0;
            for (int i = 1; i <= segments; i++)
            {
                angle = MathHelper.TwoPi / segments * i;
                cp = center;
                cp.X += (float)Math.Cos(angle) * radius;
                cp.Y += (float)Math.Sin(angle) * radius;
                Line(lp, cp);
                lp = cp;
            }
        }
    }
}
