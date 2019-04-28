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
enum BossStates
{
    Top,
    MovingDown,
    Bottom,
    MovingUp
}
enum Direction
{
    Up,
    Down,
    Left,
    Right
}

namespace DrCanoli
{
    class Boss : Enemy
    {
        private Texture2D healthBar;
        private int maxHp;
        private int health;
        private Player player;
        private BossStates state;
        private int timer;
        //private List<Bullet> list;
        private Texture2D bulletTexture;
        public Boss(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, PhysManager phys, Texture2D shadow, Texture2D healthBar, Player player, Texture2D bulletTexture)
            : base(x, y, width, height, hp, dmg, animSet, phys, shadow)
        {
            this.healthBar = healthBar;
            maxHp = hp;
            this.health = maxHp;
            this.player = player;
            state = BossStates.Top;
            //list = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }
        /*
        public void UpdateBullets()
        {
            for(int c = 0; c < list.Count; c++)
            {
                if (list[c].Active)
                {
                    list[c].Update();
                    if (FighterState == FighterState.Jump)
                    {
                        if (list[c].Box.Intersects(new Rectangle(player.Box.X, player.Box.Y, player.Box.Width, player.Box.Height)))
                        {
                            player.Hp -= 10;
                            list[c].Active = false;
                            list.Remove(list[c]);
                            c--;
                        }
                    }
                    else
                    {
                        if(list[c].Box.Intersects(new Rectangle(player.Box.X, player.Box.Y + player.Box.Height - player.Box.Width / 8,
                        player.Box.Width, player.Box.Width / 4)))
                        {
                            player.Hp -= 10;
                            list[c].Active = false;
                            list.Remove(list[c]);
                            c--;
                        }
                    }
                }
            }
        }
        public void DrawBullets(SpriteBatch batch)
        {
            foreach (Bullet b in list)
            {
                if (b.Active)
                {
                    b.Draw(batch);
                }
            }
        }
        */
        public override void Update()
        {
                switch (state)
                {
                    case BossStates.Top:
                        if (timer >= 300)
                        {
                            timer = 0;
                            state = BossStates.MovingDown;
                        }
                        else
                        {
                        if (Math.Abs(Box.X - player.Box.X) < PhysManager.Unicorns * 16)
                        {
                            if (timer % 100 == 0)
                            {
                                Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Down));
                            }
                            else if (timer % 50 == 0)
                            {
                                if (player.Box.X > Box.X)
                                {
                                    Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Right));
                                }
                                else if (player.Box.X < Box.X)
                                {
                                    Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Left));
                                }
                            }
                        }
                            timer++;
                        }
                        break;
                    case BossStates.MovingDown:
                        Box = new Rectangle(Box.X, Box.Y + 1, Box.Width, Box.Height);
                        if (Box.Y >= (PhysManager.Unicorns * 9) - Box.Height)
                        {
                            Box = new Rectangle(Box.X, (PhysManager.Unicorns * 9) - Box.Height, Box.Width, Box.Height);
                            state = BossStates.Bottom;
                        }
                        break;
                    case BossStates.Bottom:
                        if (timer >= 300)
                        {
                            timer = 0;
                            state = BossStates.MovingUp;
                        }
                        else
                        {
                        if(Math.Abs(Box.X-player.Box.X) < PhysManager.Unicorns * 16)
                        {
                            if (timer % 100 == 0)
                            {
                                Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Up));
                            }
                            else if (timer % 50 == 0)
                            {
                                if (player.Box.X > Box.X)
                                {
                                    Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Right));
                                }
                                else if (player.Box.X < Box.X)
                                {
                                    Game1.AddEntity(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, PhysManager.Unicorns * 2, PhysManager.Unicorns), Direction.Left));
                                }
                            }
                        }
                            timer++;
                        }
                        break;
                    case BossStates.MovingUp:
                        Box = new Rectangle(Box.X, Box.Y - 1, Box.Width, Box.Height);
                        if (Box.Y <= 0)
                        {
                            Box = new Rectangle(Box.X, 0, Box.Width, Box.Height);
                            state = BossStates.Top;
                        }
                        break;
                    default:
                        break;
                }
            
        }
    }
}
