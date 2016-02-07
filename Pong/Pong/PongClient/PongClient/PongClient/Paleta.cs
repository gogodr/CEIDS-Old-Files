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

        public Paleta(ContentManager Content)
        {
            posicion = new Vector2();
            textura = Content.Load<Texture2D>("Sprites\\bumper");
            centro = new Vector2(textura.Width / 2, textura.Height / 2);
        }
        public void update()
        {
            posicion= new Vector2(100, Mouse.GetState().Y);
        }
        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, posicion, null, Color.White, 0.0f, centro, 1.0f, SpriteEffects.FlipHorizontally, 1);
        }
    }

}
