using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using C3.XNA;


namespace Innlevering_2
{
    class Line
    {
        public Vector2 pointA { get; private set; }
        public Vector2 pointB { get; private set; }
        public Color color { get; private set; }
        public float thickness { get; private set; }

        public Line(Vector2 pointA, Vector2 pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.color = Color.Black;
            this.thickness = 2f;
        }

        public void Draw (SpriteBatch spriteBatch)
        {
            Primitives2D.DrawLine(spriteBatch, pointA, pointB, color, thickness);
            
        }
    }
}
