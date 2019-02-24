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
        private int hp;
        private int speed;
        private int dmg;
        private bool active;
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public int Dmg
        {
            get { return dmg; }
            set { dmg = value; }
        }
        public bool Active
        {
            get { return active; }
            set { Active = value; }
        }
        // enemy specific fields

        public Enemy(Rectangle box, Texture2D sprite, int hp, int speed, int dmg) : base(box, sprite)
        {
            this.hp = hp;
            this.speed = speed;
            this.dmg = dmg;
            active = true;
            //initialize enemy specific fields
        }
        public Enemy(int x, int y, int width, int height, Texture2D sprite, int hp, int speed, int dmg) : this(new Rectangle(x, y, width, height), sprite, hp, speed, dmg) { }

        public override void Draw(SpriteBatch batch)
        {
            if(active)
            base.Draw(batch);
        }
        public void Hit(Player player)
        {
            //If a hit lands on the player, this method will be called
            player.Hp -= dmg;
            if (player.Hp <= 0)
                player.Alive = false;
        }
    }
}