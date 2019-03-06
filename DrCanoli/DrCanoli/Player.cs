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


		public Player(Rectangle box, int hp, int dmg, AnimationSet animSet, PhysManager phys, Weapon weapon = null, FighterState fighterState = FighterState.Idle, bool facingRight = true): base(box, hp, dmg, animSet, fighterState)
        {
            wep = weapon;
            //100 is just a placeholder value, subject to change
            alive = true;
            this.facingRight = facingRight;
			this.phys = phys;
        }
        public Player(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet, PhysManager phys) : this(new Rectangle(x, y, width, height), hp, dmg, animSet, phys) { }

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
            kbPrevious = kbState;
			kbState = Keyboard.GetState();

            if (Wep != null && facingRight)
            {
                Wep.Box = new Rectangle(Box.X + Box.Width, Box.Y + Box.Height / 2, Wep.Box.Width, Wep.Box.Height);

            }
            else if (Wep != null)
                Wep.Box = new Rectangle(Box.X - Wep.Box.Width, Box.Y + Box.Height / 2, Wep.Box.Width, Wep.Box.Height);

            //PLEASE CONDENSE THIS MESS PLEASE!!!!!! I'M ITALIAN BUT THIS IS TOO MUCH SPAGHETTI!

            switch (FighterState)
			{
				case FighterState.Idle:				//IdleLeft state
                    if (kbState.IsKeyDown(Keys.D) || kbState.IsKeyDown(Keys.W) || kbState.IsKeyDown(Keys.S) || kbState.IsKeyDown(Keys.A))		//when D is pressed
                    {
                        FighterState = FighterState.Move;
                        if (kbState.IsKeyDown(Keys.D))
                            facingRight = true;
                        else if(kbState.IsKeyDown(Keys.S))
							facingRight = false;
                    }
                    if (kbState.IsKeyDown(Keys.Space))	//when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        FighterState = FighterState.Jump;
                        break;
                    }
					break;
				case FighterState.Move:             //MoveLeft State
					if (kbState.IsKeyDown(Keys.A))          //when A is pressed
					{
                        facingRight = false;
						Box = new Rectangle(Box.X - PhysManager.Unicorns / 60, Box.Y, Box.Width, Box.Height);
					}
					else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
					{
                        facingRight = true;
                        Box = new Rectangle(Box.X + PhysManager.Unicorns / 60, Box.Y, Box.Width, Box.Height);
                    }
					if (kbState.IsKeyDown(Keys.Space)) //when Space is pressed
					{
						InitialY = Box.Y;
						VelocityY = PhysManager.InitialYVelocity;
						FighterState = FighterState.Jump;
                        break;
					}
					if (kbState.IsKeyUp(Keys.A) && kbState.IsKeyUp(Keys.W) && kbState.IsKeyUp(Keys.D) && kbState.IsKeyUp(Keys.S))
					{
						FighterState = FighterState.Idle;
						facingRight = false;
					}
					if (kbState.IsKeyUp(Keys.W))            //when W is pressed
                        Box = new Rectangle(Box.X, Box.Y - PhysManager.Unicorns / 60, Box.Width, Box.Height);
                    if (kbState.IsKeyDown(Keys.S))          //when S is pressed
                        Box = new Rectangle(Box.X, Box.Y + PhysManager.Unicorns / 60, Box.Width, Box.Height);
                    break;
				case FighterState.Jump:					//Jump State
                    if (!Stunned)
                    {
                        if (kbState.IsKeyDown(Keys.A))          //when A is pressed
                        {
                            facingRight = false;
                            Box = new Rectangle(Box.X - PhysManager.Unicorns / 60, Box.Y, Box.Width, Box.Height);
                        }
                        else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
                        {
                            facingRight = true;
                            Box = new Rectangle(Box.X + PhysManager.Unicorns / 60, Box.Y, Box.Width, Box.Height);
                        }
                    }
                    else
                        Box = new Rectangle(Box.X - PhysManager.Unicorns / 60, Box.Y, Box.Width, Box.Height);
                    bool done = phys.Jump(this);
                    if (done && (kbState.IsKeyDown(Keys.A) || kbState.IsKeyDown(Keys.D)))
                        FighterState = FighterState.Move;
                    else if (done)
                        FighterState = FighterState.Idle;
                    break;
			}

            animation.FacingRight = facingRight;

			base.Update();
		}

        public override void Draw(SpriteBatch batch)	//has states for drawing character based on state
        {
            //NO state updating here. Only things that DIRECTLY draw to the screen

            base.Draw(batch);

        }
    }
}
