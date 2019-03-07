using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DrCanoli
{
    class Boss : Enemy
    {
        public Boss(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, PhysManager phys) : base(x, y, width, height, hp, dmg, animSet, phys)
        {

        }
        public void DrawHealthbar(SpriteBatch batch)
        {
            //The rectangle values are just temporary, draws a boss' healthbar
            //batch.Draw(Sprite, new Rectangle(0, 50, 600, 50), Color.White);
        }
    }
}
