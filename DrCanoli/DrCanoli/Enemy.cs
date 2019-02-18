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
    class Enemy : Fighter
    {
        //enemy specific fields

        public Enemy(Rectangle box, Texture2D sprite) : base(box, sprite)
        {
            //initialize enemy specific fields
        }
        public Enemy(int x, int y, int width, int height, Texture2D sprite) : this(new Rectangle(x, y, width, height), sprite) { }

        public override void Draw(SpriteBatch batch)
        {
            base.Draw(batch);
        }
    }
}