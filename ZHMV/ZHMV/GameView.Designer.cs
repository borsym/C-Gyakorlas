
using System;

namespace ZHMV
{
    partial class GameView
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame6 = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame8 = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame10 = new System.Windows.Forms.ToolStripMenuItem();
            this._Pause = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this._Label = new System.Windows.Forms.ToolStripStatusLabel();
            this._LabelStep = new System.Windows.Forms.ToolStripStatusLabel();
            this._Label2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._LabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newGame,
            this._newGame6,
            this._newGame8,
            this._newGame10,
            this._Pause});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(46, 24);
            this.toolStripMenuItem1.Text = "FIle";
            // 
            // _newGame
            // 
            this._newGame.Name = "_newGame";
            this._newGame.Size = new System.Drawing.Size(165, 26);
            this._newGame.Text = "New Game";
            this._newGame.Click += new System.EventHandler(this.MenuFileNewGame_Click);
            // 
            // _newGame6
            // 
            this._newGame6.Name = "_newGame6";
            this._newGame6.Size = new System.Drawing.Size(165, 26);
            this._newGame6.Text = "6x6";
            this._newGame6.Click += new System.EventHandler(this.MenuFileNewGame6_Click);
            // 
            // _newGame8
            // 
            this._newGame8.Name = "_newGame8";
            this._newGame8.Size = new System.Drawing.Size(165, 26);
            this._newGame8.Text = "8x8";
            this._newGame8.Click += new System.EventHandler(this.MenuFileNewGame8_Click);
            // 
            // _newGame10
            // 
            this._newGame10.Name = "_newGame10";
            this._newGame10.Size = new System.Drawing.Size(165, 26);
            this._newGame10.Text = "10x10";
            this._newGame10.Click += new System.EventHandler(this.MenuFileNewGame10_Click);
            // 
            // _Pause
            // 
            this._Pause.Name = "_Pause";
            this._Pause.Size = new System.Drawing.Size(165, 26);
            this._Pause.Text = "Pause";
            this._Pause.Click += new System.EventHandler(this.Pause_Game);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._Label,
            this._LabelStep,
            this._Label2,
            this._LabelTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 424);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 26);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // _Label
            // 
            this._Label.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this._Label.Name = "_Label";
            this._Label.Size = new System.Drawing.Size(55, 20);
            this._Label.Text = "Points: ";
            // 
            // _LabelStep
            // 
            this._LabelStep.Name = "_LabelStep";
            this._LabelStep.Size = new System.Drawing.Size(17, 20);
            this._LabelStep.Text = "0";
            // 
            // _Label2
            // 
            this._Label2.Name = "_Label2";
            this._Label2.Size = new System.Drawing.Size(49, 20);
            this._Label2.Text = "Time: ";
            // 
            // _LabelTime
            // 
            this._LabelTime.Name = "_LabelTime";
            this._LabelTime.Size = new System.Drawing.Size(17, 20);
            this._LabelTime.Text = "0";
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "GameView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.GameView_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GameView_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem _newGame;
        private System.Windows.Forms.ToolStripMenuItem _newGame6;
        private System.Windows.Forms.ToolStripMenuItem _newGame8;
        private System.Windows.Forms.ToolStripMenuItem _newGame10;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel _Label;
        private System.Windows.Forms.ToolStripStatusLabel _LabelStep;
        private System.Windows.Forms.ToolStripStatusLabel _Label2;
        private System.Windows.Forms.ToolStripStatusLabel _LabelTime;
        private System.Windows.Forms.ToolStripMenuItem _Pause;
    }
}

