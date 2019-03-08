using System;
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
    public partial class Form1 : Form
    {
        // Variables for file dialog, editor form, and width and height of editor form
        private EditorForm editorForm;
        private OpenFileDialog openFile;
        private int width;
        private int height;

        // Form constructor
        public Form1()
        {
            InitializeComponent();
        }

        // Create new map
        private void createButton_Click(object sender, EventArgs e)
        {
            // Width and height of map
            width = int.Parse(widthBox.Text);
            height = 6;
            // List of errors
            List<string> errorList = new List<string>();
            // Check which errors exist before opening a new map
            if(width < 10)
            {
                errorList.Add("Width too small. Minimum is 10");
            }
            // Print the errors in a message box
            if(width < 10)
            {
                for(int i = 0; i < errorList.Count; i++)
                {
                    MessageBox.Show(errorList[i]);
                }
            }
            // Open the editor form
            else
            {
                editorForm = new EditorForm(width, height);
                editorForm.ShowDialog();
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            // Width and height of map
            width = int.Parse(widthBox.Text);
            height = 6;

            // Variable for file explorer
            openFile = new OpenFileDialog();
            openFile.Title = "Open a level file.";
            openFile.Filter = "Level Files|*.txt";
            // Result of file explorer
            DialogResult dialogResult = openFile.ShowDialog();

            // Open new editor form and open map from desired file if user presses OK
            if (dialogResult == DialogResult.OK)
            {
                editorForm = new EditorForm(width, height, openFile.FileName);
                editorForm.ShowDialog();
            }
            // If they press anything else, don't do anything
            else
            {
                
            }
        }
    }
}
