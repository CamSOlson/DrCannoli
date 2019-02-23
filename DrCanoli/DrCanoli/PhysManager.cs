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
        //private List<> obstacles;
        //we could also give this a reference to the player
        private Player player;
        //weapon reference too
        //private Weapon currentWeapon;
        private int unicorns; //standard unit for window height
        private double frameSeconds, acceleration; //time between frames, acceleration in unicorns per frameSeconds squared
        private const double JUMPUNICORNS = 2;
        private double elapsedTime; //every frame we call gameTime.ElapsedGameTime.TotalSeconds in game1 and call the property here

        public PhysManager(Player player, List<Enemy> enemies, int screenHeight)
        {
            this.player = player;
            enemyList = enemies;
            unicorns = screenHeight / 9;
            frameSeconds = 1 / 60; //if the framerate isn't excatly 60 we should update this
            acceleration = -9.81 * 2 * Math.Pow(frameSeconds, 2); //treating a meter as 2 unicorns and frameSeconds being the time between frames in seconds
            elapsedTime = 0;
        }

        private void CheckCollisions()
        {
            //checks what hitboxes are touching simply for stopping movement, not damage
        }

        private int IsHit()
        {
            //checks if the player is hit by an enemy and returns damage from enemy
            Knockback(player);
            return 0;
        }

        private int EnemyHit()
        {
            //checks if any enemy has been hit by the player's weapon and returns damage
            //assuming we use a foreach loop
            foreach (Enemy e in enemyList)
            {
                Knockback(e);
            }
            return 0;
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
            int changeY = (int)((Math.Pow(player.VelocityY + acceleration * elapsedTime, 2) - Math.Pow(player.VelocityY, 2)) / 2 * acceleration);
            player.Box = new Rectangle(player.Box.X, player.Box.Y + changeY, player.Box.Width, player.Box.Height);
            if (player.Box.Y <= player.InitialY)
            {
                player.Box = new Rectangle(player.Box.X, player.InitialY, player.Box.Width, player.Box.Height);
                return true;
            }
            return false;
        }

        private void Knockback(Fighter wasHit)
        {
            //will override jumping state if hit midair, so this can use the same fields for moving back as jumping
            //also will simply calculate new position based on stored values and updates said values
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
    }
}
