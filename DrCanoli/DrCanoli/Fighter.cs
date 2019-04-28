using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrCanoli
{

	enum FighterState {Idle, Move, Jump, SusJump}
    class Fighter: Entity
    {
        //fields
        private Rectangle box;
        private int initialY; //just used for jumps and knockback
        private double velocityY, stunTime, invulnTime; //intended for just jumping and knockback but if yall have a use for it have at it, could be good for tracking horizontal movement for a jump?
		private FighterState fighterState;
        //actual stat fields here I haven't really put much thought into how we store and calculate them
        private bool stunned, invuln, jumpSuspended;
        private int hp;
        private int maxHp;
        private int dmg;
        private double speed;
        protected Animation animation;
        protected AnimationSet animationSet;
        protected Texture2D shadow;
        private Color currentColor;
        protected bool facingRight;   //true if last idle state was right, false if last idle state was left
        private int suspendedPrevious;

        public int SuspendedPrevious
        {
            get { return suspendedPrevious; }
            set { suspendedPrevious = value; }
        }

        public Color Color
        {
            get { return currentColor; }
            set { currentColor = value; }
        }

        public bool SuspendedJump
        {
            get { return jumpSuspended; }
            set { jumpSuspended = value; }
        }

        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MaxHp
        {
            get { return maxHp; }
        }
        public int Dmg
        {
            get { return dmg; }
            set { dmg = value; }
        }
        public double Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool FacingRight
        {
            get { return facingRight; }
            set { facingRight = value; }
        }
        public Fighter(Rectangle box, int hp, int damage, AnimationSet animationSet, FighterState fighterState, Texture2D shadow)
        {
            this.box = box;
            this.animationSet = animationSet;
            this.animation = animationSet.Idle;
            initialY = 0;
            velocityY = 0;
            stunned = false;
            stunTime = 0;
            this.fighterState = fighterState;
            this.hp = hp;
            this.maxHp = hp;
            dmg = damage;
            this.shadow = shadow;
            currentColor = Color.White;
            jumpSuspended = false;
        }
        public Fighter(int x, int y, int width, int height, int hp, int dmg, AnimationSet animationSet, FighterState fighterState, Texture2D shadow)
            : this(new Rectangle(x,y,width,height), hp, dmg, animationSet, fighterState, shadow) { }

        public override void Update()
        {
            animation.Update();
            animation.FacingRight = FacingRight;
        }

        public override void Draw(SpriteBatch batch)
        {
            animation.Draw(box, batch, currentColor);
        }

        public virtual void DrawShadow(SpriteBatch batch)
        {
            if (fighterState == FighterState.Jump)
            {
                batch.Draw(shadow,
                    DestinationRectangle,
                    color: Color.White);
            }
            else
            {
                batch.Draw(shadow,
                    destinationRectangle: new Rectangle(box.X - Game1.CameraOffset, box.Y + box.Height - box.Width / 8,
                        box.Width, box.Width / 4),
                    color: Color.White);
            }
        }

        public Rectangle DestinationRectangle
        {
            get { return new Rectangle(box.X - Game1.CameraOffset, initialY + box.Height - box.Width / 8, box.Width, box.Width / 4); }
        }

        public bool Stunned
        {
            get { return stunned; }
            set { stunned = value; }
        }

        public bool Invulnerable
        {
            get { return invuln; }
            set { invuln = value; }
        }

        public double InvulnTime
        {
            get { return invulnTime; }
            set { invulnTime = value; }
        }

        public override Rectangle Box
        {
            get { return box; }
            set { box = value; }
        }
        public override Rectangle Hitbox
        {
            get { return new Rectangle(box.X, box.Y + box.Height - box.Width / 4, box.Width, box.Width / 4); }
        }
        public int InitialY
        {
            get { return initialY; }
            set { initialY = value; }
        }
        public double VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }
        public Animation CurrentAnimation
        {
            get { return animation; }
            set { animation = value; }
        }
        public AnimationSet AnimationSet
        {
            get { return animationSet; }
            set { animationSet = value; }
        }

        public double StunTime
        {
            get { return stunTime; }
            set { stunTime = value; }
        }

		public FighterState FighterState
		{
			get { return fighterState; }
			set { fighterState = value; }
		}

        public double DistanceTo(Fighter f)
        {
            double step = Math.Pow(f.Box.X - box.X, 2) + Math.Pow(f.Box.Y - box.Y, 2);
            return Math.Sqrt(step);
        }
    }
}
