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
        private double velocityY, velocityX, stunTime; //intended for just jumping and knockback but if yall have a use for it have at it, could be good for tracking horizontal movement for a jump?
        //actual stat fields here I haven't really put much thought into how we store and calculate them
        private Texture2D sprite;
        private bool stunned;

        public Fighter(Rectangle box, Texture2D sprite)
        {
            this.box = box;
            this.sprite = sprite;
            initialY = 0;
            velocityY = 0;
            velocityX = 0;
            stunned = false;
            stunTime = 0;
        }
        public Fighter(int x, int y, int width, int height, Texture2D sprite) : this(new Rectangle(x, y, width, height), sprite) { }

        public virtual void Draw(SpriteBatch batch)
        {
            batch.Draw(sprite, box, Color.White);
        }

        public bool Stunned
        {
            get { return stunned; }
            set { stunned = value; }
        }

        public Rectangle Box
        {
            get { return box; }
            set { box = value; }
        }
        public int InitialY
        {
            get { return initialY; }
            set { initialY = value; }
        }
        public double VelocityX
        {
            get { return velocityX; }
            set { velocityX = value; }
        }
        public double VelocityY
        {
            get { return velocityY; }
            set { velocityY = value; }
        }
        public Texture2D Sprite
        {
            get { return sprite; }
        }

        public double StunTime
        {
            get { return stunTime; }
            set { stunTime = value; }
        }
    }
}
