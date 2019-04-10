using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace DrCanoli
{
    class Bullet
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
        public Rectangle Rect
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
        public void Update()
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
            if (rect.Y > 900 || rect.Y < 0 || rect.X > startX + 1600 || rect.X < 0)
            {
                active = false;
            }
        }
        public void Draw(SpriteBatch batch)
        {
                batch.Begin();
                batch.Draw(texture, rect, Color.White);
                batch.End();
        }
    }
}
