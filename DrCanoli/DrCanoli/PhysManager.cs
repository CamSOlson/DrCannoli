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
    class PhysManager
    {
        //if this manages enemies as well, we shoud add a list for enemy references here
        private List<Enemy> enemyList;
        //any map obstacles should go here 
        private List<Obstacle> obstacles;
        //we could also give this a reference to the player
        private Player player;
        private static int unicorns; //standard unit for window height
        private double frameSeconds;
        private static double acceleration; //time between frames, acceleration in unicorns per frameSeconds squared
        private const double JUMPUNICORNS = 2;

        //I needed to add this here so I could use it in my animations <3 -Love, Cam XOXOXOXO
        public static int Unicorns
        {
            get { return unicorns; }
        }

        public PhysManager(Player player, List<Enemy> enemies, List<Obstacle> obs, int screenHeight)
        {
            this.player = player;
            obstacles = obs;
            enemyList = enemies;
            unicorns = screenHeight / 9;
            frameSeconds = 1 / 60; //if the framerate isn't excatly 60 we should update this
            acceleration = -.981 * 2; //treating a meter as 2 unicorns and frameSeconds being the time between frames in seconds
        }

        public void CheckCollisions()
        {
            //CHECK WEAPON COLLISIONS WITH ENEMIES, calls Hit() with proper entities
            foreach (Enemy e in enemyList)
            {
                if (player.Wep.Swinging && e.Hitbox.Intersects(player.Wep.Box) && e.Active)
                {
                    if (!e.Stunned)
                        Hit(player, e);
                }
            }

            //CHECK ENEMY COLLISIONS WITH PLAYER, also calls Hit() with proper entities
            foreach (Enemy e in enemyList)
            {
                if (e.Hitbox.Intersects(player.Hitbox) && e.Active)
                {
                    if (!player.Invulnerable)
                        Hit(e, player);
                    Rectangle intersect = Rectangle.Intersect(e.Hitbox, player.Hitbox);
                    if (player.Box.X < e.Box.X)
                        e.Box = new Rectangle(e.Box.X + intersect.Width, e.Box.Y, e.Box.Width, e.Box.Height);
                    else
                        e.Box = new Rectangle(e.Box.X - intersect.Width, e.Box.Y, e.Box.Width, e.Box.Height);
                }
            }

            //CHECK AND ADJUST COLLISONS THAT DON'T PERMIT MOVEMENT
            foreach (Obstacle o in obstacles)
            {
                if (o.Box.Intersects(player.Hitbox))
                {
                    Rectangle intersect = Rectangle.Intersect(o.Box, player.Hitbox);
                    if (player.Box.X < o.Box.X && player.KBState.IsKeyDown(Keys.D))
                        player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                    else if (player.KBState.IsKeyDown(Keys.A))
                        player.Box = new Rectangle(player.Box.X + intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                    else if (player.Box.Y < o.Box.Y && player.KBState.IsKeyDown(Keys.S))
                        player.Box = new Rectangle(player.Box.X, player.Box.Y - intersect.Height, player.Box.Width, player.Box.Height);
                    else if (player.KBState.IsKeyDown(Keys.W))
                        player.Box = new Rectangle(player.Box.X, player.Box.Y + intersect.Height, player.Box.Width, player.Box.Height);
                }
            }
        }

        public void Hit(Fighter Hitter, Fighter Target)
        {
            //If a hit lands on anyone, this method will be called
            Target.Hp -= Hitter.Dmg;
			if (Hitter is Player)
			{
				Player player0 = (Player)Hitter;
				Target.Hp -= player0.Wep.Damage;
			}

            if (Target.Hp > 0)
            {
                Knockback(Target);
            }
            else
            {
                if(Target is Player)
                {
                    Player player = (Player)Target;
                    player.Alive = false;
                }
                else
                {
                    Enemy enemy = (Enemy)Target;
                    enemy.Active = false;
                }
            }

        }
        /// <summary>
        /// calculates the change in the juping fighter's height based on horizontal velocity and constructed acceleration, applies changes
        /// </summary>
        /// <param name="jumper">the fighter who is jumping</param>
        /// <returns>true if the fighter is now done jumping, false if not</returns>
        public bool Jump(Fighter jumper)
        {
            //the player should have an double (probably 2 for x and y components?) for velocity and the Y coord they jumped from to properly track jumping and an int to track how much time they have been jumping for
            //this method will simply calculate and change their coordinates based on the stored values (and updates with new values)
            //we could also store them here but then we'd need it for every player and enemy so it makes more sense to have them store it

            /*
            int changeY = (int)((Math.Pow(jumper.VelocityY + acceleration * elapsedTime, 2) - Math.Pow(jumper.VelocityY, 2)) / 2 * acceleration);
            jumper.Box = new Rectangle(jumper.Box.X, jumper.Box.Y - changeY, jumper.Box.Width, jumper.Box.Height);
            jumper.VelocityY += acceleration * elapsedTime;
            */

            jumper.Box = new Rectangle(jumper.Box.X, jumper.Box.Y - (int)jumper.VelocityY, jumper.Box.Width, jumper.Box.Height);
            if (jumper.VelocityY > 2)
                jumper.VelocityY -= jumper.VelocityY / 8;
            else
                jumper.VelocityY -= 1;
            if (jumper is Player && jumper.Invulnerable)
            {
                jumper.InvulnTime -= Game1.ElapsedTime;
                if (jumper.InvulnTime <= 0)
                {
                    jumper.Invulnerable = false;
                    //jumper.Color = Color.White;
                }
            }
            if (jumper.Box.Y >= jumper.InitialY)
            {
                jumper.Box = new Rectangle(jumper.Box.X, jumper.InitialY, jumper.Box.Width, jumper.Box.Height);
                //if (jumper.Stunned)
                 //   jumper.Color = Color.White;
                jumper.Stunned = false;
                return true;
            }
            return false;
        }

        public int DownShift(Fighter jumper)
        {
            jumper.Box = new Rectangle(jumper.Box.X, jumper.Box.Y + 1, jumper.Box.Width, jumper.Box.Height);
            return jumper.Box.Y;
        }

        //called once when hit, sets washit's state machine to jumping (which sets an initial y and passes the player an ititial y velocity
        private void Knockback(Fighter wasHit)
        {
            //will override jumping state if hit midair, so this can use the same fields for moving back as jumping
            //also will simply calculate new position based on stored values and updates said values
            //calling this should set the fighter's state to jumping

            //set player state machine to jumping
            if (wasHit.FighterState != FighterState.Jump)
            {
                wasHit.InitialY = player.Box.Y;
                wasHit.VelocityY = InitialYVelocity;
                wasHit.FighterState = FighterState.Jump;
            }
            wasHit.VelocityY /= 2;
            wasHit.Stunned = true;
            //wasHit.Color = Color.Yellow; //colors for testing
            if (wasHit is Player)
            {
                wasHit.Invulnerable = true;
                wasHit.InvulnTime = 1;
            }
        }

        //players and enemies should have a method for dying, this manager should just detract health
        //the player should also have a state for running, jumping, attacking, stunned, etc.

        public static double InitialYVelocity
        {
            get
            {
                return Math.Sqrt(-2 * acceleration * unicorns * JUMPUNICORNS);
            }
        }
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }

        public void UpdateClosest()
        {
            double smallDist = int.MaxValue;
            int ind = -1;
            foreach (Enemy e in enemyList)
                e.Closest = false;
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i].DistanceTo(player) < smallDist && enemyList[i].Active)
                {
                    smallDist = enemyList[i].DistanceTo(player);
                    ind = i;
                }
            }
            if (ind != -1)
                enemyList[ind].Closest = true;
        }
    }
}
