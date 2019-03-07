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
        public Student(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, PhysManager phys) : base(x, y, width, height, hp, dmg, animSet, phys)
        {
            this.speed = 3;
        }

        
    }
}
