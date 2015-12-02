using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Zero
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState mouseState, previousMouseState;
        Vector2 mousePosition = new Vector2(),
            screen = new Vector2(800, 600);
        ArrayList Units;
        Random rand;
        float elapsed;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = (int)screen.X;
            graphics.PreferredBackBufferHeight = (int)screen.Y;
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            this.TargetElapsedTime = TimeSpan.FromSeconds(.015);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            rand = new Random();
            Units = new ArrayList();

            //for (int i = 0; i < 10; i++)
            //    Units.Add(new Debris(new Vector2(rand.Next((int)screen.X), rand.Next((int)screen.Y)), 15 + rand.Next(10), 7780));

            BaseUnit u;
            for(int j = 1; j <= 10; j++)
            for (int i = 1; i <= 10; i++)
            {
                u = new Debris(new Vector2(i * screen.X / 12, j * screen.Y / 12), 10 + rand.Next(20), 1);
                u.Velocity = new Vector2(rand.Next(20) - 10, rand.Next(20) - 10);
                Units.Add(u);
            }

            /*u = new Debris(new Vector2(600, 300), 20, 1);
            u.Velocity = new Vector2(-40, 0);
            u.Spin = 10;
            Units.Add(u);
            u = new Debris(new Vector2(200, 300), 50, 1);
            u.Velocity = new Vector2(40, 0);
            u.Spin = 0;
            Units.Add(u);*/

            BaseUnit.Init(screen);
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
            lDraw.init(GraphicsDevice, spriteBatch);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;

            mouseState = Mouse.GetState();
            mousePosition.X = mouseState.X;
            mousePosition.Y = mouseState.Y;

            for (int i = 0; i < Units.Count; i++)
            {
                for (int j = i + 1; j < Units.Count; j++)
                {
                    if (Units[i].GetType() == typeof(Debris))
                        if (Units[j].GetType() == typeof(Debris))
                            ((Debris)Units[i]).Collision((Debris)Units[j]);
                }
            }

            foreach (BaseUnit unit in Units)
                unit.Update(elapsed);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            foreach (BaseUnit unit in Units)
                unit.Draw();

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
