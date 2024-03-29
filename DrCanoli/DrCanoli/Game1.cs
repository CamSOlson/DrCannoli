﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;

namespace DrCanoli
{
	enum GameState { Menu, Options, Game, GameOver, Pause, Victory }	//states of game, more levels can be added as needed

    /// <summary>
    /// This is the main type for your game. Neat! -Cam -Julien -Liam -Alex -Drew
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

		GameState gameState = GameState.Menu;   //deafult state brings player to menu
		KeyboardState kbState;
		KeyboardState lastKbState;
        GamePadState gpState;
        GamePadState gpPrevious;

		private Texture2D startTexture;
		private Texture2D optionsTexture;   //place-holder textures for menu buttons
		private Texture2D exitTexture;
		private Texture2D controls;
        private Texture2D obstacleTexture; // texture for obstacle
        private Texture2D bulletTexture;
        private Texture2D titleScreenTexture;
		private Rectangle startButton;
		private Rectangle optionsButton;    //positions for menu buttons
		private Rectangle exitButton;
		private Menu menu;                  //draws menu
		private SpriteFont font;    //just a placeholder font until we get an actual font
		private int gameOverCount;

		private List<IDrawn> drawables;
        private List<Obstacle> obstacles;
        private List<Enemy> enemyList;
        private static List<Entity> entities;
        private static List<Entity> addEntities;
        private static List<Entity> removeEntities;
        private Boss boss;
        private Player player;
        private Background background;
        private PhysManager phys;
        private Texture2D shadowTexture;

        private Texture2D healthBackground;
        private Texture2D healthBar;
        private Texture2D healthBarBoss;

        // Sound effects
        private SoundEffect hit;
        private SoundEffect jump;

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
        public static List<Entity> Entities
        {
            get { return entities; }
        }
        public static void AddEntity(Entity e)
        {
            addEntities.Add(e);
        }
        public static void RemoveEntity(Entity e)
        {
            removeEntities.Add(e);
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth = (int) (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height * 0.75f);
            //graphics.PreferredBackBufferHeight = graphics.PreferredBackBufferWidth / 16 * 9;

            //System.Windows.Forms.Form form = (System.Windows.Forms.Form) System.Windows.Forms.Form.FromHandle(Window.Handle);
            //form.WindowState = System.Windows.Forms.FormWindowState.Maximized;


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

			gameOverCount = 0;
			kbState = new KeyboardState();
			lastKbState = new KeyboardState();
            gpState = new GamePadState();
            gpPrevious = new GamePadState();

			// Get data from text file
			textFile = new TextFile("Content/final.txt");
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
            phys = new PhysManager(player, enemyList, obstacles, GraphicsDevice.Viewport.Height, boss);

			// TODO: use this.Content to load your game content here
			startTexture = Content.Load<Texture2D>("start");
			optionsTexture = Content.Load<Texture2D>("options");	//loads button textures
			exitTexture = Content.Load<Texture2D>("exit");
			controls = Content.Load<Texture2D>("textures/Controls");
            obstacleTexture = Content.Load<Texture2D>("obstacle");
            shadowTexture = Content.Load<Texture2D>("textures/sprites/Shadow");
            bulletTexture = Content.Load<Texture2D>("Bullet");
            titleScreenTexture = Content.Load<Texture2D>("textures/TitleScreen");
            menu = new Menu(startTexture, optionsTexture, exitTexture, startButton, optionsButton, exitButton);
			font = Content.Load<SpriteFont>("placeholderText");

            // Load sound effects
            hit = Content.Load<SoundEffect>("woosh");
            jump = Content.Load<SoundEffect>("jump");

			//Test player
			AnimationSet playerAnimSet = new AnimationSet(
                Animation.LoadAnimation(Animation.CANNOLI_IDLE, Content),
                Animation.LoadAnimation(Animation.CANNOLI_WALKING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_FALLING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_JUMPING, Content),
                Animation.LoadAnimation(Animation.CANNOLI_ATTACK_SANDWICH, Content),
                Animation.LoadAnimation(Animation.CANNOLI_HIT, Content)
            );
            phys = new PhysManager(player, enemyList, obstacles, GraphicsDevice.Viewport.Height, boss);
            player = new Player(0, 0, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, 100, 0, playerAnimSet, phys, shadowTexture, hit, jump,
                new Weapon(
                    new Rectangle(PhysManager.Unicorns * 2, PhysManager.Unicorns * 4 - PhysManager.Unicorns / 4 * 3, PhysManager.Unicorns, PhysManager.Unicorns), 
                Animation.LoadAnimation(Animation.CANNOLI_ATTACK_SANDWICH, Content), 10, 0.5));
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

            healthBarBoss = new Texture2D(graphics.GraphicsDevice, healthBackground.Width, healthBackground.Height);
            data = new Color[healthBar.Width * healthBar.Height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.CornflowerBlue;
            }
            healthBarBoss.SetData(data);

            //Start level
            LevelStart();

        }

        private void LevelStart()
        {
            //Initialize entity list
            entities = new List<Entity>();
            addEntities = new List<Entity>();
            removeEntities = new List<Entity>();

            //Spawn boss
            AnimationSet bossAnimSet = new AnimationSet(
                Animation.LoadAnimation(Animation.ERIN_IDLE, Content),
                Animation.LoadAnimation(Animation.ERIN_WALKING, Content),
                Animation.LoadAnimation(Animation.ERIN_FALLING, Content),
                Animation.LoadAnimation(Animation.ERIN_JUMPING, Content),
                Animation.LoadAnimation(Animation.ERIN_ATTACK_SANDWICH, Content),
                Animation.LoadAnimation(Animation.ERIN_HIT, Content)
            );
            boss = new Boss(PhysManager.Unicorns * 128, PhysManager.Unicorns * 9 - PhysManager.Unicorns * 4, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, bossAnimSet, 200, 0, shadowTexture, phys, bulletTexture);
            entities.Add(boss);
            phys.Boss = boss;
            //Set floor top value
            floorTop = graphics.PreferredBackBufferHeight / 3 * 2;

			//resetting level after death
			player.Hp = 100;
			player.Alive = true;

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
                            Animation.LoadAnimation(Animation.ENEMY_IDLE, Content),
                            Animation.LoadAnimation(Animation.ENEMY_WALKING, Content),
                            Animation.LoadAnimation(Animation.ENEMY_FALLING, Content),
                            Animation.LoadAnimation(Animation.ENEMY_JUMPING, Content),
                            Animation.LoadAnimation(Animation.ENEMY_ATTACK_SANDWICH, Content),
                            Animation.LoadAnimation(Animation.ENEMY_HIT, Content)
                        );
                        Enemy enemy = new Enemy(x, y, PhysManager.Unicorns * 2, PhysManager.Unicorns * 4, 50, 10, animSet, phys, shadowTexture);
                        enemyList.Add(enemy);
                        entities.Add(enemy);
                    }
                    else if (levelData[c][d] == 'O')
                    {
                        Obstacle obstacle = new Obstacle(x, y + 6 * PhysManager.Unicorns, PhysManager.Unicorns, PhysManager.Unicorns, obstacleTexture);
                        obstacles.Add(obstacle);
                        entities.Add(obstacle);
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.F1))
                Exit();

			// TODO: Add your update logic here
			lastKbState = kbState;
			kbState = Keyboard.GetState();
            gpPrevious = gpState;
            gpState = GamePad.GetState(PlayerIndex.One);

			switch (gameState)	//used for transitioning between gameStates
			{
				case GameState.Menu:
					if (menu.startClicked() || gpState.Buttons.A == ButtonState.Pressed)
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
					if (kbState.IsKeyDown(Keys.Escape) && !lastKbState.IsKeyDown(Keys.Escape))  //returns to game when esc is pressed
					{
						gameState = GameState.Menu;
					}
					break;
				case GameState.Game:
					if ((kbState.IsKeyDown(Keys.Escape) && !lastKbState.IsKeyDown(Keys.Escape)) || (gpState.Buttons.Start == ButtonState.Pressed && gpPrevious.Buttons.Start != ButtonState.Pressed))
					{
						gameState = GameState.Pause;
					}

                    //ALWAYS update player, no ifs/elses about it
                    elapsedTime = gameTime.ElapsedGameTime.TotalSeconds;

                    //Add entities that are stored in the add list to avoid concurrent modification exception
                    foreach (Entity e in addEntities)
                    {
                        entities.Add(e);
                    }
                    addEntities.Clear();
                    //Remove entities marked for removal here to avoid concurrent modification exception
                    foreach (Entity e in removeEntities)
                    {
                        entities.Remove(e);
                    }
                    removeEntities.Clear();
                    //Update all entities in the entity list
                    foreach (Entity e in entities)
                    {
                        e.Update();
                        if (e is Enemy && !((Enemy)e).Active)
                        {
                            removeEntities.Add(e);
                        }
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
                    if (!boss.Alive)
                    {               //changes state to gameover screen when player hp reaches 0
                        gameState = GameState.Victory;
                    }

                    break;
				case GameState.GameOver:
					gameOverCount++;
					if (gameOverCount > 180)		//counts to 3 seconds then restarts level
					{
						gameOverCount = 0;
						LevelStart();			//resets player and enemies when level is restarted
						gameState = GameState.Menu;
					}
					break;
                case GameState.Victory:
                    gameOverCount++;
                    if (gameOverCount > 180)        //counts to 3 seconds then restarts level
                    {
                        gameOverCount = 0;
                        LevelStart();           //resets player and enemies when level is restarted
                        gameState = GameState.Menu;
                    }
                    break;
				case GameState.Pause:
					if ((kbState.IsKeyDown(Keys.Escape) && !lastKbState.IsKeyDown(Keys.Escape)) || (gpState.Buttons.Start == ButtonState.Pressed && gpPrevious.Buttons.Start != ButtonState.Pressed))	//returns to game when esc is pressed
					{
						gameState = GameState.Game;
					}
					else if (kbState.IsKeyDown(Keys.M) || gpState.Buttons.B == ButtonState.Pressed)		//goes to menu when m is pressed
					{
						gameState = GameState.Menu;
						LevelStart();           //resets player and enemies when level is restarted
						gameState = GameState.Menu;
					}
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
        private static int EntityComparator(Entity e1, Entity e2)
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
                    spriteBatch.Draw(titleScreenTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.White);
					menu.Draw(spriteBatch);
					break;
				case GameState.Options:
                    spriteBatch.Draw(titleScreenTexture, new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), Color.Gray);
					spriteBatch.Draw(
						controls, 
						new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), 
						Color.White);
					break;
				case GameState.Game:

					//GraphicsDevice.Clear(Color.MonoGameOrange); //placeholder color for testing
                    
                    //This does the clearing, no need to waste time with redundant clears
                    background.Draw(spriteBatch);

                    foreach (Entity ent in entities)
                    {
                        if (ent is Fighter)
                        {
                            ((Fighter)ent).DrawShadow(spriteBatch);
                        }

                        ent.Draw(spriteBatch);

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

                    //Boss health bar
                    spriteBatch.Draw(healthBackground,
                        new Rectangle(graphics.PreferredBackBufferWidth - PhysManager.Unicorns / 2 - healthBackground.Width,
                            PhysManager.Unicorns / 2, healthBackground.Width, healthBackground.Height),
                        Color.White);
                    healthBarWidth = (int)((double)healthBackground.Width * ((double)boss.Hp / (double)boss.MaxHp));
                    spriteBatch.Draw(healthBarBoss,
                        new Rectangle(graphics.PreferredBackBufferWidth - PhysManager.Unicorns / 2 - healthBackground.Width + (healthBackground.Width - healthBarWidth),
                            PhysManager.Unicorns / 2, healthBarWidth, healthBarBoss.Height),
                        Color.White);



                    break;
				case GameState.GameOver:
					GraphicsDevice.Clear(Color.Black);          //placeholder color for testing
					spriteBatch.DrawString(
						font, "Your body is limp, lifeless wholly. You are dead, Dr. Cannoli", new Vector2(10, 10), Color.White
						);

					break;
                case GameState.Victory:
                    GraphicsDevice.Clear(Color.CornflowerBlue);          //placeholder color for testing
                    spriteBatch.DrawString(
                        font, "Congradulations Dr. Cannoli, you have vanquished the memes with your vast knowledge of sandwiches!", new Vector2(10, 10), Color.White
                        );

                    break;
                case GameState.Pause:
					GraphicsDevice.Clear(Color.Gray);      //placeholder color for testing
					spriteBatch.DrawString(
						font, "The game is paused. Press esc to return to game. Press m to go back to menu.", new Vector2(10, 10), Color.White
						);
					break;
			}

			spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}