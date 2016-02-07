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

namespace PongServer
{
    class Paleta
    {
        public Vector2 posicion,centro;
        Texture2D textura;
        Rectangle rect;
        int colisiono;
        

        public Paleta(ContentManager Content)
        {
            colisiono = 0;
            posicion = new Vector2(0, 500);
            textura = Content.Load<Texture2D>("Sprites\\bumper");
            centro = new Vector2(textura.Width/ 2, textura.Height / 2);
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
        }

        public void update(Vector2 pos,Bola bola) {
            posicion = pos;
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y;
            if (colisiono > 0)
            {
                colisiono -= 1;
            }
            else
            {

                if (rect.Intersects(bola.rect))
                {
                    bola.direccion.X *= -1.3f;
                    colisiono = 10;

                }
            }


        }

        public void dibujar(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.None, 1);

        }
        
    }
}
