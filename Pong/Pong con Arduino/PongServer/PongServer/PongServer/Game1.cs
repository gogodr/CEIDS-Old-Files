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

namespace PongServer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Thread conversacion,ConvSerial;
        Server server;
        Bola bola;
        Paleta j1, j2;
        SerialPort serialport;
        String data;
        int pos;
        int puntosS, puntosC;
        SpriteFont font;
        Song musica;
        String estado;
        int playcountS;
        Texture2D background;

        Cursor[] cursor;
        int i;
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
                    server.enviar(Math.Truncate(bola.posicion.X).ToString() + "Y" + Math.Truncate(bola.posicion.Y).ToString() + "Y" + puntosC + "Y" + estado + "\0");
                }
                Thread.Sleep(2);
                
            }
            
        }

        public Game1()
        {

            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 1024;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = true;
                       
            
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            data = "";
            base.Initialize();
        }

        private void parseSerialData() {

            for (; ; ) {
                if (data.Length > 8) {
                    String dataux = data;
                    if (dataux != "")
                    {
                        if (dataux.LastIndexOf(',') != -1)
                        {
                            dataux = dataux.Substring(0, dataux.LastIndexOf(','));

                            int index = dataux.LastIndexOf(',') + 1;
                            pos = int.Parse(dataux.Substring(index, (dataux.Length - index)));
                            
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

        protected override void LoadContent()
        {
            playcountS = 0;
            font = Content.Load<SpriteFont>("Fonts\\Fuente");
            puntosC = 0;
            puntosS = 0;
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            j1 = new Paleta(Content);
            j2 = new Paleta(Content);
            bola = new Bola(Content);
            server = new Server("192.168.52.56", 8888);
            server.comenzarServidor();
            conversacion = new Thread(new ThreadStart(envRec));
            conversacion.Start();

            serialport = new SerialPort();
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
            musica = Content.Load<Song>("Sounds\\HyperspaceBonusRight");
            MediaPlayer.IsRepeating = true;

            background = Content.Load<Texture2D>("Sprites\\bgRight");

            cursor = new Cursor[101];
            for (int i = 1; i <= 100; i++)
            {
                cursor[i] = new Cursor(Content);
            }
                     
         

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
            bola.updatear(ref puntosS, ref puntosC);
            j1.update(new Vector2(1160, 512 + (pos-50)*-10), bola,true);
            j2.update(j2.posicion, bola,false);
            base.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.R)) {
                puntosC = 0;
                puntosS = 0;
                bola.nuevaBola();
                MediaPlayer.Stop();
                MediaPlayer.Play(musica);
                estado = "play";
                playcountS = 5;
            }
            if (playcountS >= 0) {                
                playcountS--;
                if (playcountS == 0) {
                    estado = "---";
                }
            }

            if (i >= 100)
            {
                i = 1;
            }

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            // TODO: Add your drawing code here
            base.Draw(gameTime);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);            
            spriteBatch.Draw(background, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(font, "Puntaje: " + puntosS, new Vector2(500, 45), Color.Yellow);
            bola.dibujar(spriteBatch);
            j1.dibujar(spriteBatch);

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

            spriteBatch.End();
        }
    }
}
