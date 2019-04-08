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
        public Boss(int x, int y, int width, int height, AnimationSet animSet, int hp, int dmg, PhysManager phys, Texture2D shadow, Texture2D healthBar, Player player)
            : base(x, y, width, height, hp, dmg, animSet, phys, shadow)
        {
            this.healthBar = healthBar;
            maxHp = hp;
            this.health = maxHp;
            this.player = player;
            state = BossStates.Top;
        }

        public void DrawHealthbar(SpriteBatch batch)
        {
            //The rectangle values are just temporary, draws a boss' healthbar
            batch.Draw(healthBar, new Rectangle(0, 50, 600, 50), Color.White);
            batch.Draw(healthBar, new Rectangle(0, 50, (health / maxHp) * 600, 50), Color.Red);
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
