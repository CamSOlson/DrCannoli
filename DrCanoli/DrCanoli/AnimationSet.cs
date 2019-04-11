using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrCanoli
{
    class AnimationSet
    {
        private Animation idle;
        private Animation walking;
        private Animation falling;
        private Animation jumping;
        private Animation attacking;
        private Animation knockback;

        public Animation Idle
        {
            get { return idle; }
            set { idle = value; }
        }
        public Animation Walking
        {
            get { return walking; }
            set { walking = value; }
        }
        public Animation Falling
        {
            get { return falling; }
            set { falling = value; }
        }
        public Animation Jumping
        {
            get { return jumping; }
            set { jumping = value; }
        }
        public Animation Attacking
        {
            get { return attacking; }
            set { attacking = value; }
        }
        public Animation Knockback
        {
            get { return knockback; }
            set { knockback = value; }
        }

        public AnimationSet(Animation idle = null, Animation walking = null, Animation falling = null, Animation jumping = null,
            Animation attacking = null, Animation knockback = null)
        {
            this.idle = idle;
            this.walking = walking;
            this.falling = falling;
            this.jumping = jumping;
            this.attacking = attacking;
            this.knockback = knockback;
        }
    }
}
