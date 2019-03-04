using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DrCanoli
{
	class Menu
	{
		private Texture2D startTexture;
		private Texture2D optionsTexture;	//textures for menu buttons
		private Texture2D exitTexture;

		private Rectangle startButton;
		private Rectangle optionsButton;	//positions for menu buttons
		private Rectangle exitButton;

		private MouseState mouseState = Mouse.GetState();
		private Color startColor;          //used for changing color of menu buttons when moused over
		private Color optionsColor;
		private Color exitColor;

		/// <summary>
		/// gives menu class textures and button positions
		/// </summary>
		/// <param name="startTexture">start button texture</param>
		/// <param name="optionsTexture">options button Texture</param>
		/// <param name="exitTexture">exit button Texture</param>
		/// <param name="startButton">start button Texture</param>
		/// <param name="optionsButton">options button Texture</param>
		/// <param name="exitButton">exit button Texture</param>
		public Menu(Texture2D startTexture, Texture2D optionsTexture, Texture2D exitTexture, Rectangle startButton, Rectangle optionsButton, Rectangle exitButton)
		{
			this.startTexture = startTexture;
			this.optionsTexture = optionsTexture;
			this.exitTexture = exitTexture;

			this.startButton = startButton;
			this.optionsButton = optionsButton;
			this.exitButton = exitButton;
		}

		/// <summary>
		/// draws menu buttons and changes button color if moused over
		/// </summary>
		/// <param name="spriteBatch">gives access to game1 spriteBatch</param>
		public void Draw(SpriteBatch spriteBatch)
		{
			mouseState = Mouse.GetState();

            //If I see code without curly braces like this was again i will throw the both of us off eastman
			if (startButton.Contains(mouseState.Position))
			{
				startColor = Color.Gray;
			}
            else
            {
                startColor = Color.White;
            }
            if (optionsButton.Contains(mouseState.Position))    //changes button color if moused over
            {
                optionsColor = Color.Gray;
            }
            else
            {
                optionsColor = Color.White;
            }
            if (exitButton.Contains(mouseState.Position))
            {
                exitColor = Color.Gray;
            }
            else
            {
                exitColor = Color.White;
            }

			spriteBatch.Draw(           //draws start button
				startTexture,
				startButton,
				startColor
				);

			spriteBatch.Draw(           //draws options button
				optionsTexture,
				optionsButton,
				optionsColor
				);

			spriteBatch.Draw(           //draws exit button
				exitTexture,
				exitButton,
				exitColor
				);
		}

		/// <summary>
		/// tells Game1 if start button has been clicked
		/// </summary>
		/// <returns>true if clicked, false otherwise</returns>
		public bool startClicked()		//tells game1 if start button has been clicked
		{
            //I fixed this for u
            return (startButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed);

            /*
			if (startButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            return false;
            */
		}

		/// <summary>
		/// tells Game1 if options button has been clicked
		/// </summary>
		/// <returns>true if clicked, false otherwise</returns>
		public bool optionsClicked()      //tells game1 if options button has been clicked
		{
            return (optionsButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed);

            /*
            if (optionsButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
			return false;
            */
        }

        /// <summary>
        /// tells Game1 if exit button has been clicked
        /// </summary>
        /// <returns>true if clicked, false otherwise</returns>
        public bool exitClicked()      //tells game1 if exit button has been clicked
		{
            return (exitButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed);

            /*
            if (exitButton.Contains(mouseState.Position) && mouseState.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
			return false;
            */
        }
    }
}
