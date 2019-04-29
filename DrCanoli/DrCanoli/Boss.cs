using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;



namespace DrCanoli
{
    enum BossState
    {
        Jumping, Walking, Idle
    }

    class Boss : Fighter
    {

        private BossState state;
        private int timer;
        private PhysManager phys;
        private Texture2D bulletTexture;
        private Random rando;
        private int startY;
        private int endY;
        private bool alive;

        public Boss(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, Texture2D shadow, PhysManager phys, Texture2D bulletTexture)
            : base(x, y, width, height, hp, dmg, animSet, FighterState.Idle, shadow)
        {
            state = BossState.Idle;
            this.phys = phys;
            this.bulletTexture = bulletTexture;
            rando = new Random();
            alive = true;
        }

        public bool Alive
        {
            get { return (Hp > 0); }
        }

        public override void Update()
        {
            base.Update();

            if (Invulnerable)
            {
                //Color = Color.Blue;
                InvulnTime -= Game1.ElapsedTime;
                if (InvulnTime <= 0)
                {
                    Invulnerable = false;
                    //Color = Color.White;
                }
            }

            if (phys.Player.Box.X < Box.X)
            {
                //If player is to the left of the enemy
                facingRight = false;
            }
            else
            {
                //If player is to the right
                facingRight = true;
            }


            switch (state)
            {
                default:
                    //Idle
                    FighterState = FighterState.Idle;

                    //Every second, send a shockwave
                    if (timer == 0 || timer == 60 || timer == 120)
                    {
                        animation = AnimationSet.Attacking;
                        SpawnShockwave();
                    }
                    //Half a second after sending each shockwave, update animation back to idle
                    if (timer == 30 || timer == 90 || timer == 150)
                    {
                        animation = AnimationSet.Idle;
                    }
                    //After 4 seconds, move to next state
                    if (timer >= 180)
                    {
                        //Set timer to -1 so after switch it will update to 0 before updating with next state
                        timer = -1;

                        startY = Box.Y;

                        //If the boss is not within 1 unicorn of the bottom, it will move to the bottom
                        if (Box.Y + Box.Height < PhysManager.Unicorns * 8)
                        {
                            endY = PhysManager.Unicorns * 9;
                        }
                        else
                        {
                            endY = (int) Math.Round(PhysManager.Unicorns * 6.5d);
                        }

                        //Move to next position by a randomly chosen method
                        if (rando.Next(2) % 2 == 0)
                        {
                            //move by jumping
                            state = BossState.Jumping;
                        }
                        else
                        {
                            //move by walking
                            state = BossState.Walking;
                        }
                    }
                    break;
                case BossState.Jumping:
                    //Jumping to next position
                    FighterState = FighterState.Jump;

                    if (timer >= 0)
                    {
                        //If just started, start jump
                        if (timer == 0)
                        {
                            VelocityY = PhysManager.InitialYVelocity;
                            animation = AnimationSet.Jumping;
                            InitialY = startY;
                        }

                        bool done = phys.Jump(this);

                        //Falling
                        if (VelocityY < 0)
                        {
                            animation = AnimationSet.Falling;
                        }

                        if (done)
                        {
                            //If jump is done, set timer to negatives and count back up towards 0 for a delay
                            FighterState = FighterState.Idle;
                            Box = new Rectangle(Box.X, endY - Box.Height, Box.Width, Box.Height);
                            InitialY = endY - Box.Height;
                            timer = -60;
                            animation = AnimationSet.Idle;
                        }
                        else
                        {
                            //Move towards the end point
                            int moveAmount = (int)Math.Round(PhysManager.Unicorns * 2d / 30d);

                            //If end is greater than 1 unicorn above the bottom, negate the value
                            if (endY > PhysManager.Unicorns * 8)
                            {
                                moveAmount = -moveAmount;
                            }

                            InitialY -= moveAmount;
                            Box = new Rectangle(Box.X, Box.Y - moveAmount, Box.Width, Box.Height);
                        }

                    }
                    else if (timer == -1)
                    {
                        //This is a delay before moving to next state to give the player a chance to attack
                        state = BossState.Idle;
                    }
                    break;
                case BossState.Walking:
                    //Walking to next position

                    //Every second, send a shockwave
                    if (timer == 30 || timer == 90 || timer == 150)
                    {
                        FighterState = FighterState.Idle;
                        animation = AnimationSet.Attacking;
                        SpawnShockwave();
                    }
                    //Half a second after sending each shockwave, update animation to moving and move to the end position
                    if ((timer >= 0 && timer < 30) || (timer >= 60 && timer < 90) || (timer >= 120 && timer < 150) || (timer >= 180 && timer < 210))
                    {
                        FighterState = FighterState.Move;
                        animation = AnimationSet.Walking;

                        //Move towards the end point
                        int moveAmount = (int)Math.Round(PhysManager.Unicorns * 2d / 90d);

                        //If end is greater than 1 unicorn above the bottom, negate the value
                        if (endY > PhysManager.Unicorns * 8)
                        {
                            moveAmount = -moveAmount;
                        }

                        InitialY -= moveAmount;
                        Box = new Rectangle(Box.X, Box.Y - moveAmount, Box.Width, Box.Height);
                    }
                    //After 4 seconds, move to next state
                    if (timer >= 210)
                    {
                        //Set timer to -1 so after switch it will update to 0 before updating with next state
                        timer = -1;
                        state = BossState.Idle;
                        Box = new Rectangle(Box.X, endY - Box.Height, Box.Width, Box.Height);
                    }
                    break;
            }

            timer++;

        }

        private void SpawnShockwave()
        {
            Game1.AddEntity(new Shockwave(bulletTexture,
                new Rectangle(Box.X + (facingRight ? PhysManager.Unicorns * 2 : PhysManager.Unicorns * -2),
                Box.Y + Box.Height - PhysManager.Unicorns + 1, PhysManager.Unicorns * 2, PhysManager.Unicorns),
                facingRight));
        }

        //Old update method. Keeping just in case, but otherwise DO NOT USE
        /*
        public void OldUpdate()
        {
                switch (state)
                {
                    case BossState.Top:
                        if (timer >= 300)
                        {
                            timer = 0;
                            state = BossState.MovingDown;
                        }
                        else
                        {
                        if (Math.Abs(Box.X - player.Box.X) < PhysManager.Unicorns * 16)
                        {
                            if (timer % 100 == 0)
                            {
                                Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Down));
                            }
                            else if (timer % 50 == 0)
                            {
                                if (player.Box.X > Box.X)
                                {
                                    Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Right));
                                }
                                else if (player.Box.X < Box.X)
                                {
                                    Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Left));
                                }
                            }
                        }
                            timer++;
                        }
                        break;
                    case BossState.MovingDown:
                        Box = new Rectangle(Box.X, Box.Y + 1, Box.Width, Box.Height);
                        if (Box.Y >= (PhysManager.Unicorns * 9) - Box.Height)
                        {
                            Box = new Rectangle(Box.X, (PhysManager.Unicorns * 9) - Box.Height, Box.Width, Box.Height);
                            state = BossState.Bottom;
                        }
                        break;
                    case BossState.Bottom:
                        if (timer >= 300)
                        {
                            timer = 0;
                            state = BossState.MovingUp;
                        }
                        else
                        {
                        if(Math.Abs(Box.X-player.Box.X) < PhysManager.Unicorns * 16)
                        {
                            if (timer % 100 == 0)
                            {
                                Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Up));
                            }
                            else if (timer % 50 == 0)
                            {
                                if (player.Box.X > Box.X)
                                {
                                    Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Right));
                                }
                                else if (player.Box.X < Box.X)
                                {
                                    Game1.AddEntity(new Shockwave(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Left));
                                }
                            }
                        }
                            timer++;
                        }
                        break;
                    case BossState.MovingUp:
                        Box = new Rectangle(Box.X, Box.Y - 1, Box.Width, Box.Height);
                        if (Box.Y <= 0)
                        {
                            Box = new Rectangle(Box.X, 0, Box.Width, Box.Height);
                            state = BossState.Top;
                        }
                        break;
                    default:
                        break;
                }
            
        }
        */
    }
}
