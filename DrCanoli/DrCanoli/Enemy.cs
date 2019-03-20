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
    class Enemy : Fighter
    {
        private bool active;
        private bool facingRight;
        private PhysManager phys;
        private KeyboardState kbState, kbPrevious;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        // enemy specific fields

        public Enemy(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet, PhysManager phys, FighterState fighterState = FighterState.Idle, bool facingRight = true) : base(x, y, width, height, hp, dmg, animSet, fighterState)
        {
            this.facingRight = facingRight;
            active = true;
            kbState = Keyboard.GetState();
            this.phys = phys;
            Speed = 3;
        }

        public override void Draw(SpriteBatch batch)
        {
            if(active)
                base.Draw(batch);
        }

        public override void Update()
        {
            kbPrevious = kbState;
            kbState = Keyboard.GetState();
            switch (FighterState)
            {
                case FighterState.Idle:				//IdleLeft state
                    if (Box.X != phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2 || Box.Y != phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2)	
                    {
                        FighterState = FighterState.Move;
                        AnimationSet.Idle.Reset();
                        animation = AnimationSet.Walking;

                        if (Box.X < phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2)
                            facingRight = true;
                        else if (Box.X > phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2)
                            facingRight = false;
                    }
                    /* edit when enemies can jump on their own
                    if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space))	
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y - 5, Box.Width, Box.Height);
                        break;
                    }
                    */
                    break;
                case FighterState.Move:             //MoveLeft State
                    if (Box.X > phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2 && Box.X > 0)          
                    {
                        facingRight = false;
                        Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                    }
                    else if (Box.X < phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2)   
                    {
                        facingRight = true;
                        Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                    }
                    /* edit when enemies can jump
                    if (kbState.IsKeyDown(Keys.Space) && kbPrevious.IsKeyUp(Keys.Space)) //when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        Box = new Rectangle(Box.X, Box.Y - 5, Box.Width, Box.Height);
                        break;
                    }
                    */
                    if (Box.X == phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2 && Box.Y == phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2)
                    {
                        FighterState = FighterState.Idle;
                        AnimationSet.Walking.Reset();
                        animation = AnimationSet.Idle;
                    }
                    if (Box.Y > phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2 && Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop)            //when W is pressed
                    {
                        Box = new Rectangle(Box.X, (int)(Box.Y - PhysManager.Unicorns / (60 / Speed * 2)), Box.Width, Box.Height);
                    }
                    if (Box.Y < phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2 && Box.Y + Box.Height < GraphicsDeviceManager.DefaultBackBufferHeight)          //when S is pressed
                    {
                        Box = new Rectangle(Box.X, (int)(Box.Y + PhysManager.Unicorns / (60 / Speed * 2)), Box.Width, Box.Height);
                    }
                    break;
                case FighterState.Jump:					//Jump State
                    if (!Stunned)
                    {
                        if (Box.X > phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2 && Box.X > 0)          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle((int)(Box.X - PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
                            if (kbPrevious.IsKeyUp(Keys.A))
                            {
                                AnimationSet.Idle.Reset();
                                animation = AnimationSet.Walking;
                            }
                        }
                        else if (Box.X < phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2)     //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed)), Box.Y, Box.Width, Box.Height);
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
                        if (Box.Y > phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2)            //when W is pressed
                        {
                            InitialY -= (int)(PhysManager.Unicorns / (60 / Speed * 2));
                            //Box.Y + Box.Height - Box.Height / 8 > Game1.FloorTop
                            if (InitialY + Box.Height - Box.Height / 8 < Game1.FloorTop)
                            {
                                InitialY = (int)(Game1.FloorTop + Box.Height / 8 - Box.Height);
                            }
                        }
                        if (Box.Y < phys.Player.Box.Y + phys.Player.Box.Height / 2 - Box.Height / 2)          //when S is pressed
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
                            Box = new Rectangle((int)(Box.X + PhysManager.Unicorns / (60 / Speed * 10)), Box.Y, Box.Width, Box.Height);
                        }
                    }
                    bool done = phys.Jump(this);
                    if (done && (Box.X > phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2 || Box.X < phys.Player.Box.X + phys.Player.Box.Width / 2 - Box.Width / 2))
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
        }
    }
}