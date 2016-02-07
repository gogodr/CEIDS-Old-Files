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
    class Cursor
    {

        Vector2 posicion, center;
        Texture2D textura;
        float alpha;
        public bool vivo;


        public Cursor(ContentManager Content)
        {
            textura = Content.Load<Texture2D>("Sprites\\bola");
            posicion = new Vector2(0, 0);
            center = new Vector2(textura.Bounds.Width / 2, textura.Bounds.Height / 2);
            alpha = 255;
            vivo = false;


        }

        public void dibujarCursor(SpriteBatch spriteBatch, Vector2 pos)
        {
            if (vivo == false)
            {
                posicion = pos;
                vivo = true;
            }
            else
            {

            }

             //spriteBatch.Draw(textura, new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height), Color.Lerp(Color.White, Color.Transparent, alpha));
            spriteBatch.Draw(textura, posicion, null, Color.White * (1 - alpha), 0.0f, center, (1 - alpha), SpriteEffects.None, 1);

            alpha += 0.07f;

            if (alpha >= 1)
            {
                alpha = 0;
                vivo = false;
            }
        }

    }
}
