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

        public Enemy(Rectangle box) : base(box)
        {
            //initialize enemy specific fields
        }
        public Enemy(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height)) { }

        public override void Draw(SpriteBatch batch)
        {
            //enemy draw method, again we should probably call within the spritebatch .begin and .end and not have those here
        }
    }
}