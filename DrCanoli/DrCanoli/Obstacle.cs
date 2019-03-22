﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DrCanoli
{
    class Obstacle: IDrawn
    {
        private Rectangle box;
        private Texture2D sprite;

		public Obstacle(Rectangle box, Texture2D sprite)
        {
            this.box = box;
            this.sprite = sprite;
        }

        public Obstacle(int x, int y, int width, int height, Texture2D sprite) : this(new Rectangle(x, y, width, height), sprite)
		{
			this.sprite = sprite;
		}

        public void Update()
        {
			box.X -= Game1.CameraOffset;
        }

        public void Draw(SpriteBatch batch)
        {
			batch.Draw(sprite, box, Color.White);
		}

        public Rectangle Box
        {
            get { return box; }
            set { box = value; }
        }
    }
}
