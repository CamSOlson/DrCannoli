using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DrCanoli
{
	enum GameState { Menu, Settings, Level1, GameOver }	//states of game, more levels can be added as needed

    /// <summary>
    /// This is the main type for your game. Neat! -Cam -Julien -Liam -Alex -Drew
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		GameState gameState = GameState.Menu;	//deafult state brings player to menu
		private Texture2D startTexture;
		private Texture2D optionsTexture;
		private Texture2D exitTexture;

        private List<IDrawn> drawables;
        private List<Enemy> enemyList;
        private Player player;
        private PhysManager phys;

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
            drawables = new List<IDrawn>();
            enemyList = new List<Enemy>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            //player = new Player()
            //phys = new PhysManager(player, enemyList, GraphicsDevice.Viewport.Height); //change viewport to max resolution ingame

			// TODO: use this.Content to load your game content here
			startTexture = Content.Load<Texture2D>("start");
			optionsTexture = Content.Load<Texture2D>("options");
			exitTexture = Content.Load<Texture2D>("exit");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			// TODO: Add your update logic here
			switch (gameState)	//used for transitioning between gameStates
			{
				case GameState.Menu:
					break;
				case GameState.Settings:
					break;
				case GameState.Level1:
					break;
				case GameState.GameOver:
					break;
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
			spriteBatch.Begin();

			switch (gameState)  //used for drawing screen based on gameState
			{
				case GameState.Menu:            //put all menu draw methods here

					spriteBatch.Draw(			//draws start button
						startTexture,
						new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 4 - 25, 100, 50),
						Color.White
						);

					spriteBatch.Draw(           //draws options button
						optionsTexture,
						new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 5 - 25, 100, 50),
						Color.White
						);

					spriteBatch.Draw(           //draws exit button
						exitTexture,
						new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 6 - 25, 100, 50),
						Color.White
						);

					break;
				case GameState.Settings:
					break;
				case GameState.Level1:
					break;
				case GameState.GameOver:
					break;
			}

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
