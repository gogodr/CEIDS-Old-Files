using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ServerClientCEIDS;
using System.Threading;

namespace PongServer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Thread conversacion;
        Server server;
        Bola bola;
        Paleta j1, j2;
              
        
        public void envRec()
        {
            
            for (; ;)
            {
                try
                {
                    server.leer();
                    if (server.paquete.Split('Y').Length == 2)
                    {
                        j2.posicion = new Vector2(-1180, float.Parse(server.paquete.Split('Y')[1]));
                    }
                }
                catch { }
                finally
                {
                    server.enviar(bola.posicion.X.ToString() + "Y" + bola.posicion.Y.ToString() + "\0");
                }
                
            }
        }

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = true;
                       
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            j1 = new Paleta(Content);
            j2 = new Paleta(Content);
            bola = new Bola(Content);
            
            server = new Server("192.168.52.27", 8888);
            server.comenzarServidor();

            conversacion = new Thread(new ThreadStart(envRec));
            conversacion.Start();
            
            

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            bola.updatear();
            j1.update(new Vector2(1160, Mouse.GetState().Y), bola);
            j2.update(j2.posicion, bola);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here


            base.Draw(gameTime);
            spriteBatch.Begin();
            bola.dibujar(spriteBatch);
            j1.dibujar(spriteBatch);
            spriteBatch.End();
        }
    }
}
