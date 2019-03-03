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
        private int speed;
        private bool active;
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

        public Enemy(Rectangle box, Texture2D sprite, int hp, int dmg, int speed) : base(box.X, box.Y, box.Width, box.Height, hp, dmg)
        {
            this.speed = speed;
            active = true;
            //initialize enemy specific fields
        }
        public Enemy(int x, int y, int width, int height, Texture2D sprite, int hp, int speed, int dmg) : this(new Rectangle(x, y, width, height), sprite, hp, speed, dmg) { }

        public override void Draw(SpriteBatch batch)
        {
            if(active)
            base.Draw(batch);
        }
    }
}