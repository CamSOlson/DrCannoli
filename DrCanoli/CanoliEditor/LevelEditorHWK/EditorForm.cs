﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LevelEditorHWK
{
    // Map editor form
    public partial class EditorForm : Form
    {
        // Width, height, file dialogs, and tile color variables
        public int width;
        public int height;
        public int loadedWidth;
        public int loadedHeight;
        public Color tileColor;
        private SaveFileDialog saveFile;
        private OpenFileDialog loadFile;
        // Lists for picture boxes and their colors
        public List<PictureBox> boxList;
        public List<List<PictureBox>> totalList;
        public List<String> colorList;
        // Picture box variable
        private GroupBox box;
        // Panel variable
        private Panel panel;
        private bool saved = true;

        // Form constructor
        public EditorForm(int width, int height, string fileName = null)
        {
            // Set default tile color
            tileColor = Color.Lime;
            // Define list of picture boxes
            boxList = new List<PictureBox>();
            totalList = new List<List<PictureBox>>();

            InitializeComponent();
            panel = this.mapPanel;
            mapBox.Controls.Add(panel);

            // If a file is not loaded, create a blank level
            if(fileName == null)
            {
                // Set width and height based on parameters
                this.width = width;
                this.height = height;
                // Set title for editor window
                this.Text = "Level Editor - Untitled";
                // Generate picture boxes for map tiles
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Location = new Point((j * (500 / height)), (i * (500 / height)));
                        pictureBox.Size = new Size((500 / height), (500 / height));
                        pictureBox.BackColor = Color.Black;
                        panel.Controls.Add(pictureBox);
                        pictureBox.MouseClick += ChangeColor;
                        // Add picture box to list of boxes
                        boxList.Add(pictureBox);
                    }
                    totalList.Add(boxList);
                    boxList = new List<PictureBox>();
                }
            }
            // If a file is loaded, create it based on data from the text file
            else
            {
                LoadLevel(fileName);
            }
        }

        // Change the tile color and update the current color indicator
        private void greenButton_Click(object sender, EventArgs e)
        {
            if (sender is Button)
            {
                Button tileButton = (Button)sender;
                tileColor = tileButton.BackColor;
                currentTileButton.BackColor = tileColor;
            }
        }

        // When a picture box is clicked, change its color to the current color and set the saved boolean to false, since changes have been made
        public void ChangeColor(object sender, EventArgs e)
        {
            if (sender is PictureBox)
            {
                PictureBox tempBox = (PictureBox)sender;
                tempBox.BackColor = tileColor;
                if (saved)
                {
                    saved = false;
                    this.Text += " *";
                }
            }

        }

        // Save data
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Variable for file explorer
            saveFile = new SaveFileDialog();
            saveFile.Title = "Save a level file.";
            saveFile.Filter = "Text Files|*.txt";
            // Result of file explorer
            DialogResult dialogResult = saveFile.ShowDialog();

            // Write data to file if user presses save
            if (dialogResult == DialogResult.OK)
            {
                // Create variable for text file
                StreamWriter writer = null;
                try
                {
                    // Set variable to write text file
                    writer = new StreamWriter(saveFile.FileName);
                    // Write to the file
                    for(int i = 0; i < totalList.Count; i++)
                    {
                        foreach (PictureBox box in totalList[i])
                        {
                            if (box.BackColor == Color.Lime)
                            {
                                writer.Write('X');
                            }
                            if (box.BackColor == Color.Red)
                            {
                                writer.Write('E');
                            }
                            if (box.BackColor == Color.Black)
                            {
                                writer.Write('-');
                            }
                            if(box.BackColor == Color.Orange)
                            {
                                writer.Write('O');
                            }
                        }
                        if(i!= totalList.Count - 1)
                        {
                            writer.WriteLine();
                        }
                    }
                    // Close file
                    writer.Close();
                    // Give feedback if level saves successfully, and set the saved boolean to true
                    MessageBox.Show("Level saved successfully!");
                    saved = true;
                    // Update title of window
                    this.Text = "Level Editor - " + Path.GetFileName(saveFile.FileName);
                }
                catch (Exception f)
                {
                    // Catch any errors
                    MessageBox.Show("File error, " + f.Message);
                }
                finally
                {
                    if (writer != null)
                    {
                        // Close the stream
                        writer.Close();
                    }
                }
            }
            // If they press anything else, don't do anything
            else
            {

            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Variable for file explorer
            loadFile = new OpenFileDialog();
            loadFile.Title = "Open a level file.";
            loadFile.Filter = "Level Files|*.txt";
            // Result of file explorer
            DialogResult dialogResult = loadFile.ShowDialog();

            // Open new editor form and open map from desired file if user presses OK
            if (dialogResult == DialogResult.OK)
            {
                LoadLevel(loadFile.FileName);
            }
            // If they press anything else, don't do anything
            else
            {

            }
        }
        private void CloseForm(object sender, FormClosingEventArgs e)
        {
            // If the form is saved, close normally
            if (saved)
            {

            }
            // If the form is not saved, check if the user wants to save before closing
            else
            {
                DialogResult closeResult = MessageBox.Show("There are unsaved changes. Are you sure you want to quit?", "Unsaved Changes", MessageBoxButtons.YesNo);
                if(closeResult == DialogResult.No)
                {
                    // Cancel closing the form
                    e.Cancel = true;
                }
                // If the user selects yes, close the form as normal
                else
                {

                }
            }
        }

        private void LoadLevel(string fileName)
        {
            totalList.Clear();
            boxList.Clear();
            // Streams for file reading
            StreamReader reader = null;
            try
            {
                // Open designated file
                reader = new StreamReader(fileName);
                this.Text = "Level Editor - " + Path.GetFileName(fileName);
                // Read data from the file
                string lineOfText = null;

                lineOfText = reader.ReadLine();

                loadedWidth = lineOfText.Length;
                lineOfText = null;

                reader = new StreamReader(fileName);

                // Initialize stream reader, string variable, and char variable
                // Create positionList
                List<List<char>> positionList = new List<List<char>>();

                // Read in each line as a list of characters, then add this list to positionList
                while ((lineOfText = reader.ReadLine()) != null)
                {
                    List<char> row = new List<char>();
                    foreach (char ch in lineOfText)
                    {
                        row.Add(ch);
                    }
                    positionList.Add(row);
                }

                // Close the stream
                reader.Close();
                // Clear the group box
                mapBox.Controls.Clear();
                mapBox.Controls.Add(panel);
                // Generate picture boxes for map tiles
                for (int i = 0; i < positionList.Count; i++)
                {

                    for (int j = 0; j < positionList[i].Count; j++)
                    {
                        PictureBox pictureBox = new PictureBox();
                        pictureBox.Location = new Point((j * (500 / 6)), (i * (500 / 6)));
                        pictureBox.Size = new Size((500 / 6), (500 / 6));
                        if (positionList[i][j] == '-')
                        {
                            pictureBox.BackColor = Color.Black;
                        }
                        if (positionList[i][j] == 'X')
                        {
                            pictureBox.BackColor = Color.Lime;
                        }
                        if (positionList[i][j] == 'E')
                        {
                            pictureBox.BackColor = Color.Red;
                        }
                        if(positionList[i][j] == 'O')
                        {
                            pictureBox.BackColor = Color.Orange;
                        }
                        pictureBox.Visible = true;
                        panel.Controls.Add(pictureBox);
                        pictureBox.MouseClick += ChangeColor;
                        // Add the picture box to the list of picture boxes and increment the color count
                        boxList.Add(pictureBox);
                    }
                    totalList.Add(boxList);
                    boxList = new List<PictureBox>();
                }
                
                // Give feedback if level loads sucessfully
                MessageBox.Show("Level successfully loaded!");
            }
            catch (Exception f)
            {
                // Catch the exception
                Console.WriteLine("File error, " + f.Message);
            }
            finally
            {
                // Close the stream
                reader.Close();
            }
        }
    }
}
