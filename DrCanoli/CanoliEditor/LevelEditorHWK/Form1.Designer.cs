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
            this.dimensionBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // dimensionBox
            // 
            this.dimensionBox.Controls.Add(this.createButton);
            this.dimensionBox.Controls.Add(this.widthBox);
            this.dimensionBox.Controls.Add(this.widthLabel);
            this.dimensionBox.Location = new System.Drawing.Point(98, 350);
            this.dimensionBox.Name = "dimensionBox";
            this.dimensionBox.Size = new System.Drawing.Size(490, 272);
            this.dimensionBox.TabIndex = 1;
            this.dimensionBox.TabStop = false;
            this.dimensionBox.Text = "Create New Map";
            // 
            // createButton
            // 
            this.createButton.Location = new System.Drawing.Point(64, 151);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(364, 121);
            this.createButton.TabIndex = 4;
            this.createButton.Text = "Create Map";
            this.createButton.UseVisualStyleBackColor = true;
            this.createButton.Click += new System.EventHandler(this.createButton_Click);
            // 
            // widthBox
            // 
            this.widthBox.Location = new System.Drawing.Point(219, 73);
            this.widthBox.Name = "widthBox";
            this.widthBox.Size = new System.Drawing.Size(100, 31);
            this.widthBox.TabIndex = 2;
            this.widthBox.Text = "20";
            // 
            // widthLabel
            // 
            this.widthLabel.AutoSize = true;
            this.widthLabel.Location = new System.Drawing.Point(23, 73);
            this.widthLabel.Name = "widthLabel";
            this.widthLabel.Size = new System.Drawing.Size(192, 25);
            this.widthLabel.TabIndex = 0;
            this.widthLabel.Text = "Width (in unicorns)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 793);
            this.Controls.Add(this.dimensionBox);
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
    }
}

