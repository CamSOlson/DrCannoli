using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DrCanoli
{
    public abstract class Entity: IDrawn
    {

        public abstract Rectangle Box
        {
            get;
            set;
        }
        public abstract Rectangle Hitbox
        {
            get;
        }

        public abstract void Update();
        public abstract void Draw(SpriteBatch batch);

    }
}
