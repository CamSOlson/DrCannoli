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
        private int suspendedPrevious;

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


		public Player(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet, PhysManager phys, Texture2D shadow, Weapon weapon = null, FighterState fighterState = FighterState.Idle, bool facingRight = true)
            : base(new Rectangle(x, y, width, height), hp, dmg, animSet, fighterState, shadow)
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

        public KeyboardState KBState
        {
            get { return kbState; }
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
                Wep.Box = new Rectangle(Box.X + Box.Width - 5, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);
                Wep.Update();
            }
            else if (Wep != null)
            {
                Wep.Box = new Rectangle(Box.X - Wep.Box.Width + 5, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);
                Wep.Update();
            }



            switch (FighterState)
            {
                case FighterState.Idle:				//Idle state
                    if (Invulnerable)
                    {
                        //Color = Color.Blue;
                        InvulnTime -= Game1.ElapsedTime;
                        if (InvulnTime <= 0)
                        {
                            Invulnerable = false;
                            //Color = Color.White;
                        }
                    }
                    if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.A))		//when D is pressed
                    {
                        FighterState = FighterState.Move;
                        AnimationSet.Idle.Reset();
                        animation = AnimationSet.Walking;

                        if (kbState.IsKeyDown(Keys.D))
                            facingRight = true;
                        else if (kbState.IsKeyDown(Keys.A))
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
                    
                    break;
                case FighterState.Move:             //MoveLeft State
                    if (Invulnerable)
                    {
                        //Color = Color.Blue;
                        InvulnTime -= Game1.ElapsedTime;
                        if (InvulnTime <= 0)
                        {
                            Invulnerable = false;
                            //Color = Color.White;
                        }
                    }

                    if (kbState.IsKeyDown(Keys.A) && Box.X > 0)          //when A is pressed
                    {
                        facingRight = false;
                        Box = new Rectangle((int) Math.Round(Box.X - (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                    }
                    if (kbState.IsKeyDown(Keys.D))     //when D is pressed
                    {
                        facingRight = true;
                        Box = new Rectangle((int) Math.Round(Box.X + (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                    }
                    if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space)) //when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y, Box.Width, Box.Height);
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
                        Box = new Rectangle(Box.X, (int) Math.Round(Box.Y - PhysManager.Unicorns / (60d / Speed * 2d)), Box.Width, Box.Height);
                    }
                    if (kbState.IsKeyDown(Keys.S) && Box.Y + Box.Height < GraphicsDeviceManager.DefaultBackBufferHeight)          //when S is pressed
                    {
                        Box = new Rectangle(Box.X, (int) Math.Round(Box.Y + PhysManager.Unicorns / (60d / Speed * 2d)), Box.Width, Box.Height);
                    }
                    break;
                case FighterState.Jump:					//Jump State
                    if (!Stunned)
                    {
                        if (kbState.IsKeyDown(Keys.A) && Box.X > 0)          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                            if (kbPrevious.IsKeyUp(Keys.A) && VelocityY > 0)
                            {
                                AnimationSet.Idle.Reset();
                                animation = AnimationSet.Walking;
                            }
                        }
                        if (kbState.IsKeyDown(Keys.D))     //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                            if (kbPrevious.IsKeyUp(Keys.D) && VelocityY > 0)
                            {
                                AnimationSet.Idle.Reset();
                                animation = AnimationSet.Walking;
                            }
                        }
                        else if (kbState.IsKeyUp(Keys.A) && VelocityY > 0)
                        {
                            AnimationSet.Walking.Reset();
                            animation = AnimationSet.Idle;
                        }
                        if (kbState.IsKeyDown(Keys.W))            //when W is pressed
                        {
                            InitialY -= (int)(PhysManager.Unicorns / (60 / Speed * 2));
                            //Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop
                            if (InitialY + Box.Height - Box.Height / 8 < Game1.FloorTop)
                            {
                                InitialY = (int)(Game1.FloorTop + Box.Height / 8 - Box.Height);
                            }
                        }
                        if (kbState.IsKeyDown(Keys.S))          //when S is pressed
                        {
                            InitialY += (int)(PhysManager.Unicorns / (60 / Speed * 2));
                            if (InitialY + Box.Height > GraphicsDeviceManager.DefaultBackBufferHeight)
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

                    if (VelocityY <= 0) //suspended jump
                    {
                        if (suspendedPrevious > Box.Y)
                        {
                            VelocityY = 0;
                            FighterState = FighterState.SusJump;
                        }
                    }

                    if (FighterState == FighterState.Jump)
                    {
                        bool done = phys.Jump(this); //IMPORTANT: moving the suspendPrevious update before this line or the suspend jump check after this line will BREAK jumping lol

                        //Falling
                        if (VelocityY <= 0)
                        {
                            animation = AnimationSet.Falling;

                            suspendedPrevious = Box.Y;

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
                        }
                        else
                        {
                            animation = AnimationSet.Jumping;
                        }
                    }
                    break;
                case FighterState.SusJump:					//Suspended Jump State
                    if (kbState.IsKeyDown(Keys.A) && Box.X > 0)          //when A is pressed
                    {
                        facingRight = false;
                        Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                        if (kbPrevious.IsKeyUp(Keys.A) && VelocityY > 0)
                        {
                            AnimationSet.Idle.Reset();
                            animation = AnimationSet.Walking;
                        }
                    }
                    if (kbState.IsKeyDown(Keys.D))     //when D is pressed
                    {
                        facingRight = true;
                        Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                        if (kbPrevious.IsKeyUp(Keys.D) && VelocityY > 0)
                        {
                            AnimationSet.Idle.Reset();
                            animation = AnimationSet.Walking;
                        }
                    }
                    else if (kbState.IsKeyUp(Keys.A) && VelocityY > 0)
                    {
                        AnimationSet.Walking.Reset();
                        animation = AnimationSet.Idle;
                    }
                    if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space)) //when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y, Box.Width, Box.Height);
                        break;
                    }
                    else
                    {
                        phys.DownShift(this);
                        if (suspendedPrevious < Box.Y)
                        {
                            FighterState = FighterState.Jump;
                        }
                        else suspendedPrevious = Box.Y;
                    }

                    break;
            }

            //attacking
            if ((kbState.IsKeyDown(Keys.P) && kbPrevious.IsKeyUp(Keys.P)) ||
                (mState.LeftButton.Equals(ButtonState.Pressed) && mStatePrev.LeftButton.Equals(ButtonState.Released)))
            {
                Wep.Swinging = true;
            }
            else
            {
                wep.Swinging = false;
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
