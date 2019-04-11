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
    class Weapon : IDrawn
    {
        // Fields and properties
        private Rectangle box;
        private Animation attackAnimation;
        protected int damage;
        private bool swinging;
        protected int fireRate;

        public int Damage
        {
            get
            {
                return damage;
            }
            set
            {
                damage = value;
            }
        }
        public int FireRate
        {
            get
            {
                return fireRate;
            }
            set
            {
                fireRate = value;
            }
        }
        public Animation AttackAnimation
        {
            get { return attackAnimation; }
            set { attackAnimation = value; }
        }
        public bool Swinging
        {
            get { return swinging; }
            set { swinging = value; }
        }

        public Rectangle Box
        {
            get { return box; }
            set { box = value; }
        }

        // Class constructor
        public Weapon(Rectangle box, Animation attackAnimation, int damage, int fireRate)
        {
            this.box = box;
            this.damage = damage;
            this.fireRate = fireRate;
            this.attackAnimation = attackAnimation;
            swinging = false;
        }

        public void Update()
        {
            box = new Rectangle(box.X - Game1.CameraOffset, box.Y, box.Width, box.Height);
        }

        // Draw weapon
        public virtual void Draw(SpriteBatch batch)
        {

        }

        public void Swing(int fireRate)
        {
            // Put swing animation code here
        }


    }
}
