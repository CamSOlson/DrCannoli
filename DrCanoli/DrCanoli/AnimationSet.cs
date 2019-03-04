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

        public AnimationSet(Animation idle = null, Animation walking = null)
        {
            this.idle = idle;
            this.walking = walking;
        }
    }
}
