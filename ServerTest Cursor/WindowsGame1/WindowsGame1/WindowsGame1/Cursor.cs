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



namespace WindowsGame1
{
    class Cursor
    {

        Vector2 posicion, center;
        Texture2D textura;
        float alpha;
        public bool vivo;
        

        public Cursor(ContentManager Content)
        {
            textura = Content.Load<Texture2D>("Sprites\\cursor");
            posicion = new Vector2(0, 0);
            center = new Vector2(textura.Bounds.Width / 2, textura.Bounds.Height / 2);
            alpha = 255;
            vivo = false;


        }

        public void dibujarCursor(SpriteBatch spriteBatch, Vector2 posicion,Color color)
        {
            spriteBatch.Draw(textura, new Rectangle((int)posicion.X, (int)posicion.Y, textura.Width, textura.Height), color);
            
        }

    }
}
