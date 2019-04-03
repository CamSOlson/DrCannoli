using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System.Reflection;

namespace DrCanoli
{
    /// <summary>
    /// A class that loads, stores, and draws animations
    /// 
    /// Cam Olson
    /// </summary>
    class Animation
    {
        //Some basic animation directories for quick loading
        //public static string CANNOLI_IDLE = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "/Content/animations/cannoli/Idle.anim";
        public static string BASE = AppDomain.CurrentDomain.BaseDirectory + @"\..\..\..\..\";
        public static string CANNOLI_IDLE = BASE + @"Content\animations\cannoli\Idle.anim";
        public static string CANNOLI_WALKING = BASE + @"Content\animations\cannoli\Walking.anim";
        public static string CANNOLI_FALLING = BASE + @"Content\animations\cannoli\Falling.anim";
        public static string CANNOLI_JUMPING = BASE + @"Content\animations\cannoli\Jumping.anim";

        private Texture2D texture;
        private Rectangle[] frameBounds;
        private int[] updatesPerFrame;
        private int currentFrame;
        private int updates;
        private int frames;
        private bool facingRight;
        private SpriteEffects spriteEffects;

        public Rectangle this[int frame]
        {
            get { return frameBounds[frame]; }
        }
        public int Frames
        {
            get { return frames; }
        }
        public bool FacingRight
        {
            get { return facingRight; }
            set { facingRight = value; }
        }

        public Animation()
        {
            spriteEffects = SpriteEffects.None;
        }

        public void Update()
        {
            updates++;

            //If the number of recorded updates is greater than the number of updates for the current frame, update current frame and reset updates
            if (updates > updatesPerFrame[currentFrame])
            {
                currentFrame++;
                updates = 0;
            }

            //Make sure current frame never goes over the frame count
            if (currentFrame >= frames)
            {
                currentFrame = 0;
            }

            //Update sprite effects if it needs to be flipped
            if (FacingRight && spriteEffects.Equals(SpriteEffects.FlipHorizontally))
            {
                spriteEffects = SpriteEffects.None;
            }
            else if (!FacingRight && !spriteEffects.Equals(SpriteEffects.FlipHorizontally))
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        /// <summary>
        /// Draw the animation to the screen at the entity's x and y coordinates
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="sb"></param>
        public void Draw(Rectangle bounds, SpriteBatch sb, Color drawColor)
        {
            sb.Draw(texture,
                sourceRectangle: frameBounds[currentFrame],
                destinationRectangle: new Rectangle(bounds.X - Game1.CameraOffset, bounds.Y, bounds.Width, bounds.Height),
                color: drawColor,
                effects: spriteEffects);
        }

        /// <summary>
        /// Load an <code>Animation</code> from a given animation file (type .anim)
        /// </summary>
        /// <param name="dir">The directory to the <code>Animation</code> file</param>
        /// <param name="content">The <code>ContentManager</code> to load the images from</param>
        public void Load(string dir, ContentManager content)
        {
            //Read file
            StreamReader input = null;
            try
            {
                input = new StreamReader(dir);

                //Read first line (texture directory)
                string data = input.ReadLine();
                //Load texture
                texture = content.Load<Texture2D>(data);

                //Read second line (frame times and mappings)
                data = input.ReadLine();
                //Separate into frames (data for each frame is separated by a "|")
                string[] framesData = data.Split('|');
                //Create array to store frame bounds and times
                frames = framesData.Length;
                frameBounds = new Rectangle[frames];
                updatesPerFrame = new int[frames];

                for (int i = 0; i < framesData.Length; i++)
                {
                    string frameData = framesData[i];

                    //Separate bounds information from update count (separated by a ":")
                    string[] frameDataSplit = frameData.Split(':');

                    //Parse frame bounds and add them to the bounds array
                    string boundString = frameDataSplit[0];
                    string[] boundStringSplit = boundString.Split(',');
                    int x1 = 0;
                    Int32.TryParse(boundStringSplit[0], out x1);
                    int y1 = 0;
                    Int32.TryParse(boundStringSplit[1], out y1);
                    int x2 = 0;
                    Int32.TryParse(boundStringSplit[2], out x2);
                    int y2 = 0;
                    Int32.TryParse(boundStringSplit[3], out y2);
                    //Create new rectangle and store it in the frameBounds array
                    frameBounds[i] = new Rectangle(x1, y1, x2 - x1, y2 - y1);

                    //Parse frame times and add them to the update array
                    Int32.TryParse(frameDataSplit[1], out updatesPerFrame[i]);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Close reader
            if (input != null)
            {
                input.Close();
            }
        }

        /// <summary>
        /// Get a new animation from the current dir
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static Animation LoadAnimation(String dir, ContentManager content)
        {
            Animation anim = new Animation();
            anim.Load(dir, content);
            return anim;
        }

        /// <summary>
        /// Set current frame and update counter to 0, effectively "resetting" the animation to the beginning
        /// </summary>
        public void Reset()
        {
            currentFrame = 0;
            updates = 0;
            facingRight = true;
        }
    }
}
