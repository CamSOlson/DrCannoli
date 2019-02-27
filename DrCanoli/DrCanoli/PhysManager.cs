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
        //weapon reference too
        //private Weapon currentWeapon;
        private int unicorns; //standard unit for window height
        private double frameSeconds, acceleration; //time between frames, acceleration in unicorns per frameSeconds squared
        private const double JUMPUNICORNS = 2;
        private double elapsedTime; //every frame we call gameTime.ElapsedGameTime.TotalSeconds in game1 and call the property here
        private int offset;

        public PhysManager(Player player, List<Enemy> enemies, List<Obstacle> obs, int screenHeight)
        {
            this.player = player;
            obstacles = obs;
            enemyList = enemies;
            unicorns = screenHeight / 9;
            frameSeconds = 1 / 60; //if the framerate isn't excatly 60 we should update this
            acceleration = -9.81 * 2 * Math.Pow(frameSeconds, 2); //treating a meter as 2 unicorns and frameSeconds being the time between frames in seconds
            elapsedTime = 0;
            offset = player.Box.X;
        }

        private void CheckCollisions()
        {
            //CHECK WEAPON COLLISIONS WITH ENEMIES, calls Hit() with proper entities

            //CHECK ENEMY COLLISIONS WITH PLAYER, also calls Hit() with proper entities
            foreach (Enemy e in enemyList)
            {
                if (e.Box.Intersects(player.Box))
                {
                    if (!player.Stunned)
                        Hit(e, player);
                }
            }

            //CHECK AND ADJUST COLLISONS THAT DON'T PERMIT MOVEMENT
            foreach (Obstacle o in obstacles)
            {
                if (o.Box.Intersects(player.Box))
                {
                    Rectangle intersect = Rectangle.Intersect(o.Box, player.Box);
                    if (player.Box.X < o.Box.X)
                        offset -= intersect.Width;
                    else
                        offset += intersect.Width;
                    if (player.Box.Y < o.Box.Y)
                        player.Box = new Rectangle(player.Box.X, player.Box.Y - intersect.Height, player.Box.Width, player.Box.Height);
                    else
                        player.Box = new Rectangle(player.Box.X, player.Box.Y + intersect.Height, player.Box.Width, player.Box.Height);
                }
            }
        }

        public void Hit(Fighter Hitter, Fighter Target)
        {
            //If a hit lands on anyone, this method will be called
            Target.Hp -= Hitter.Dmg;

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
        private bool Jump(Fighter jumper)
        {
            //the player should have an double (probably 2 for x and y components?) for velocity and the Y coord they jumped from to properly track jumping and an int to track how much time they have been jumping for
            //this method will simply calculate and change their coordinates based on the stored values (and updates with new values)
            //we could also store them here but then we'd need it for every player and enemy so it makes more sense to have them store it
            int changeY = (int)((Math.Pow(jumper.VelocityY + acceleration * elapsedTime, 2) - Math.Pow(jumper.VelocityY, 2)) / 2 * acceleration);
            jumper.Box = new Rectangle(jumper.Box.X, jumper.Box.Y + changeY, jumper.Box.Width, jumper.Box.Height);
            jumper.VelocityY += acceleration * elapsedTime;
            if (player.Stunned)
            {
                jumper.StunTime -= elapsedTime;
                if (jumper.StunTime <= 0)
                    player.Stunned = false;
            }
            if (jumper.Box.Y <= jumper.InitialY)
            {
                jumper.Box = new Rectangle(jumper.Box.X, jumper.InitialY, jumper.Box.Width, jumper.Box.Height);
                jumper.Stunned = false;
                return true;
            }
            return false;
        }

        //called once when hit, sets washit's state machine to jumping (which sets an initial y and passes the player an ititial y velocity
        private void Knockback(Fighter wasHit)
        {
            //will override jumping state if hit midair, so this can use the same fields for moving back as jumping
            //also will simply calculate new position based on stored values and updates said values
            //calling this should set the fighter's state to jumping

            //set player state machine to jumping
            if (Math.Abs(wasHit.VelocityX) < 1) //horizontal move speed
            {
                if (wasHit.VelocityX >= 0)
                    wasHit.VelocityX = -1;
                else
                    wasHit.VelocityX = 1;
            }
            else
                wasHit.VelocityX = -wasHit.VelocityX;
            wasHit.VelocityY /= 2;
            wasHit.Stunned = true;
            wasHit.StunTime = 0.2;
        }

        //players and enemies should have a method for dying, this manager should just detract health
        //the player should also have a state for running, jumping, attacking, stunned, etc.

        public double InitialYVelocity
        {
            get
            {
                return Math.Sqrt(-2 * acceleration * unicorns * JUMPUNICORNS);
            }
        }

        public double ElapsedTime
        {
            get { return elapsedTime; }
            set { elapsedTime = value; }
        }

        public int Offset
        {
            get { return offset; }
            set { offset = value; }
        }
    }
}
