namespace KristinaLall_Assignment4_Graphics
{
    partial class MazeGame
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
            this.components = new System.ComponentModel.Container();
            this.timer_myTimer = new System.Windows.Forms.Timer(this.components);
            this.label_WinMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer_myTimer
            // 
            this.timer_myTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_WinMessage
            // 
            this.label_WinMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label_WinMessage.AutoSize = true;
            this.label_WinMessage.BackColor = System.Drawing.Color.Black;
            this.label_WinMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_WinMessage.ForeColor = System.Drawing.Color.White;
            this.label_WinMessage.Location = new System.Drawing.Point(151, 196);
            this.label_WinMessage.Name = "label_WinMessage";
            this.label_WinMessage.Size = new System.Drawing.Size(304, 39);
            this.label_WinMessage.TabIndex = 0;
            this.label_WinMessage.Text = "You found the exit!";
            this.label_WinMessage.Visible = false;
            // 
            // MazeGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(603, 586);
            this.Controls.Add(this.label_WinMessage);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "MazeGame";
            this.Text = "Maze Game";
            this.Load += new System.EventHandler(this.Maze_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MazeGame_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MazeGame_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer_myTimer;
        private System.Windows.Forms.Label label_WinMessage;
    }
}

