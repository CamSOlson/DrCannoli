﻿using System;
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
        Rectangle box;
        Texture2D sprite;
        protected int damage;
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
        protected int fireRate;
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

        // Class constructor
        public Weapon(Rectangle box, Texture2D sprite, int damage, int fireRate)
        {
            this.box = box;
            this.sprite = sprite;
            this.damage = damage;
            this.fireRate = fireRate;
        }

        // Draw weapon
        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite, box, Color.White);
        }

        public void Swing(int fireRate)
        {
            // Put swing animation code here
        }
    }
}