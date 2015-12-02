using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Zero
{
    abstract class BaseUnit
    {
        protected static Vector2 screen;
        public abstract float Bounding
        {
            get;
        }
        public abstract float Mass
        {
            get;
        }
        public Vector2 Position
        {
            get;
            protected set;
        }
        public Vector2 Velocity
        {
            get;
            set;
        }
        public Vector2 Acceleration
        {
            get;
            protected set;
        }
        public float Rotation
        {
            get;
            protected set;
        }
        public float Spin
        {
            get;
            set;
        }
        public float AngAccel
        {
            get;
            protected set;
        }

        public BaseUnit(Vector2 pos)
        {
            Position = pos;
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Rotation = 0;
            Spin = 0;
            AngAccel = 0;
        }

        public static void Init(Vector2 scr)
        {
            screen = scr;
        }
        public void Update(float elapsed)
        {
            Velocity += Acceleration * elapsed;
            Position += Velocity * elapsed;
            //Acceleration = Vector2.Zero;
            Acceleration = (screen / 2 - Position) / 200;
            Spin += AngAccel * elapsed;
            Rotation += Spin * elapsed;
            AngAccel = 0;
        }
        public abstract void Draw();
    }

    class Debris : BaseUnit
    {
        private float radius, mass, mI, spring, friction;
        public override float Bounding {
            get { return radius; }
        }
        public override float Mass
        {
            get { return mass; }
        }

        public Debris(Vector2 pos, float rad, float density)
            : base(pos)
        {
            radius = rad;
            mass = density * 4 / 3 * MathHelper.Pi * (float)Math.Pow(radius, 3);
            mI = mass * 2 / 5 * (float)Math.Pow(radius, 2);
            spring = 999999;
            friction = 2;
        }

        public void Collision(Debris other)
        {
            if (other == this)
                return;
            float compression = Bounding + other.Bounding - Vector2.Distance(Position, other.Position);
            if (compression <= 0)
                return;
            Vector2 fromOther = Vector2.Normalize(Position - other.Position);

            float force = compression * (spring * other.spring) / (spring + other.spring);
            Vector2 forceDif = fromOther * force;
            Acceleration += forceDif / Mass;
            other.Acceleration -= forceDif / other.Mass;

            float angle = Calc.Angle(Velocity, -fromOther);
            float spn = Spin * radius + Velocity.Length() * (float)Math.Sin(angle);
            float oangle = Calc.Angle(other.Velocity, fromOther);
            float ospn = other.Spin * other.radius + other.Velocity.Length() * (float)Math.Sin(oangle);
            float torqueDif = Calc.Sign(ospn - spn) * (friction + other.friction) * force;

            AngAccel += torqueDif / mI;
            other.AngAccel += torqueDif / other.mI;
        }
        public override void Draw()
        {
            lDraw.test(Position, radius, Rotation);
            lDraw.Circle(Position, radius, 30);
        }
    }

    class Jumper : BaseUnit
    {
        public override float Bounding
        {
            get { return 2; }
        }
        public override float Mass
        {
            get { return 70; }
        }

        public Jumper(Vector2 pos)
            : base(pos)
        {}

        public override void Draw()
        {
            lDraw.Circle(Position, 5, 10);
            lDraw.Circle(Position, 2, 10);
        }
    }

    class Conglomerate : BaseUnit
    {
        float mass, bounding;
        private ArrayList components;
        public override float Bounding
        {
            get { return bounding; }
        }
        public override float Mass
        {
            get { return mass; }
        }

        public Conglomerate()
            : base(Vector2.Zero)
        {
            components = new ArrayList();
        }

        public void Add(BaseUnit other)
        {
            // TODO: Add the piece to the components, then shift the mass, position, etc.
        }
        public override void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
