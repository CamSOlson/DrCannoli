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
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
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

        public Player(Rectangle box, Texture2D sprite, Weapon weapon = null): base(box, sprite)
        {
            wep = weapon;
            //100 is just a placeholder value, subject to change
            hp = 100;
            alive = true;
        }
        public Player(int x, int y, int width, int height, Texture2D sprite) : this(new Rectangle(x, y, width, height), sprite) { }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
        public void Hit(Enemy enemy)
        {
            //If a hit lands on the enemy
            enemy.Hp -= wep.Damage;
            if(enemy.Hp <= 0)
                enemy.Active = false;
        }
    }
}
