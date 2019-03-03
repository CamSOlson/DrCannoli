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

        public Animation Idle
        {
            get { return idle; }
            set { idle = value; }
        }

        public AnimationSet(Animation idle)
        {
            this.idle = idle;
        }
    }
}
