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
        Texture2D textura;
        public Vector2 antes;

        public Bola(ContentManager Content)
        {
            posicion = new Vector2(0, 500);
            direccion = new Vector2(-2, 1);
            textura = Content.Load<Texture2D>("Sprites\\cursor");
            centro = new Vector2(textura.Width / 2, textura.Height / 2);
            antes = new Vector2(); 
                    }
        public void updatear(Vector2 pos)
        {
            if (pos == posicion)
            {
                posicion = posicion + (posicion - antes);
            }
            else
            {
                posicion = new Vector2(pos.X + 1280, pos.Y);
            }
            
            antes = posicion;
        }

        public void dibujar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.None, 1);
        }



    }
}
