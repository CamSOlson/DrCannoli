using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace DrCanoli
{
	enum GameState { Menu, Options, Level1, GameOver }	//states of game, more levels can be added as needed

    /// <summary>
    /// This is the main type for your game. Neat! -Cam -Julien -Liam -Alex -Drew
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		GameState gameState = GameState.Menu;   //deafult state brings player to menu

		private Texture2D startTexture;
		private Texture2D optionsTexture;   //place-holder textures for menu buttons
		private Texture2D exitTexture;
		private Rectangle startButton;
		private Rectangle optionsButton;    //positions for menu buttons
		private Rectangle exitButton;
		private Menu menu;                  //draws menu
		private SpriteFont font;	//just a placeholder font until we get an actual font

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
			this.IsMouseVisible = true;

            drawables = new List<IDrawn>();
            enemyList = new List<Enemy>();

			startButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 4 - 25, 100, 50);
			optionsButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 5 - 25, 100, 50);
			exitButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 6 - 25, 100, 50);

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
			//uncomment these when we can fully initialize player
			//player = new Player()
			//phys = new PhysManager(player, enemyList, GraphicsDevice.Viewport.Height); //change viewport to max resolution ingame
			//drawables.Add(player);
			foreach (Enemy e in enemyList)
			{
				drawables.Add(e);
			}

			// TODO: use this.Content to load your game content here
			startTexture = Content.Load<Texture2D>("start");
			optionsTexture = Content.Load<Texture2D>("options");	//loads button textures
			exitTexture = Content.Load<Texture2D>("exit");
			menu = new Menu(startTexture, optionsTexture, exitTexture, startButton, optionsButton, exitButton);
			font = Content.Load<SpriteFont>("placeholderFont");
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
					if (menu.startClicked())			
						gameState = GameState.Level1;	//goes to level1 state when start is clicked
					if (menu.optionsClicked())
						gameState = GameState.Options;	//goes to options menu state when options is clicked
					if (menu.exitClicked())
						this.Exit();					//closes game when exit is clicked
					break;
				case GameState.Options:
					break;
				case GameState.Level1:
					if (player.Hp <= 0)					//changes state to gameover screen when player hp reaches 0
						gameState = GameState.GameOver;
					break;
				case GameState.GameOver:
					break;
			}

            phys.ElapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

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
					menu.Draw(spriteBatch);
					break;
				case GameState.Options:
					GraphicsDevice.Clear(Color.LawnGreen);      //placeholder color for testing
					spriteBatch.DrawString(
						font, "This is the options menu", new Vector2(10, 10), Color.White
						);
					break;
				case GameState.Level1:
					GraphicsDevice.Clear(Color.MonoGameOrange); //placeholder color for testing
					spriteBatch.DrawString(
						font, "This is level1", new Vector2(10, 10), Color.White
						);
					break;
				case GameState.GameOver:
					GraphicsDevice.Clear(Color.Black);          //placeholder color for testing
					spriteBatch.DrawString(
						font, "This is the Game Over Screen", new Vector2(10, 10), Color.White
						);
					break;
			}

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
