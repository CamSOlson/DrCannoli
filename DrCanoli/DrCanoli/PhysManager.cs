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
    class PhysManager
    {
        //if this manages enemies as well, we shoud add a list for enemy references here
        private List<Enemy> enemyList;
        //any map obstacles should go here 
        private List<Obstacle> obstacles;
        //we could also give this a reference to the player
        private Player player;
        private Boss boss;
        private static int unicorns; //standard unit for window height
        private double frameSeconds;
        private static double acceleration; //time between frames, acceleration in unicorns per frameSeconds squared
        private const double JUMPUNICORNS = 2;

        //I needed to add this here so I could use it in my animations <3 -Love, Cam XOXOXOXO
        public static int Unicorns
        {
            get { return unicorns; }
        }

        public Boss Boss
        {
            get { return boss; }
            set { boss = value; }
        }

        public PhysManager(Player player, List<Enemy> enemies, List<Obstacle> obs, int screenHeight, Boss boss)
        {
            this.player = player;
            obstacles = obs;
            enemyList = enemies;
            unicorns = screenHeight / 9;
            frameSeconds = 1 / 60; //if the framerate isn't excatly 60 we should update this
            acceleration = -.981 * 2; //treating a meter as 2 unicorns and frameSeconds being the time between frames in seconds
            this.boss = boss;
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

            //check weapons with boss
            if (player.Wep.Swinging && boss.Hitbox.Intersects(player.Wep.Box))
            {
                if (!boss.Invulnerable)
                    Hit(player, boss);
            }
            if (player.Hitbox.Intersects(boss.Hitbox) && player.FighterState != FighterState.Jump)
            {
                Rectangle intersect = Rectangle.Intersect(player.Hitbox, boss.Hitbox);
                if (player.Hitbox.X < boss.Hitbox.X && intersect.Height > intersect.Width)
                {
                    player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                }
                else if (intersect.Height > intersect.Width)
                {
                    player.Box = new Rectangle(player.Box.X + intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                }
                else if (player.Hitbox.Y < boss.Hitbox.Y && intersect.Height < intersect.Width)
                {
                    player.Box = new Rectangle(player.Box.X, player.Box.Y - intersect.Height, player.Box.Width, player.Box.Height);
                }
                else if (intersect.Height < intersect.Width)
                {
                    player.Box = new Rectangle(player.Box.X, player.Box.Y + intersect.Height, player.Box.Width, player.Box.Height);
                }
            }

            //CHECK ENEMY COLLISIONS WITH PLAYER, also calls Hit() with proper entities
            foreach (Enemy e in enemyList)
            {
                if (e.Hitbox.Intersects(player.Hitbox) && e.Active)
                {
                    Rectangle intersect = Rectangle.Intersect(e.Hitbox, player.Hitbox);


                    if (intersect.Width < intersect.Height)
                    {
                        //enemy is coming from sides
                        if (!player.Invulnerable)
                            Hit(e, player);

                        if (player.Box.X < e.Box.X && player.FighterState != FighterState.Jump)
                        {
                            //From left
                            e.Box = new Rectangle(e.Box.X + intersect.Width, e.Box.Y, e.Box.Width, e.Box.Height);
                        }
                        else if (player.FighterState != FighterState.Jump)
                        {
                            //From right
                            e.Box = new Rectangle(e.Box.X - intersect.Width, e.Box.Y, e.Box.Width, e.Box.Height);
                        }
                    }
                    else
                    {
                        Rectangle newBox = e.Box;
                        //Enemy is coming from top/bottom
                        if (player.Box.Y < e.Box.Y && player.FighterState != FighterState.Jump)
                        {
                            //From bottom
                            newBox = new Rectangle(e.Box.X, e.Box.Y + intersect.Height, e.Box.Width, e.Box.Height);
                        }
                        else if (player.FighterState != FighterState.Jump)
                        {
                            //From top
                            newBox = new Rectangle(e.Box.X, e.Box.Y - intersect.Height, e.Box.Width, e.Box.Height);
                        }

                        //Move to start finding place to hit
                        if (player.Box.X < e.Box.X)
                        {
                            //Right side of player is closer, begin moving to the right
                            newBox.X += (int)Math.Round(60d / e.Speed / 3d);
                            e.FighterState = FighterState.Move;
                        }
                        else
                        {
                            //Left side of player is closer, begin moving to the left
                            newBox.X -= (int)Math.Round(60d / e.Speed / 3d);
                            e.FighterState = FighterState.Move;
                        }

                        e.Box = newBox;

                    }

                }
            }

            //CHECK AND ADJUST COLLISONS THAT DON'T PERMIT MOVEMENT
            foreach (Obstacle o in obstacles)
            {
                if (player.FighterState != FighterState.Jump && o.Box.Intersects(player.Hitbox))
                {
                    Rectangle intersect = Rectangle.Intersect(o.Box, player.Hitbox);
                    //in the rare case intersect.Height = intersect.Width
                    if (player.Hitbox.X < o.Box.X && player.Hitbox.Y < o.Box.Y && intersect.Height == intersect.Width)
                        player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y - intersect.Width, player.Box.Width, player.Box.Height);
                    else if (player.Hitbox.X > o.Box.X && player.Hitbox.Y < o.Box.Y && intersect.Height == intersect.Width)
                        player.Box = new Rectangle(player.Box.X + intersect.Width, player.Box.Y - intersect.Width, player.Box.Width, player.Box.Height);
                    else if (player.Hitbox.X < o.Box.X && player.Hitbox.Y > o.Box.Y && intersect.Height == intersect.Width)
                        player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y + intersect.Width, player.Box.Width, player.Box.Height);
                    else if (player.Hitbox.X > o.Box.X && player.Hitbox.Y > o.Box.Y && intersect.Height == intersect.Width)
                        player.Box = new Rectangle(player.Box.X + intersect.Width, player.Box.Y + intersect.Width, player.Box.Width, player.Box.Height);

                    //grounded object collisions 2.0
                    else if (player.Hitbox.X < o.Box.X && intersect.Height > intersect.Width)
                    {
                        player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                    }
                    else if (intersect.Height > intersect.Width)
                    {
                        player.Box = new Rectangle(player.Box.X + intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                    }
                    else if (player.Hitbox.Y < o.Box.Y && intersect.Height < intersect.Width)
                    {
                        player.Box = new Rectangle(player.Box.X, player.Box.Y - intersect.Height, player.Box.Width, player.Box.Height);
                        player.SuspendedJump = true;
                    }
                    else if (intersect.Height < intersect.Width)
                    {
                        player.Box = new Rectangle(player.Box.X, player.Box.Y + intersect.Height, player.Box.Width, player.Box.Height);
                    }
                }
                else if (player.FighterState == FighterState.Jump && player.DestinationRectangle.Intersects(o.Box) && player.Hitbox.X < o.Box.X && player.VelocityY <= 0)
                {
                    Rectangle intersect = Rectangle.Intersect(o.Box, player.Hitbox);
                    player.Box = new Rectangle(player.Box.X - intersect.Width, player.Box.Y, player.Box.Width, player.Box.Height);
                    player.SuspendedJump = true;
                }
            }
        }

        public void Hit(Fighter Hitter, Fighter Target)
        {
            //If a hit lands on anyone, this method will be called
            Target.Hp -= Hitter.Dmg;
            if (Hitter.Box.X < Target.Box.X)
                Target.FacingRight = false;
            else
                Target.FacingRight = true;
			if (Hitter is Player)
			{
				Player player0 = (Player)Hitter;
				Target.Hp -= player0.Wep.Damage;
			}

            if (Target.Hp > 0 && !(Target is Boss))
            {
                Knockback(Target);
            }
            else if (Target.Hp > 0)
            {
                Target.Invulnerable = true;
                Target.InvulnTime = 1;
            }
            else
            {
                if(Target is Player)
                {
                    Player player = (Player)Target;
                    player.Alive = false;
                }
                else if (Target is Enemy)
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
        public static void Knockback(Fighter wasHit)
        {
            //will override jumping state if hit midair, so this can use the same fields for moving back as jumping
            //also will simply calculate new position based on stored values and updates said values
            //calling this should set the fighter's state to jumping

            //set player state machine to jumping
            if (wasHit.FighterState != FighterState.Jump)
            {
                wasHit.InitialY = wasHit.Box.Y;
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
