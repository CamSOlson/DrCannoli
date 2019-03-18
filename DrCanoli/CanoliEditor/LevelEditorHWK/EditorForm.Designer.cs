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
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.redButton = new System.Windows.Forms.Button();
            this.greyButton = new System.Windows.Forms.Button();
            this.greenButton = new System.Windows.Forms.Button();
            this.currentTileBox = new System.Windows.Forms.GroupBox();
            this.currentTileButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.mapBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tileBox.SuspendLayout();
            this.currentTileBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tileBox
            // 
            this.tileBox.Controls.Add(this.label3);
            this.tileBox.Controls.Add(this.label2);
            this.tileBox.Controls.Add(this.label1);
            this.tileBox.Controls.Add(this.redButton);
            this.tileBox.Controls.Add(this.greyButton);
            this.tileBox.Controls.Add(this.greenButton);
            this.tileBox.Location = new System.Drawing.Point(13, 13);
            this.tileBox.Name = "tileBox";
            this.tileBox.Size = new System.Drawing.Size(120, 169);
            this.tileBox.TabIndex = 0;
            this.tileBox.TabStop = false;
            this.tileBox.Text = "Object Selector";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 136);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Enemy";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 67);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Empty";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 67);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Player";
            // 
            // redButton
            // 
            this.redButton.BackColor = System.Drawing.Color.Red;
            this.redButton.Location = new System.Drawing.Point(7, 89);
            this.redButton.Name = "redButton";
            this.redButton.Size = new System.Drawing.Size(44, 44);
            this.redButton.TabIndex = 3;
            this.redButton.UseVisualStyleBackColor = false;
            this.redButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // greyButton
            // 
            this.greyButton.BackColor = System.Drawing.Color.Black;
            this.greyButton.Location = new System.Drawing.Point(67, 20);
            this.greyButton.Name = "greyButton";
            this.greyButton.Size = new System.Drawing.Size(44, 44);
            this.greyButton.TabIndex = 1;
            this.greyButton.UseVisualStyleBackColor = false;
            this.greyButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // greenButton
            // 
            this.greenButton.BackColor = System.Drawing.Color.Lime;
            this.greenButton.Location = new System.Drawing.Point(7, 20);
            this.greenButton.Name = "greenButton";
            this.greenButton.Size = new System.Drawing.Size(44, 44);
            this.greenButton.TabIndex = 0;
            this.greenButton.UseVisualStyleBackColor = false;
            this.greenButton.Click += new System.EventHandler(this.greenButton_Click);
            // 
            // currentTileBox
            // 
            this.currentTileBox.Controls.Add(this.currentTileButton);
            this.currentTileBox.Location = new System.Drawing.Point(20, 189);
            this.currentTileBox.Name = "currentTileBox";
            this.currentTileBox.Size = new System.Drawing.Size(113, 100);
            this.currentTileBox.TabIndex = 1;
            this.currentTileBox.TabStop = false;
            this.currentTileBox.Text = "Current Tile";
            // 
            // currentTileButton
            // 
            this.currentTileButton.BackColor = System.Drawing.Color.Lime;
            this.currentTileButton.Location = new System.Drawing.Point(17, 19);
            this.currentTileButton.Name = "currentTileButton";
            this.currentTileButton.Size = new System.Drawing.Size(78, 74);
            this.currentTileButton.TabIndex = 0;
            this.currentTileButton.UseVisualStyleBackColor = false;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(37, 295);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 65);
            this.saveButton.TabIndex = 2;
            this.saveButton.Text = "Save File";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // mapBox
            // 
            this.mapBox.Location = new System.Drawing.Point(210, 13);
            this.mapBox.Name = "mapBox";
            this.mapBox.Size = new System.Drawing.Size(500, 500);
            this.mapBox.TabIndex = 4;
            this.mapBox.TabStop = false;
            this.mapBox.Text = "Map";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 397);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 69);
            this.button1.TabIndex = 5;
            this.button1.Text = "Load File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 552);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mapBox);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.currentTileBox);
            this.Controls.Add(this.tileBox);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CloseForm);
            this.tileBox.ResumeLayout(false);
            this.tileBox.PerformLayout();
            this.currentTileBox.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox mapBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}