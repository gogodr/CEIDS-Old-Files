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
    class Bola
    {
        public Vector2 posicion, direccion,centro;
        Texture2D textura;
        public Rectangle rect;
        Random randito;

        public Bola(ContentManager Content)
        {
            randito = new Random();
            posicion = new Vector2(0, 500);
            direccion = new Vector2(((float)Math.Pow(randito.Next(1, 2), 2) - 2.5f) *4, randito.Next(3, 5));
            textura = Content.Load<Texture2D>("Sprites\\cursor");
            centro = new Vector2(textura.Width/ 2, textura.Height / 2);
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
        }
        public void updatear() {
            posicion += direccion;
            
            if (posicion.X < -1360|posicion.X > 1360) {
                posicion = new Vector2(0, 500);                                                                                                                                                                                     
                direccion = new Vector2(((float)Math.Pow(randito.Next(1, 2),2)-2.5f)*4,randito.Next(3, 5));
            }
            if (posicion.Y > 1004 | posicion.Y <20)
            {
                direccion.Y *= -1;
            }
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y;
        }

        public void dibujar(SpriteBatch spriteBatch) {
            
            spriteBatch.Draw(textura, posicion, null, Color.White,0.0f, centro,1.0f, SpriteEffects.None, 1);

        }


        
    }
}
