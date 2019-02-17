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
    class Player: Fighter
    {
        //player specific fields

        public Player(Rectangle box): base(box)
        {
            //initialize player specific fields
        }
        public Player(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height)) { }

        public override void Draw(SpriteBatch batch)
        {
            //player draw method, again we should probably call within the spritebatch .begin and .end and not have those here
        }
    }
}
