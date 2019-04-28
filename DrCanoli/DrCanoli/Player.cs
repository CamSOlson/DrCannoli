using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace DrCanoli
{
    class Player: Fighter
    {
        private Weapon wep;
        private bool alive;
        KeyboardState kbState, kbPrevious;
        GamePadState gpState, gpPrevious;
        MouseState mState, mStatePrev;
        PhysManager phys;
        SoundEffect hit;
        SoundEffect jump;

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


		public Player(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet, PhysManager phys, Texture2D shadow, SoundEffect hit, SoundEffect jump, Weapon weapon = null, FighterState fighterState = FighterState.Idle, bool facingRight = true)
            : base(new Rectangle(x, y, width, height), hp, dmg, animSet, fighterState, shadow)
        {
            wep = weapon;
			//100 is just a placeholder value, subject to change
            alive = true;
            this.facingRight = facingRight;
            this.phys = phys;
            Stunned = false;
            Speed = 7;

            //Initialize keyboard and mouse, and gamepad states
            kbState = Keyboard.GetState();
            mState = Mouse.GetState();
            gpState = GamePad.GetState(PlayerIndex.One);

            // Initialize sound effects
            this.hit = hit;
            this.jump = jump;
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

            gpPrevious = gpState;
            gpState = GamePad.GetState(PlayerIndex.One);

            if (Wep != null && facingRight)
            {
                Wep.Box = new Rectangle(Box.X + Box.Width - 5, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);
                Wep.Update(this);
            }
            else if (Wep != null)
            {
                Wep.Box = new Rectangle(Box.X - Wep.Box.Width + 5, Box.Y + Box.Height / 2 + 20, Wep.Box.Width, Wep.Box.Height);
                Wep.Update(this);
            }



            switch (FighterState)
            {
                case FighterState.Idle:             //Idle state

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
                    if (!Wep.Swinging)
                    {
                        if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.A) || gpState.DPad.Right == ButtonState.Pressed || gpState.DPad.Up == ButtonState.Pressed || gpState.DPad.Down == ButtonState.Pressed || gpState.DPad.Left == ButtonState.Pressed)        //when D is pressed
                        {
                            FighterState = FighterState.Move;
                            AnimationSet.Idle.Reset();
                            animation = AnimationSet.Walking;

                            if (kbState.IsKeyDown(Keys.D) || gpState.DPad.Right == ButtonState.Pressed)
                                facingRight = true;
                            else if (kbState.IsKeyDown(Keys.A) || gpState.DPad.Left == ButtonState.Pressed)
                                facingRight = false;
                        }
                        if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space) || gpState.Buttons.A == ButtonState.Pressed && gpPrevious.Buttons.A == ButtonState.Released)    //when Space is pressed
                        {
                            if (!SuspendedJump)
                                InitialY = Box.Y;
                            VelocityY = PhysManager.InitialYVelocity;
                            FighterState = FighterState.Jump;
                            Box = new Rectangle(Box.X, Box.Y - 5, Box.Width, Box.Height);
                            jump.Play();
                            break;
                        }
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
                    if (!wep.Swinging)
                    {
                        if ((kbState.IsKeyDown(Keys.A) || gpState.DPad.Left == ButtonState.Pressed) && Box.X > 0)          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle((int)Math.Round(Box.X - (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                        }
                        if (kbState.IsKeyDown(Keys.D) || gpState.DPad.Right == ButtonState.Pressed)    //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle((int)Math.Round(Box.X + (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                        }
                        if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space) || gpState.Buttons.A == ButtonState.Pressed && gpPrevious.Buttons.A == ButtonState.Released) //when Space is pressed
                        {
                            if (!SuspendedJump)
                                InitialY = Box.Y;
                            VelocityY = PhysManager.InitialYVelocity;
                            FighterState = FighterState.Jump;
                            Box = new Rectangle(Box.X, Box.Y, Box.Width, Box.Height);
                            jump.Play();
                            break;
                        }
                        if (kbState.IsKeyUp(Keys.A) && kbState.IsKeyUp(Keys.W) && kbState.IsKeyUp(Keys.D) && kbState.IsKeyUp(Keys.S) && gpState.DPad.Right == ButtonState.Released && gpState.DPad.Up == ButtonState.Released && gpState.DPad.Down == ButtonState.Released && gpState.DPad.Left == ButtonState.Released)
                        {
                            FighterState = FighterState.Idle;
                            AnimationSet.Walking.Reset();
                            animation = AnimationSet.Idle;
                        }
                        if ((kbState.IsKeyDown(Keys.W) || gpState.DPad.Up == ButtonState.Pressed) && Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop)            //when W is pressed
                        {
                            Box = new Rectangle(Box.X, (int)Math.Round(Box.Y - PhysManager.Unicorns / (60d / Speed * 2d)), Box.Width, Box.Height);
                        }
                        if ((kbState.IsKeyDown(Keys.S) || gpState.DPad.Down == ButtonState.Pressed) && Box.Y + Box.Height < GraphicsDeviceManager.DefaultBackBufferHeight)          //when S is pressed
                        {
                            Box = new Rectangle(Box.X, (int)Math.Round(Box.Y + PhysManager.Unicorns / (60d / Speed * 2d)), Box.Width, Box.Height);
                        }
                        if (Box.Y + Box.Height - Box.Height / 8 < Game1.FloorTop)
                        {
                            if (SuspendedJump)
                            {
                                phys.DownShift(this);
                                SuspendedJump = false;
                            }
                            else
                            {
                                FighterState = FighterState.Jump;
                                InitialY = (int)(Game1.FloorTop + Box.Height / 8 - Box.Height);
                                VelocityY = 0;
                            }
                        }
                    }
                    break;
                case FighterState.Jump:					//Jump State
                    if (!Stunned)
                    {
                        if ((kbState.IsKeyDown(Keys.A) || gpState.DPad.Left == ButtonState.Pressed) && Box.X > 0)          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle((int)Math.Round(Box.X - (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                        }
                        if (kbState.IsKeyDown(Keys.D) || gpState.DPad.Right == ButtonState.Pressed)     //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle((int)Math.Round(Box.X + (PhysManager.Unicorns / (60d / Speed))), Box.Y, Box.Width, Box.Height);
                        }
                        if (kbState.IsKeyDown(Keys.W) || gpState.DPad.Up == ButtonState.Pressed)            //when W is pressed
                        {
                            InitialY -= (int)(PhysManager.Unicorns / (60 / Speed * 2));
                            //Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop
                            if (InitialY + Box.Height - Box.Height / 8 < Game1.FloorTop)
                            {
                                InitialY = (int)(Game1.FloorTop + Box.Height / 8 - Box.Height);
                            }
                            else
                                Box = new Rectangle(Box.X, Box.Y - (int)(PhysManager.Unicorns / (60 / Speed * 2)) - 2, Box.Width, Box.Height);
                        }
                        if (kbState.IsKeyDown(Keys.S) || gpState.DPad.Down == ButtonState.Pressed)          //when S is pressed
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
                        if (Box.X > 0 && facingRight)
                        {
                            Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed * 2)), Box.Y, Box.Width, Box.Height);
                        }
                        else if (Box.X > 0)
                        {
                            Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed * 2)), Box.Y, Box.Width, Box.Height);
                        }
                    }


                    bool done = phys.Jump(this); 

                    //Falling
                    if (VelocityY <= 0)
                    {
                        if (!Wep.Swinging)
                            animation = AnimationSet.Falling;


                        if (done && (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.D) || gpState.DPad.Left == ButtonState.Pressed || gpState.DPad.Right == ButtonState.Pressed))
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
                    else if (!Wep.Swinging)
                    {
                        animation = AnimationSet.Jumping;
                    }

                    if (Stunned)
                    {
                        animation = AnimationSet.Knockback;
                    }
                    break;
            }


            //attacking
            if (((kbState.IsKeyDown(Keys.P) && kbPrevious.IsKeyUp(Keys.P)) ||
                (mState.LeftButton.Equals(ButtonState.Pressed) && mStatePrev.LeftButton.Equals(ButtonState.Released)) || (gpState.Buttons.X == ButtonState.Pressed && gpPrevious.Buttons.X == ButtonState.Released)) && !Wep.Swinging && Wep.SwingDuration <= 0 && !Stunned)
            {
                Wep.SwingDuration = Wep.FireRate;
                Wep.Swinging = true;
                animation = Wep.AttackAnimation;
                hit.Play();
            }
            else if (Wep.Swinging && Wep.SwingDuration < .20)
            {
                Wep.Swinging = false;
                if (FighterState == FighterState.Idle)
                    animation = AnimationSet.Idle;
                else if (FighterState == FighterState.Move)
                    animation = AnimationSet.Walking;
            }
            if (Wep.SwingDuration > 0)
            {
                Wep.SwingDuration -= Game1.ElapsedTime;
            }

            //Attacking animation

            animation.FacingRight = facingRight;

            //Damage tester
            if (kbState.IsKeyDown(Keys.H) && kbPrevious.IsKeyUp(Keys.H))
            {
                Hp = Hp - 10;
            }
		}

        public override void Draw(SpriteBatch batch)	//has states for drawing character based on state
        {
            //NO state updating here. Only things that DIRECTLY draw to the screen

            base.Draw(batch);

        }


    }
}
