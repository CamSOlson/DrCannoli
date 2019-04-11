using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace DrCanoli
{
	enum GameState { Menu, Options, Game, GameOver }	//states of game, more levels can be added as needed

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
        private Texture2D obstacleTexture; // texture for obstacle
        private Texture2D bulletTexture;
		private Rectangle startButton;
		private Rectangle optionsButton;    //positions for menu buttons
		private Rectangle exitButton;
		private Menu menu;                  //draws menu
		private SpriteFont font;	//just a placeholder font until we get an actual font

		private List<IDrawn> drawables;
        private List<Obstacle> obstacles;
        private List<Fighter> entities;
        private List<Enemy> enemyList;
        private Boss boss;
        private Player player;
        private Background background;
        private PhysManager phys;
        private Texture2D shadowTexture;

        private Texture2D healthBackground;
        private Texture2D healthBar;

        // Sound effects
        private SoundEffect hit;

        private static double elapsedTime;
        

        // List of enemy positions
        private List<List<char>> levelData;
        // Text file
        private TextFile textFile;

        private static int cameraOffset; //Stores how far in the x direction the camera should be shifted. MUST be static to access in other classes
        private static int floorTop; //Stores how far from the top of the screen the floor should be

        public static int CameraOffset
        {
            get { return cameraOffset; }
            set { cameraOffset = value; }
        }
        public static int FloorTop
        {
            get { return floorTop; }
            set { floorTop = value; }
        }
        public static double ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
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
			this.IsMouseVisible = true;

            drawables = new List<IDrawn>();
            enemyList = new List<Enemy>();
            obstacles = new List<Obstacle>();
			//menu buttons
			startButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 4 - 25, 100, 50);
			optionsButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 5 - 25, 100, 50);
			exitButton = new Rectangle(GraphicsDevice.Viewport.Width / 2 - 50, (GraphicsDevice.Viewport.Height / 8) * 6 - 25, 100, 50);

            // Get data from text file
            textFile = new TextFile("Content/obstacles.txt");
            levelData = textFile.Read();
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
			drawables.Add(player);
			foreach (Enemy e in enemyList)
			{
				drawables.Add(e);
			}

			// TODO: use this.Content to load your game content here
			startTexture = Content.Load<Texture2D>("start");
			optionsTexture = Content.Load<Texture2D>("options");	//loads button textures
			exitTexture = Content.Load<Texture2D>("exit");
            obstacleTexture = Content.Load<Texture2D>("obstacle");
            shadowTexture = Content.Load<Texture2D>("textures/sprites/Shadow");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            menu = new Menu(startTexture, optionsTexture, exitTexture, startButton, optionsButton, exitButton);
			font = Content.Load<SpriteFont>("placeholderText");
            hit = Content.Load<SoundEffect>("woosh");

			//Test player
			AnimationSet playerAnimSet = new AnimationSet(
                Animation.LoadAnimation(Animation.CANNOLI_IDLE, Content),
                Animation.LoadAnimation(Animation.CANNOLI_WALKING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_FALLING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_JUMPING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_ATTACK_SANDWICH, Content),
                Animation.LoadAnimation(Animation.CANNOLI_HIT, Content)
            );
            AnimationSet animSet = new AnimationSet(
                            Animation.LoadAnimation(Animation.CANNOLI_IDLE, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_WALKING, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_FALLING, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_JUMPING, Content)
                        );
            phys = new PhysManager(player, enemyList, obstacles, GraphicsDevice.Viewport.Height);
            player = new Player(0, 0, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, 100, 0, playerAnimSet, phys, shadowTexture,
                new Weapon(new Rectangle(0, 0, (int)(PhysManager.Unicorns * 1.4), PhysManager.Unicorns), 
                Animation.LoadAnimation(Animation.CANNOLI_ATTACK_SANDWICH, Content), 10, 1));
            phys.Player = player;

            //Background
            background = new Background(Content.Load<Texture2D>("textures/backgrounds/Classroom"));

            //Health bar
            healthBackground = new Texture2D(graphics.GraphicsDevice, PhysManager.Unicorns * 4, PhysManager.Unicorns / 2);
            Color[] data = new Color[healthBackground.Width * healthBackground.Height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.DarkGray;
            }
            healthBackground.SetData(data);

            healthBar = new Texture2D(graphics.GraphicsDevice, healthBackground.Width, healthBackground.Height);
            data = new Color[healthBar.Width * healthBar.Height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.IndianRed;
            }
            healthBar.SetData(data);
            boss = new Boss(PhysManager.Unicorns * 16, 0, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, animSet, 200, 0, phys, shadowTexture, healthBar, player, bulletTexture);
            LevelStart();

        }

        private void LevelStart()
        {
            //Set floor top value
            floorTop = graphics.PreferredBackBufferHeight / 3 * 2;

            //Initialize entity list
            entities = new List<Fighter>();

            for (int c = 0; c < levelData.Count; c++)
            {
                for (int d = 0; d < levelData[c].Count; d++)
                {
                    int x = 10 * d;
                    int y;
                    if (c == 0)
                    {
                        y = 10;
                    }
                    else
                    {
                        y = GraphicsDevice.Viewport.Height / 6 * c;
                    }
                    if (levelData[c][d] == 'X')
                    {
                        player.Box = new Rectangle(x, y, player.Box.Width, player.Box.Height);
                        entities.Add(player);
                    }
                    else if (levelData[c][d] == 'E')
                    {
                        AnimationSet animSet = new AnimationSet(
                            Animation.LoadAnimation(Animation.CANNOLI_IDLE, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_WALKING, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_FALLING, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_JUMPING, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_ATTACK_SANDWICH, Content),
                            Animation.LoadAnimation(Animation.CANNOLI_HIT, Content)
                        );
                        Enemy enemy = new Enemy(x, y, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, 50, 10, animSet, phys, shadowTexture);
                        enemyList.Add(enemy);
                        entities.Add(enemy);
                    }
                    else if (levelData[c][d] == 'O')
                    {
                        Obstacle obstacle = new Obstacle(x, y, PhysManager.Unicorns, PhysManager.Unicorns, obstacleTexture);
                        obstacles.Add(obstacle);
                    }
                }
            }
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
                    {
                        gameState = GameState.Game;	//goes to level1 state when start is clicked
                    }
                    if (menu.optionsClicked())
                    {
                        gameState = GameState.Options;	//goes to options menu state when options is clicked
                    }
                    if (menu.exitClicked())
                    {
                        this.Exit();					//closes game when exit is clicked
                    }
                    break;
				case GameState.Options:
					break;
				case GameState.Game:
                    //ALWAYS update player, no ifs/elses about it
                    elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;
                    player.Update();
                    boss.Update();
                    boss.UpdateBullets();
                    foreach (Enemy e in enemyList)
                    {
                        e.Update();
                    }
                    phys.CheckCollisions();

                    //Update camera
                    cameraOffset = player.Box.X - graphics.PreferredBackBufferWidth / 2 + player.Box.Width / 2;

                    if (cameraOffset < 0)
                    {
                        cameraOffset = 0;
                    }

					//
					foreach (Obstacle obstacle in obstacles)
					{
						obstacle.Update();
					}

                    //Sort entities
                    SortEntities();
                    phys.UpdateClosest();
                    

                    if (!player.Alive)
					{               //changes state to gameover screen when player hp reaches 0
						gameState = GameState.GameOver;
					}

                    break;
				case GameState.GameOver:
					break;
			}

            //phys.ElapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// Sort the entity list to order the player/enemies so they are layered correctly
        /// </summary>
        private void SortEntities()
        {
            entities.Sort(EntityComparator);
        }
        /// <summary>
        /// The Comparaison<Fighter> that compares entities for sorting purposes
        /// </summary>
        /// <param name="e1"></param>
        /// <param name="e2"></param>
        /// <returns></returns>
        private static int EntityComparator(Fighter e1, Fighter e2)
        {
            if (e1.Box.Y + e1.Box.Height > e2.Box.Y + e2.Box.Height)
            {
                //e1 is in front of e2, so must be rendered later
                return 1;
            }
            else if (e1.Box.Y + e1.Box.Height < e2.Box.Y + e2.Box.Height)
            {
                //e1 is behind e2, so must be rendered earlier
                return -1;
            }
            return 0;
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
            
            //Eventually, all of these should/will be moved to individual class files to make it more organized


			switch (gameState)  //used for drawing screen based on gameState
			{
				case GameState.Menu:            //put all menu draw methods here
					menu.Draw(spriteBatch);
					break;
				case GameState.Options:
					GraphicsDevice.Clear(Color.LawnGreen);      //placeholder color for testing
					spriteBatch.DrawString(
						font, "Options", new Vector2(10, 10), Color.White
						);
					break;
				case GameState.Game:

					//GraphicsDevice.Clear(Color.MonoGameOrange); //placeholder color for testing
                    
                    //This does the clearing, no need to waste time with redundant clears
                    background.Draw(spriteBatch);
                    boss.Draw(spriteBatch);
                    boss.DrawHealthbar(spriteBatch);
                    boss.DrawBullets(spriteBatch);
                    //Entities (enemies and player)
                    foreach (Fighter ent in entities)
                    {
                        if (ent is Enemy && ((Enemy) ent).Active)
                        {
                            ent.DrawShadow(spriteBatch);
                            ent.Draw(spriteBatch);
                        }
                        else if (ent is Player)
                        {
                            ent.DrawShadow(spriteBatch);
                            ent.Draw(spriteBatch);
                            //This will be moved into player eventually, and removed when the animation is finished
                            if (player.Wep != null)
                                player.Wep.Draw(spriteBatch);
                        }
                    }

					//draws the obstacles
					foreach (Obstacle obstacle in obstacles)
					{
						obstacle.Draw(spriteBatch);
					}

                    //GUI
                    //Health bar
                    spriteBatch.Draw(healthBackground,
                        new Rectangle(PhysManager.Unicorns / 2, PhysManager.Unicorns / 2, healthBackground.Width, healthBackground.Height),
                        Color.White);
                    int healthBarWidth = (int) ((double) healthBackground.Width * ((double) player.Hp / (double) player.MaxHp));
                    spriteBatch.Draw(healthBar,
                        new Rectangle(PhysManager.Unicorns / 2, PhysManager.Unicorns / 2, healthBarWidth, healthBar.Height),
                        Color.White);


                    break;
				case GameState.GameOver:
					GraphicsDevice.Clear(Color.Black);          //placeholder color for testing
					spriteBatch.DrawString(
						font, "Your body is limp, lifeless wholly. You are dead, Dr. Cannoli", new Vector2(10, 10), Color.White
						);
					break;
			}

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}