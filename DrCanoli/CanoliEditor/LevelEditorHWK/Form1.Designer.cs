namespace LevelEditorHWK
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dimensionBox = new System.Windows.Forms.GroupBox();
            this.createButton = new System.Windows.Forms.Button();
            this.widthBox = new System.Windows.Forms.TextBox();
            this.widthLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dimensionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dimensionBox
            // 
            this.dimensionBox.Controls.Add(this.button1);
            this.dimensionBox.Controls.Add(this.createButton);
            this.dimensionBox.Controls.Add(this.widthBox);
            this.dimensionBox.Controls.Add(this.widthLabel);
            this.dimensionBox.Location = new System.Drawing.Point(49, 182);
            this.dimensionBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dimensionBox.Name = "dimensionBox";
            this.dimensionBox.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dimensionBox.Size = new System.Drawing.Size(245, 219);
            this.dimensionBox.TabIndex = 1;
            this.dimensionBox.TabStop = false;
            this.dimensionBox.Text = "Create New Map";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(32, 79);
            this.createButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(182, 63);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(110, 38);
            this.widthBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(52, 20);
            this.widthBox.TabIndex = 2;
            this.widthBox.Text = "20";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(12, 38);
            this.widthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(95, 13);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width (in unicorns)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(32, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(182, 57);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load Map";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(356, 412);
            this.Controls.Add(this.dimensionBox);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.dimensionBox.ResumeLayout(false);
            this.dimensionBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox dimensionBox;
        private System.Windows.Forms.Button createButton;
        private System.Windows.Forms.TextBox widthBox;
        private System.Windows.Forms.Label widthLabel;
        private System.Windows.Forms.Button button1;
    }
}

