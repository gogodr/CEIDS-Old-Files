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
    class Paleta
    {
        Texture2D textura;
        Vector2 posicion,centro;
        SoundEffect soundEffectHit, soundEffectLose, soundEffectStart;
        Rectangle rect;
        int colisiono;

        public Paleta(ContentManager Content)
        {
            posicion = new Vector2();
            textura = Content.Load<Texture2D>("Sprites\\bumper");
            centro = new Vector2(textura.Width / 2, textura.Height / 2);
            soundEffectHit = Content.Load<SoundEffect>("Sounds\\hit");
            soundEffectLose = Content.Load<SoundEffect>("Sounds\\perder");            
            soundEffectStart = Content.Load<SoundEffect>("Sounds\\start");
            centro = new Vector2(textura.Width / 2, textura.Height / 2);
            rect = new Rectangle(0, 0, textura.Width, textura.Height);
            colisiono = 0;
            
        }
        public void update(int posi, Bola bola)
        {
            rect.X = (int)posicion.X;
            rect.Y = (int)posicion.Y-(rect.Height/2);
            posicion= new Vector2(100,512 + (posi-50)*-10);
            
            if (bola.posicion.X<0){
                soundEffectLose.Play();
            }


            if (colisiono > 0)
            {
                colisiono -= 1;
            }
            if (rect.Intersects(bola.rect))
            {
                colisiono = 10;
                soundEffectHit.Play();
            }

        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.FlipHorizontally, 1);
        }
    }

}
