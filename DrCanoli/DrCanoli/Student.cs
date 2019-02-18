using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DrCanoli
{
    // Student class
    class Student : Enemy
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
        protected int health;
        public int Health
        {
            get
            {
                return health;
            }
            set
            {
                health = value;
            }
        }

        // Class constructor
        public Student(Rectangle box, Texture2D sprite, int damage, int health) : base(box, sprite)
        {
            this.box = box;
            this.sprite = sprite;
            this.damage = damage;
            this.health = health;
        }

        
    }
}
