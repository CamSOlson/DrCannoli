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
		private bool facingRight;	//true if last idle state was right, false if last idle state was left

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


		public Player(Rectangle box, AnimationSet animSet, Weapon weapon = null, FighterState fighterState = FighterState.IdleRight, bool facingRight = true): base(box, animSet)
        {
            wep = weapon;
            //100 is just a placeholder value, subject to change
            hp = 100;
            alive = true;
        }
        public Player(int x, int y, int width, int height, AnimationSet animSet) : this(new Rectangle(x, y, width, height), animSet) { }

		/// <summary>
		/// used to update player's state based on input
		/// </summary>
		public override void Update()
		{
            animation.Update();
			KeyboardState kbState = Keyboard.GetState();
			switch (fighterState)
			{
				case FighterState.IdleLeft:
					if (kbState.IsKeyDown(Keys.A))
						fighterState = FighterState.MoveLeft;
					else if (kbState.IsKeyDown(Keys.D))
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					else if (kbState.IsKeyDown(Keys.W))
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					break;
				case FighterState.IdleRight:
					if (kbState.IsKeyDown(Keys.A))
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))
						fighterState = FighterState.MoveRight;
					else if (kbState.IsKeyDown(Keys.W))
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					break;
				case FighterState.MoveLeft:
					if (kbState.IsKeyDown(Keys.A))
						fighterState = FighterState.MoveLeft;
					else if (kbState.IsKeyDown(Keys.D))
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					else if (kbState.IsKeyDown(Keys.W))
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					break;
				case FighterState.MoveRight:
					if (kbState.IsKeyDown(Keys.A))
					{
						fighterState = FighterState.IdleLeft;
						facingRight = false;
					}
					else if (kbState.IsKeyDown(Keys.D))
						fighterState = FighterState.MoveRight;
					else if (kbState.IsKeyDown(Keys.W))
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						fighterState = FighterState.IdleRight;
						facingRight = true;
					}
					break;
				case FighterState.MoveUp:
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
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						if (facingRight == true)
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					break;
				case FighterState.MoveDown:
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
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						if (facingRight == true)
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					break;
				case FighterState.Jump:
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
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						if (facingRight == true)
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					break;
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
						fighterState = FighterState.MoveUp;
					else if (kbState.IsKeyDown(Keys.S))
						fighterState = FighterState.MoveDown;
					else if (kbState.IsKeyDown(Keys.Space))
						fighterState = FighterState.Jump;
					else if (kbState.IsKeyDown(Keys.P))
						fighterState = FighterState.Attack;
					else
					{
						if (facingRight == true)
							fighterState = FighterState.IdleRight;
						else
							fighterState = FighterState.IdleLeft;
					}
					break;
			}
			base.Update();
		}

        public override void Draw(SpriteBatch batch)	//has states for drawing character based on state
        {
            base.Draw(batch);
            animation.Draw(Box, batch);

            switch (fighterState)
			{
				case FighterState.IdleLeft:
					//draw idle animation facing left
					break;
				case FighterState.IdleRight:
					//draw idle animation facing right
					break;
				case FighterState.MoveLeft:
					//draw walking left animation
					break;
				case FighterState.MoveRight:
					//draw walking right animation
					break;
				case FighterState.MoveUp:
					//draw walking up animation
					break;
				case FighterState.MoveDown:
					//draw walking down animation
					break;
				case FighterState.Jump:
					//draw jump animation
					break;
				case FighterState.Attack:
					//draw attack animation
					break;
			}

        }
    }
}
