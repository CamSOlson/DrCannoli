using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private List<Bullet> list;
        private Texture2D bulletTexture;
        public Boss(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, PhysManager phys, Texture2D shadow, Texture2D healthBar, Player player, Texture2D bulletTexture)
            : base(x, y, width, height, hp, dmg, animSet, phys, shadow)
        {
            this.healthBar = healthBar;
            maxHp = hp;
            this.health = maxHp;
            this.player = player;
            state = BossStates.Top;
            list = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        public void DrawHealthbar(SpriteBatch batch)
        {
            //The rectangle values are just temporary, draws a boss' healthbar
            batch.Draw(healthBar, new Rectangle(0, 50, 600, 50), Color.White);
            batch.Draw(healthBar, new Rectangle(0, 50, (health / maxHp) * 600, 50), Color.Red);
        }
        public void UpdateBullets(SpriteBatch batch)
        {
            foreach(Bullet b in list)
            {
                if (b.Active)
                {
                    b.Update();
                    b.Draw(batch);
                    if (b.Rect.Intersects(player.Box))
                    {
                        player.Hp -= 10;
                        b.Active = false;
                    }
                }
            }
        }
        public override void Update()
        {
            switch (state)
            {
                case BossStates.Top:
                    if(timer >= 300)
                    {
                        timer = 0;
                        state = BossStates.MovingDown;
                    }
                    else
                    {
                        if(timer % 30 == 0)
                        {
                            list.Add(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, 25, 25), Direction.Down));
                        }
                        else if(timer % 15 == 0)
                        {
                            if(player.Box.X > Box.X)
                            {
                                list.Add(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, 25, 25), Direction.Right));
                            }
                            else if(player.Box.X < Box.X)
                            {
                                list.Add(new Bullet(bulletTexture, new Rectangle(Box.X, Box.Y, 25, 25), Direction.Left));
                            }
                        }
                        timer++;
                    }
                    break;
                case BossStates.MovingDown:

                    break;
                case BossStates.Bottom:

                    break;
                case BossStates.MovingUp:

                    break;
                default:
                    break;
            }
        }
    }
}
