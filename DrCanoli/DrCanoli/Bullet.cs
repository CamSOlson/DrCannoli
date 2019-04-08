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
        public Bullet(Texture2D texture, Rectangle rect, Direction dir)
        {
            this.texture = texture;
            this.rect = rect;
            this.dir = dir;
        }
    }
}
