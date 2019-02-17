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
    class Fighter: IDrawn
    {
        //fields
        private Rectangle box;
        private int initialY; //just used for jumps and knockback
        private double velocityY, velocityX; //intended for just jumping and knockback but if yall have a use for it have at it, could be good for tracking horizontal movement for a jump?
        //actual stat fields here I haven't really put much thought into how we store and calculate them

        public Fighter(Rectangle box)
        {
            this.box = box;
            initialY = 0;
            velocityY = 0;
            velocityX = 0;
        }
        public Fighter(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height)) { }

        public virtual void Draw(SpriteBatch batch)
        {
            //base draw method, we should probably call within the spritebatch .begin and .end and not have those here
        }
    }
}
