namespace Shooter
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Shooter.Properties;

    partial class WildShooter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.components = new Container();
            this.timerGameLoop = new Timer(this.components);
            this.SuspendLayout();
            // 
            // timerGameLoop
            // 
            this.timerGameLoop.Tick += new EventHandler(this.timerGameLoop_Tick);
            // 
            // WildShooter
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackgroundImage = Resources.Background;
            this.ClientSize = new Size(1234, 642);
            this.DoubleBuffered = true;
            this.Name = "WildShooter";
            this.Text = "Wild Shooter";
            this.WindowState = FormWindowState.Maximized;
            this.MouseClick += new MouseEventHandler(this.WildShooter_MouseClick);           
            this.ResumeLayout(false);
        }

        #endregion

        private Timer timerGameLoop;
    }
}

