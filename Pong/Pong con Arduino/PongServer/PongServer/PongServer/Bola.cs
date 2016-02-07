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
        Texture2D textura,backgroundA,textura2;
        public Rectangle rect;
        Random randito;
        public float effectO,effect1;
   
        public Bola(ContentManager Content)
        {
            randito = new Random();
            nuevaBola();
            textura = Content.Load<Texture2D>("Sprites\\bola");
            textura2 = Content.Load<Texture2D>("Sprites\\bola2");
            centro = new Vector2(textura.Width/ 2, textura.Height / 2);
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
            backgroundA = Content.Load<Texture2D>("Sprites\\bg2Right");
            effectO = 0.0f;
            effect1 = 0.0f;
        }

        public void nuevaBola() {
            posicion = new Vector2(0, 500);
            direccion = new Vector2((float)(randito.NextDouble() * ((((randito.Next(-2, 0) + 1)) * 8) + 4)), (float)(randito.NextDouble() * 3 * ((((randito.Next(-2, 0) + 1)) * 2) + 1)));
        }

        public void updatear(ref int puntajeS,ref int puntajeC) {
            if (Math.Abs(direccion.X) < 20) {
                direccion.X *= 1.05f;
            }
            posicion += direccion;
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y;
            if (posicion.X < -1360) {
                puntajeS++;
                nuevaBola();
            }

            if (posicion.X > 1360)
            {
                puntajeC++;
                nuevaBola();
                effectO = 1.0f;
            }
            if (posicion.Y > 1004 | posicion.Y <20)
            {
                direccion.Y *= -1;
            }
            
            if (effectO > 0.0f) {
                effectO -= 0.05f;
            }
            if (effect1 > 0.0f)
            {
                effect1 -= 0.05f;
            }
        }

        public void dibujar(SpriteBatch spriteBatch) {
            spriteBatch.Draw(backgroundA, new Vector2(0, 0), Color.White * effectO);
            spriteBatch.Draw(textura, posicion, null, Color.White,0.0f, centro,1.0f, SpriteEffects.None, 1);
            spriteBatch.Draw(textura2, posicion, null, Color.White * effect1, 0.0f, centro, 1.0f, SpriteEffects.None, 1);
        }


        
    }
}
