using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DrCanoli
{

    class Shockwave: Entity
    {
        private Texture2D texture;
        private Rectangle rect;
        private bool active;
        private int startX;
        private bool facingRight;

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }
        public override Rectangle Box
        {
            get { return rect; }
            set { rect = value; }
        }
        public override Rectangle Hitbox
        {
            get { return new Rectangle(Box.X, Box.Y + PhysManager.Unicorns / 2, Box.Width, PhysManager.Unicorns / 2); }
        }
        public bool FacingRight
        {
            get { return facingRight; }
            set { facingRight = value; }
        }

        public Shockwave(Texture2D texture, Rectangle rect, bool facingRight)
        {
            this.texture = texture;
            this.rect = rect;
            active = true;
            startX = rect.X;
            this.facingRight = facingRight;
        }

        public override void Update()
        {
            //Update position
            if (facingRight)
            {
                rect.X += (int)Math.Round(PhysManager.Unicorns * 5d / 60d);
            }
            else
            {
                rect.X -= (int)Math.Round(PhysManager.Unicorns * 5d / 60d);
            }
            
            //Check for player collision
            foreach (Entity e in Game1.Entities)
            {
                if (e is Player)
                {
                    Player p = ((Player)e);
                    if (p.FighterState != FighterState.Jump && Hitbox.Intersects(p.Hitbox))
                    {
                        p.Hp -= 10;
                        PhysManager.Knockback(p);
                        Game1.RemoveEntity(this);
                    }
                }
            }

            //Remove if 20 or more unicorns from start so they don't stack up and lag
            if (Math.Abs(Box.X - startX) > PhysManager.Unicorns * 7)
            {
                Game1.RemoveEntity(this);
            }
        }

        public override void Draw(SpriteBatch batch)
        {
            batch.Draw(texture,
                destinationRectangle: new Rectangle(rect.X - Game1.CameraOffset, rect.Y, rect.Width, rect.Height),
                color: Color.White,
                effects: facingRight ? SpriteEffects.FlipHorizontally: SpriteEffects.None);
        }
    }
}
