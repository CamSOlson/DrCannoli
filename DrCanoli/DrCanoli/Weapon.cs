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
    class Weapon
    {
        // Fields and properties
        private Rectangle box, initialBox;
        private Animation attackAnimation;
        protected int damage;
        protected double fireRate;
        private bool swinging;
        private double swingDuration;

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
        public double FireRate
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
        public double SwingDuration
        {
            get { return swingDuration; }
            set { swingDuration = value; }
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
        public Weapon(Rectangle box, Animation attackAnimation, int damage, double fireRate)
        {
            this.box = box;
            this.initialBox = new Rectangle(box.X, box.Y, box.Width, box.Height);
            this.damage = damage;
            this.fireRate = fireRate;
            this.attackAnimation = attackAnimation;
            swinging = false;

        }

        public void Update(Fighter user)
        {
            box = new Rectangle(user.Box.X + initialBox.X, user.Box.Y + initialBox.Y, initialBox.Width, initialBox.Height);

            if (!user.FacingRight)
            {
                box.X -= initialBox.X + user.Box.Width;
            }
        }

        public void Swing(double fireRate)
        {
            // Put swing animation code here
        }


    }
}
