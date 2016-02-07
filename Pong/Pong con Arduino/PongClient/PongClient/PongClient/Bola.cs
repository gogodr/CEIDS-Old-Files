using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PongClient
{
    class Bola
    {
        public Vector2 posicion, direccion,centro;
        Texture2D textura,backgroundA;
        public Vector2 antes;
        public Rectangle rect;
        float effectO;
        public Bola(ContentManager Content)
        {
            posicion = new Vector2(0, 500);
            direccion = new Vector2(-2, 1);
            textura = Content.Load<Texture2D>("Sprites\\cursor");
            centro = new Vector2(textura.Width / 2, textura.Height / 2);
            antes = new Vector2();
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
            
            backgroundA = Content.Load<Texture2D>("Sprites\\bg2Left");
            effectO = 0.0f;
             
        }
        public void updatear(Vector2 pos)
        {
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y;
            if (pos == posicion)
            {
                posicion = posicion + (posicion - antes);
            }
            else
            {
                posicion = new Vector2(pos.X + 1280, pos.Y);
            }
            
            if (posicion.X < 0) {
                effectO = 1.0f;
            }

            if (effectO > 0.0f)
            {
                effectO -= 0.05f;
            }
             
            
            antes = posicion;
        }

        public void dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundA, new Vector2(0, 0), Color.White * effectO);
            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.None, 1);
        }



    }
}
