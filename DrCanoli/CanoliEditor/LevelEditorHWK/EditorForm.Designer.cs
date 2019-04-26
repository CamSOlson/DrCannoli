namespace LevelEditorHWK
{
    partial class EditorForm
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
            this.tileBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.redButton = new System.Windows.Forms.Button();
            this.greyButton = new System.Windows.Forms.Button();
            this.greenButton = new System.Windows.Forms.Button();
            this.currentTileBox = new System.Windows.Forms.GroupBox();
            this.currentTileButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.mapPanel = new System.Windows.Forms.Panel();
            this.tileBox.SuspendLayout();
            this.currentTileBox.SuspendLayout();
            this.mapBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileBox
            // 
            this.tileBox.Controls.Add(this.label4);
            this.tileBox.Controls.Add(this.button2);
            this.tileBox.Controls.Add(this.label3);
            this.tileBox.Controls.Add(this.label2);
            this.tileBox.Controls.Add(this.label1);
            this.tileBox.Controls.Add(this.redButton);
            this.tileBox.Controls.Add(this.greyButton);
            this.tileBox.Controls.Add(this.greenButton);
            this.tileBox.Location = new System.Drawing.Point(26, 25);
            this.tileBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tileBox.Name = "tileBox";
            this.tileBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tileBox.Size = new System.Drawing.Size(240, 325);
            this.tileBox.TabIndex = 0;
            this.tileBox.TabStop = false;
            this.tileBox.Text = "Object Selector";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(130, 262);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 25);
            this.label4.TabIndex = 8;
            this.label4.Text = "Obstacle";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Orange;
            this.button2.Location = new System.Drawing.Point(134, 171);
            this.button2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 85);
            this.button2.TabIndex = 7;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 262);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 6;
            this.label3.Text = "Enemy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(144, 129);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 25);
            this.label2.TabIndex = 5;
            this.label2.Text = "Empty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 129);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Player";
            // 
            // redButton
            // 
            this.redButton.BackColor = System.Drawing.Color.Red;
            this.redButton.Location = new System.Drawing.Point(14, 171);
            this.redButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.redButton.Name = "redButton";
            this.redButton.Size = new System.Drawing.Size(88, 85);
            this.redButton.TabIndex = 3;
            this.redButton.UseVisualStyleBackColor = false;
            this.redButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // greyButton
            // 
            this.greyButton.BackColor = System.Drawing.Color.Black;
            this.greyButton.Location = new System.Drawing.Point(134, 38);
            this.greyButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.greyButton.Name = "greyButton";
            this.greyButton.Size = new System.Drawing.Size(88, 85);
            this.greyButton.TabIndex = 1;
            this.greyButton.UseVisualStyleBackColor = false;
            this.greyButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // greenButton
            // 
            this.greenButton.BackColor = System.Drawing.Color.Lime;
            this.greenButton.Location = new System.Drawing.Point(14, 38);
            this.greenButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.greenButton.Name = "greenButton";
            this.greenButton.Size = new System.Drawing.Size(88, 85);
            this.greenButton.TabIndex = 0;
            this.greenButton.UseVisualStyleBackColor = false;
            this.greenButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // currentTileBox
            // 
            this.currentTileBox.Controls.Add(this.currentTileButton);
            this.currentTileBox.Location = new System.Drawing.Point(40, 363);
            this.currentTileBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.currentTileBox.Name = "currentTileBox";
            this.currentTileBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.currentTileBox.Size = new System.Drawing.Size(226, 192);
            this.currentTileBox.TabIndex = 1;
            this.currentTileBox.TabStop = false;
            this.currentTileBox.Text = "Current Tile";
            // 
            // currentTileButton
            // 
            this.currentTileButton.BackColor = System.Drawing.Color.Lime;
            this.currentTileButton.Location = new System.Drawing.Point(34, 37);
            this.currentTileButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.currentTileButton.Name = "currentTileButton";
            this.currentTileButton.Size = new System.Drawing.Size(156, 142);
            this.currentTileButton.TabIndex = 0;
            this.currentTileButton.UseVisualStyleBackColor = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(74, 567);
            this.saveButton.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(150, 125);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 763);
            this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 133);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // mapBox
            // 
            this.mapBox.Controls.Add(this.mapPanel);
            this.mapBox.Location = new System.Drawing.Point(374, 25);
            this.mapBox.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.mapBox.Name = "mapBox";
            this.mapBox.Padding = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.mapBox.Size = new System.Drawing.Size(1000, 1022);
            this.mapBox.TabIndex = 6;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // mapPanel
            // 
            this.mapPanel.AutoScroll = true;
            this.mapPanel.Location = new System.Drawing.Point(9, 23);
            this.mapPanel.Name = "mapPanel";
            this.mapPanel.Size = new System.Drawing.Size(982, 990);
            this.mapPanel.TabIndex = 0;
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1931, 1062);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentTileBox);
            this.Controls.Add(this.tileBox);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "EditorForm";
            this.Text = "Editor Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.tileBox.ResumeLayout(false);
            this.tileBox.PerformLayout();
            this.currentTileBox.ResumeLayout(false);
            this.mapBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox tileBox;
        private System.Windows.Forms.Button redButton;
        private System.Windows.Forms.Button greyButton;
        private System.Windows.Forms.Button greenButton;
        private System.Windows.Forms.GroupBox currentTileBox;
        private System.Windows.Forms.Button currentTileButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox mapBox;
        private System.Windows.Forms.Panel mapPanel;
    }
}