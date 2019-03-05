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
        private int hp;
        private Weapon wep;
        private bool alive;
		private FighterState fighterState;
		private bool facingRight;   //true if last idle state was right, false if last idle state was left
		private bool movingUp;
		private bool movingDown;

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
		public FighterState FighterState
		{
			get { return fighterState; }
			set { fighterState = value; }
		}
		//player specific fields


		public Player(Rectangle box, int hp, int dmg, AnimationSet animSet, Weapon weapon = null, FighterState fighterState = FighterState.IdleRight, bool facingRight = true): base(box, hp, dmg, animSet)
        {
            wep = weapon;
            //100 is just a placeholder value, subject to change
            hp = 100;
            alive = true;
        }
        public Player(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet) : this(new Rectangle(x, y, width, height), hp, dmg, animSet) { }

		/// <summary>
		/// used to update player's state based on input
		/// </summary>
		public override void Update()
		{
            if(hp <= 0)
            {
                alive = false;
            }
            base.Update();
			KeyboardState kbState = Keyboard.GetState();

            if (Wep != null)
            {
                Wep.Box = new Rectangle(Box.X + Box.Width, Box.Y + Box.Height / 2, Wep.Box.Width, Wep.Box.Height);

            }

            //PLEASE CONDENSE THIS MESS PLEASE!!!!!! I'M ITALIAN BUT THIS IS TOO MUCH SPAGHETTI!

            switch (fighterState)
			{
				case FighterState.IdleLeft:				//IdleLeft state
                    if (kbState.IsKeyDown(Keys.A))			//when A is pressed
                        fighterState = FighterState.MoveLeft;
                    else if (kbState.IsKeyDown(Keys.D))		//when D is pressed
                    {
                        fighterState = FighterState.IdleRight;
                        facingRight = true;
                    }
                    else if (kbState.IsKeyDown(Keys.Space))	//when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        fighterState = FighterState.Jump;
                    }
                    else if (kbState.IsKeyDown(Keys.P))		//when P is pressed -- Attack state likely to be replaced with a bool
                        fighterState = FighterState.Attack;
                    else									//when nothing is pressed
                    {
                        fighterState = FighterState.IdleLeft;
                        facingRight = false;
                    }
					if (kbState.IsKeyUp(Keys.W))			//when W is pressed
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))			//when S is pressed
						movingDown = false;
					break;
				case FighterState.IdleRight:			//IdleRight state
					if (kbState.IsKeyDown(Keys.A))			//when A is pressed
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))		//when D is pressed
						fighterState = FighterState.MoveRight;
					else if (kbState.IsKeyDown(Keys.Space))	//When Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        fighterState = FighterState.Jump;
                    }
                    else if (kbState.IsKeyDown(Keys.P))     //When P is pressed -- Attack state likely to be replaced with a bool
						fighterState = FighterState.Attack;
					else									//When nothing is pressed
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
						movingUp = false;
						movingDown = false;
					}
					if (kbState.IsKeyUp(Keys.W))			//when W is pressed
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))			//when S is pressed
						movingDown = false;
					break;
				case FighterState.MoveLeft:             //MoveLeft State
					if (kbState.IsKeyDown(Keys.A))          //when A is pressed
					{
						fighterState = FighterState.MoveLeft;
						Box = new Rectangle(Box.X - PhysManager.Unicorns, Box.Y, Box.Width, Box.Height);
					}
					else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					else if (kbState.IsKeyDown(Keys.Space)) //when Space is pressed
					{
						InitialY = Box.Y;
						VelocityY = PhysManager.InitialYVelocity;
						fighterState = FighterState.Jump;
					}
					else if (kbState.IsKeyDown(Keys.P))     //When P is pressed -- Attack state likely to be replaced with a bool
						fighterState = FighterState.Attack;
					else                                    //when nothing is pressed
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					if (kbState.IsKeyUp(Keys.W))			//when W is pressed
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))			//when S is pressed
						movingDown = false;
					break;
				case FighterState.MoveRight:            //MoveRight State
					if (kbState.IsKeyDown(Keys.A))          //when A is pressed
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))     //when D is pressed
					{
						fighterState = FighterState.MoveRight;
						Box = new Rectangle(Box.X + PhysManager.Unicorns, Box.Y, Box.Width, Box.Height);
					}
					else if (kbState.IsKeyDown(Keys.Space)) //when Space is pressed
					{
						InitialY = Box.Y;
						VelocityY = PhysManager.InitialYVelocity;
						fighterState = FighterState.Jump;
					}
					else if (kbState.IsKeyDown(Keys.P))     //when P is pressed
						fighterState = FighterState.Attack;
					else                                    //when nothing is pressed
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					if (kbState.IsKeyUp(Keys.W))			//when W is pressed
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))			//when S is pressed
						movingDown = false;
					break;
				case FighterState.Jump:					//Jump State
					if (kbState.IsKeyDown(Keys.A))			//when A is pressed
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))		//when D is pressed
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					else if (kbState.IsKeyDown(Keys.Space))	//when Space is pressed
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        fighterState = FighterState.Jump;
                    }
                    else if (kbState.IsKeyDown(Keys.P))     //When P is pressed -- Attack state likely to be replaced with a bool
						fighterState = FighterState.Attack;
					else									//when nothing is pressed
					{
						if (facingRight == true)	//determines direction to face while jumping
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					if (kbState.IsKeyUp(Keys.W))			//when W is pressed
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))			//when S is pressed
						movingDown = false;
					break;
                    /* Ideally we shouldn't use this state, so the player can attack midair and while moving
                     * 
                case FighterState.Attack:
					if (kbState.IsKeyDown(Keys.A))
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					else if (kbState.IsKeyDown(Keys.W))
						movingUp = true;
					else if (kbState.IsKeyDown(Keys.S))
						movingDown = true;
					else if (kbState.IsKeyDown(Keys.Space))
                    {
                        InitialY = Box.Y;
                        VelocityY = PhysManager.InitialYVelocity;
                        fighterState = FighterState.Jump;
                    }
                    else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						if (facingRight == true)
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					if (kbState.IsKeyUp(Keys.W))
						movingUp = false;
					if (kbState.IsKeyDown(Keys.S))
						movingDown = false;
					break;
                    */
			}

			//not in FSM so player can move up and down while also moving left or right
			if(movingUp == true)		//moves player up if movingUp is true
				Box = new Rectangle(Box.X, Box.Y - PhysManager.Unicorns, Box.Width, Box.Height);
			else if(movingDown == true)	//moves player down if movingDown is true
				Box = new Rectangle(Box.X, Box.Y + PhysManager.Unicorns, Box.Width, Box.Height);

			base.Update();
		}

        public override void Draw(SpriteBatch batch)	//has states for drawing character based on state
        {
            //NO state updating here. Only things that DIRECTLY draw to the screen

            base.Draw(batch);

        }
    }
}
