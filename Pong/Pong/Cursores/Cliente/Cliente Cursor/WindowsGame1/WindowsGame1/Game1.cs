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

namespace WindowsGame1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        Cliente cliente;
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Cursor cursor,cursor2;
        Thread mandar;
        String paquete;
        Vector2 pos;
            public void mandacion()
            {
                cliente = new Cliente();
                cliente.conectar("192.168.52.56", 8888);
                for (; ; )
                {
                    
                   // cliente.enviar( "Y" + "" + "\0");

                    cliente.recibir(ref paquete);
                
                    if (paquete.Split('Y').Length == 2)
                    {
                        try
                        {
                            pos = new Vector2(float.Parse(paquete.Split('Y')[0]), float.Parse(paquete.Split('Y')[1]));
                        }
                        catch {

                        }
                    }
            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
                       
            cursor = new Cursor(Content);
            cursor2 = new Cursor(Content);
            
           
            mandar = new Thread(new ThreadStart(mandacion));
            mandar.Start();
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                cliente.close();
                this.Exit();
            }
            // TODO: Add your update logic here
           
           
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
           
            cursor.dibujarCursor(spriteBatch, new Vector2(Mouse.GetState().X,Mouse.GetState().Y),Color.Red);
            cursor2.dibujarCursor(spriteBatch, pos, Color.White);    
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
