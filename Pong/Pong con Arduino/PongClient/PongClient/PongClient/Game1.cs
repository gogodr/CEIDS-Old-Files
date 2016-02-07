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
using System.IO.Ports;

namespace PongClient
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Cliente cliente;
        Thread mandar, ConvSerial;
        String paquete;
        Vector2 pos;
        Bola bola;
        Paleta jugador;
        SerialPort serialport;
        String data;
        int posi;
        String puntaje;
        SpriteFont font;
        Texture2D background;

        Song musica;
        String estado;
        Boolean recibioPlay;
        Cursor[] cursor;
        int i;
       
        private void parseSerialData()
        {
            for (; ; )
            {
                if (data.Length > 8)
                {
                    String dataux = data;
                    if (dataux != "")
                    {
                        if (dataux.LastIndexOf(',') != -1)
                        {
                            dataux = dataux.Substring(0, dataux.LastIndexOf(','));

                            int index = dataux.LastIndexOf(',') + 1;
                            posi = int.Parse(dataux.Substring(index, (dataux.Length - index)));

                        }
                    }

                }
                Thread.Sleep(2);
            }
            
        }


        private void serialPortRecibir(object sender, SerialDataReceivedEventArgs e)
        {
            data += serialport.ReadExisting();
        }

        public void mandacion()
        {
            cliente = new Cliente();
            cliente.conectar("192.168.52.56", 8888);
            for (; ; )
            {
                Thread.Sleep(2);
                try
                {
                    cliente.enviar(100 + "Y" + (512 + (posi - 50) * -10) + "\0");
                }
                catch
                {
                }

                cliente.recibir(ref paquete);

                if (paquete.Split('Y').Length == 4)
                {

                    pos = new Vector2(float.Parse(paquete.Split('Y')[0]), float.Parse(paquete.Split('Y')[1]));
                    puntaje = paquete.Split('Y')[2];
                    estado = paquete.Split('Y')[3];
                   
                }

            }
        }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight=1024;
            graphics.PreferredBackBufferWidth=1280;
            graphics.IsFullScreen = true;
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
            data = "";
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

            bola = new Bola(Content);
            jugador = new Paleta(Content);
            mandar = new Thread(new ThreadStart(mandacion));
            mandar.Start();
            serialport = new SerialPort();
            font = Content.Load<SpriteFont>("Fonts\\Fuente");
            String portname = "";
           
            foreach (string name in SerialPort.GetPortNames())
            {
                portname = name;                
            }
            serialport.PortName = portname;
            serialport.BaudRate = 9600;
            serialport.Parity = Parity.None;
            serialport.DataBits = 8;
            serialport.Open();
            serialport.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPortRecibir);

            ConvSerial = new Thread(new ThreadStart(parseSerialData));
            ConvSerial.Start();
            musica = Content.Load<Song>("Sounds\\HyperspaceBonusLeft");

            
            recibioPlay = false;
            background = Content.Load<Texture2D>("Sprites\\bgLeft");
            cursor = new Cursor[101];
            for (int i = 1; i <= 100; i++)
            {
                cursor[i] = new Cursor(Content);
            }
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
                this.Exit();

            // TODO: Add your update logic here
            bola.updatear(pos);
            jugador.update(posi, bola);


            if (estado == "play")
            {
                if (recibioPlay == false)
                {
                    MediaPlayer.Stop();
                    MediaPlayer.Play(musica);
                    recibioPlay = true;
                }
            }
            else
            {
                recibioPlay = false;
            }
            if (i >= 100)
            {
                i = 1;
            }
            
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
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
            
            bola.dibujar(spriteBatch);
            jugador.draw(spriteBatch);

            i++;
            if (cursor[i].vivo == false)
            {
                cursor[i].dibujarCursor(spriteBatch, bola.posicion);
            }
            for (int a = 1; a < 100; a++)
            {
                if (cursor[a].vivo == true)
                {
                    cursor[a].dibujarCursor(spriteBatch, bola.posicion);
                }
            }

            spriteBatch.DrawString(font, "Puntaje: " + puntaje, new Vector2(150, 45), Color.Yellow);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
