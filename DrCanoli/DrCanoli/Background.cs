using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DrCanoli
{
    class Background
    {

        private Texture2D texture;
        private int width;

        public Texture2D Texture
        {
            get { return texture; }
            set
            {
                width = (int)((double)texture.Width * ((double)GraphicsDeviceManager.DefaultBackBufferHeight / (double)texture.Height));
                texture = value;
            }
        }

        public Background(Texture2D texture)
        {
            this.texture = texture;
            width = (int) ((double) texture.Width * ((double) GraphicsDeviceManager.DefaultBackBufferHeight / (double) texture.Height));
        }

        public void Draw(SpriteBatch sb)
        {
            //The x value of the last background
            int lastBackgroundX = (int) (Game1.CameraOffset / width) * width;
            //The x value of the next background
            int nextBackgroundX = (int) (Game1.CameraOffset / width + 1) * width;

            Rectangle lastBkgBounds = new Rectangle(lastBackgroundX - Game1.CameraOffset, 0,
                width, GraphicsDeviceManager.DefaultBackBufferHeight);

            sb.Draw(texture, lastBkgBounds, Color.White);

            Rectangle nextBkgBounds = new Rectangle(nextBackgroundX - Game1.CameraOffset, 0,
                width, GraphicsDeviceManager.DefaultBackBufferHeight);

            sb.Draw(texture, nextBkgBounds, Color.White);

        }

    }
}
