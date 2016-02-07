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
        SoundEffect soundEffectHit, soundEffectLose, soundEffectStart;
        int murio;
            
        

        public Paleta(ContentManager Content)
        {
            soundEffectHit = Content.Load<SoundEffect>("Sounds\\hit");
            soundEffectLose = Content.Load<SoundEffect>("Sounds\\perder");
            soundEffectStart = Content.Load<SoundEffect>("Sounds\\start");
            colisiono = 0;
            posicion = new Vector2(0, 500);
            textura = Content.Load<Texture2D>("Sprites\\bumper");
            centro = new Vector2(textura.Width/ 2, textura.Height / 2);
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
            murio = 0;
        }

        public void update(Vector2 pos,Bola bola,bool mine) {
            posicion = pos;
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y-(rect.Height/2);
            if (colisiono > 0)
            {
                colisiono -= 1;
            }
            else
            {

                if (rect.Intersects(bola.rect))
                {
                    

                    bola.direccion.Y = (posicion.Y - bola.posicion.Y)/18;



                    if (Math.Abs(bola.direccion.X) < 40.0f)
                    {
                        bola.direccion.X *= -1.5f;
                    }
                    else {
                        bola.direccion.X *= -1.0f;
                    }
                    colisiono = 10;
                    if (mine)
                    {
                        bola.effect1 = 1.0f;
                        soundEffectHit.Play();
                    }
                }

            }
            if (bola.posicion.X > 1280) {
                if (murio == 0)
                {
                    soundEffectLose.Play(0.5f, 1.0f, 0.0f);
                    murio = 50;
                }
            }
            if (murio > 0) {
                murio--;
            }

        }

        public void dibujar(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.None, 1);

        }
        
    }
}
