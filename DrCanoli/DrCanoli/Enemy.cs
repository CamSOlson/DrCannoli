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
        private int speed; //should be in terms of unicorns per frame, if not just the same speed as the player
        private bool active;
        private bool facingRight;
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public bool Active
        {
            get { return active; }
            set { Active = value; }
        }
        // enemy specific fields

        public Enemy(Rectangle box, int hp, int dmg, AnimationSet animSet, FighterState fighterState = FighterState.Idle, bool facingRight = true) : base(box, hp, dmg, animSet, fighterState)
        {
            this.facingRight = facingRight;
            active = true;
        }
        public Enemy(int x, int y, int width, int height, int hp, int dmg, AnimationSet animSet) : this(new Rectangle(x, y, width, height), hp, dmg, animSet) { }

        public override void Draw(SpriteBatch batch)
        {
            if(active)
            base.Draw(batch);
        }
    }
}