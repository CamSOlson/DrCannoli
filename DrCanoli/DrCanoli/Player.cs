using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrCanoli
{
    class Player: Fighter
    {
        private Weapon wep;
        private bool alive;
		private bool facingRight;   //true if last idle state was right, false if last idle state was left
        KeyboardState kbState, kbPrevious;
        MouseState mState, mStatePrev;
        PhysManager phys;

        public Weapon Wep
        {
            get { return wep; }
            set { wep = value; }
        }
        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

		//player specific fields


		public Player(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet, PhysManager phys, Weapon weapon = null, FighterState fighterState = FighterState.Idle, bool facingRight = true): base(new Rectangle(x, y, width, height), hp, dmg, animSet, fighterState)
        {
            wep = weapon;
            //100 is just a placeholder value, subject to change
            alive = true;
            this.facingRight = facingRight;
            this.phys = phys;
            Stunned = false;
            Speed = 7;

            //Initialize keyboard and mouse states
            kbState = Keyboard.GetState();
            mState = Mouse.GetState();
        }
      
		/// <summary>
		/// used to update player's state based on input
		/// </summary>
		public override void Update()
		{
            if(Hp <= 0)
            {
                alive = false;
            }
            base.Update();

            //Will eventually make an input manager in polishing stage for better control over input
            kbPrevious = kbState;
			kbState = Keyboard.GetState();

            mStatePrev = mState;
            mState = Mouse.GetState();

            if (Wep != null && facingRight)
            {
                Wep.Box = new Rectangle(Box.X + Box.Width - 5 - Game1.CameraOffset, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);

            }
            else if (Wep != null)
                Wep.Box = new Rectangle(Box.X - Wep.Box.Width + 5 - Game1.CameraOffset, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);

            switch (FighterState)
			{
				case FighterState.Idle:				//IdleLeft state
                    if (Invulnerable)
                    {
                        InvulnTime -= Game1.ElapsedTime;
                        if (InvulnTime <= 0)
                            Invulnerable = false;
                    }
                    if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.A))		//when D is pressed
                    {
                        FighterState = FighterState.Move;
                        AnimationSet.Idle.Reset();
                        animation = AnimationSet.Walking;

                        if (kbState.IsKeyDown(Keys.D))
                            facingRight = true;
                        else if(kbState.IsKeyDown(Keys.A))
							facingRight = false;
                    }
                    if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space))	//when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y - 5, Box.Width, Box.Height);
                        break;
                    }
                    if (kbState.IsKeyDown(Keys.P) && kbPrevious.IsKeyUp(Keys.P))	//triggers attack on P press
                    {
                        Wep.Swinging = true;
                    }
                    else
                    {
                        wep.Swinging = false;
                    }
					break;
				case FighterState.Move:             //MoveLeft State
                    if (Invulnerable)
                    {
                        InvulnTime -= Game1.ElapsedTime;
                        if (InvulnTime <= 0)
                            Invulnerable = false;
                    }
					if (kbState.IsKeyDown(Keys.A) && Box.X > 0)          //when A is pressed
					{
                        facingRight = false;
						Box = new Rectangle((int) (Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
					}
					else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
					{
                        facingRight = true;
                        Box = new Rectangle((int) (Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                    }
					if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space)) //when Space is pressed
					{
						InitialY = Box.Y;
						VelocityY = PhysManager.InitialYVelocity;
						FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y - 5, Box.Width, Box.Height);
                        break;
					}
					if (kbState.IsKeyUp(Keys.A) && kbState.IsKeyUp(Keys.W) && kbState.IsKeyUp(Keys.D) && kbState.IsKeyUp(Keys.S))
					{
						FighterState = FighterState.Idle;
                        AnimationSet.Walking.Reset();
                        animation = AnimationSet.Idle;
					}
                    if (kbState.IsKeyDown(Keys.W) && Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop)            //when W is pressed
                    {
                        Box = new Rectangle(Box.X, (int) (Box.Y - PhysManager.Unicorns / (60 / Speed * 2)), Box.Width, Box.Height);
                    }
                    if (kbState.IsKeyDown(Keys.S) && Box.Y + Box.Height < GraphicsDeviceManager.DefaultBackBufferHeight)          //when S is pressed
                    {
                        Box = new Rectangle(Box.X, (int) (Box.Y + PhysManager.Unicorns / (60 / Speed * 2)), Box.Width, Box.Height);
                    }
                    if (kbState.IsKeyDown(Keys.P) && kbPrevious.IsKeyUp(Keys.P))
                    {
                        Wep.Swinging = true;
                    }
                    else
                    {
                        wep.Swinging = false;
                    }
                    break;
				case FighterState.Jump:					//Jump State
                    if (!Stunned)
                    {
                        if (kbState.IsKeyDown(Keys.A) && Box.X > 0)          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle((int) (Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                            if (kbPrevious.IsKeyUp(Keys.A))
                            {
                                AnimationSet.Idle.Reset();
                                animation = AnimationSet.Walking;
                            }
                        }
                        else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle((int) (Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                            if (kbPrevious.IsKeyUp(Keys.D))
                            {
                                AnimationSet.Idle.Reset();
                                animation = AnimationSet.Walking;
                            }
                        }
                        else
                        {
                            AnimationSet.Walking.Reset();
                            animation = AnimationSet.Idle;
                        }
                        if (kbState.IsKeyDown(Keys.W))            //when W is pressed
                        {
                            InitialY -= (int) (PhysManager.Unicorns / (60 / Speed * 2));
                            //Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop
                            if (InitialY + Box.Height - Box.Height / 8 < Game1.FloorTop)
                            {
                                InitialY = (int)(Game1.FloorTop + Box.Height / 8 - Box.Height);
                            }
                        }
                        if (kbState.IsKeyDown(Keys.S))          //when S is pressed
                        {
                            InitialY += (int) (PhysManager.Unicorns / (60 / Speed * 2));
                            if(InitialY + Box.Height > GraphicsDeviceManager.DefaultBackBufferHeight)
                            {
                                InitialY = GraphicsDeviceManager.DefaultBackBufferHeight - Box.Height;
                            }
                        }
                    }
                    else
                    {
                        if (Box.X > 0)
                        {
                            Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed * 2)), Box.Y, Box.Width, Box.Height);
                        }
                    }

                    if ((kbState.IsKeyDown(Keys.P) && kbPrevious.IsKeyUp(Keys.P)) ||
                        (mState.LeftButton.Equals(ButtonState.Pressed) && mStatePrev.LeftButton.Equals(ButtonState.Released)))
                    {
                        Wep.Swinging = true;
                    }
                    else
                    {
                        wep.Swinging = false;
                    }

                    bool done = phys.Jump(this);
                    if (done && (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.D)))
                    {
                        FighterState = FighterState.Move;
                        AnimationSet.Idle.Reset();
                        animation = AnimationSet.Walking;
                    }
                    else if (done)
                    {
                        FighterState = FighterState.Idle;
                        AnimationSet.Walking.Reset();
                        animation = AnimationSet.Idle;
                    }
                    break;
			}

            animation.FacingRight = facingRight;

            //Damage tester
            if (kbState.IsKeyDown(Keys.H) && kbPrevious.IsKeyUp(Keys.H))
            {
                Hp = Hp - 10;
            }

			base.Update();
		}

        public override void Draw(SpriteBatch batch)	//has states for drawing character based on state
        {
            //NO state updating here. Only things that DIRECTLY draw to the screen

            base.Draw(batch);

        }
    }
}
