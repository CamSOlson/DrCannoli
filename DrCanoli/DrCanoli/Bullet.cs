using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DrCanoli
{
    class Bullet: Entity
    {
        private Texture2D texture;
        private Rectangle rect;
        private Direction dir;
        private bool active;
        private int startX;
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
            get { return rect; }
        }
        public Bullet(Texture2D texture, Rectangle rect, Direction dir)
        {
            this.texture = texture;
            this.rect = rect;
            this.dir = dir;
            active = true;
            startX = rect.X;
        }
        public override void Update()
        {
                switch (dir)
                {
                    case Direction.Up:
                        rect.Y -= 5;
                        break;
                    case Direction.Down:
                        rect.Y += 5;
                        break;
                    case Direction.Left:
                        rect.X -= 5;
                        break;
                    case Direction.Right:
                        rect.X += 5;
                        break;
                    default:
                        break;
                }
            if (rect.Y > PhysManager.Unicorns * 9 || rect.Y < 0 || rect.X > startX + (PhysManager.Unicorns * 16) || rect.X < 0)
            {
                active = false;
            }
            /*
            if (list[c].Box.Intersects(new Rectangle(player.Box.X, player.Box.Y + player.Box.Height - player.Box.Width / 8,
player.Box.Width, player.Box.Width / 4)))
            {
                player.Hp -= 10;
                list[c].Active = false;
                list.Remove(list[c]);
                c--;
            }
            */
            
            foreach (Entity e in Game1.Entities)
            {
                if (e is Player)
                {
                    Player p = ((Player)e);
                    if (p.FighterState != FighterState.Jump && Box.Intersects(p.Hitbox))
                    {
                        p.Hp -= 10;
                        PhysManager.Knockback(p);
                    }
                }
            }
        }
        public override void Draw(SpriteBatch batch)
        {
                batch.Draw(texture, new Rectangle(rect.X - Game1.CameraOffset, rect.Y, rect.Width, rect.Height), Color.White);
        }
    }
}
