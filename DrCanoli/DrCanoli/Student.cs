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
        protected int dmg;
        public int Dmg
        {
            get
            {
                return dmg;
            }
            set
            {
                dmg = value;
            }
        }
        protected int hp;
        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }
        protected int speed;
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        // Class constructor
        public Student(Rectangle box, Texture2D sprite, int hp, int speed, int dmg) : base(box, sprite, hp, speed, dmg)
        {
            this.box = box;
            this.sprite = sprite;
            this.hp = hp;
            this.speed = speed;
            this.dmg = dmg;
        }

        
    }
}
