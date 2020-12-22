
using System;

namespace FoxAndChees
{
    partial class FoxView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FoxView));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.Just_text = new System.Windows.Forms.ToolStripStatusLabel();
            this._foxLife = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this._timeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this._eatenChees = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.six = new System.Windows.Forms.ToolStripMenuItem();
            this.eight = new System.Windows.Forms.ToolStripMenuItem();
            this.ten = new System.Windows.Forms.ToolStripMenuItem();
            this._newGame = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Just_text,
            this._foxLife,
            this.toolStripStatusLabel1,
            this._timeLabel,
            this.toolStripStatusLabel2,
            this._eatenChees,
            this.toolStripDropDownButton1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 627);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1182, 26);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // Just_text
            // 
            this.Just_text.Name = "Just_text";
            this.Just_text.Size = new System.Drawing.Size(57, 20);
            this.Just_text.Text = "Fox life";
            // 
            // _foxLife
            // 
            this._foxLife.Name = "_foxLife";
            this._foxLife.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(45, 20);
            this.toolStripStatusLabel1.Text = "Time:";
            // 
            // _timeLabel
            // 
            this._timeLabel.Name = "_timeLabel";
            this._timeLabel.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(85, 20);
            this.toolStripStatusLabel2.Text = "EatenChees";
            // 
            // _eatenChees
            // 
            this._eatenChees.Name = "_eatenChees";
            this._eatenChees.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(34, 24);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.six,
            this.eight,
            this.ten,
            this._newGame});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(56, 24);
            this.toolStripMenuItem1.Text = "Jatek";
            // 
            // six
            // 
            this.six.Name = "six";
            this.six.Size = new System.Drawing.Size(165, 26);
            this.six.Text = "6x6";
            this.six.Click += new System.EventHandler(this.NewGame6_Click);
            // 
            // eight
            // 
            this.eight.Name = "eight";
            this.eight.Size = new System.Drawing.Size(165, 26);
            this.eight.Text = "8x8";
            this.eight.Click += new System.EventHandler(this.NewGame8_Click);
            // 
            // ten
            // 
            this.ten.Name = "ten";
            this.ten.Size = new System.Drawing.Size(165, 26);
            this.ten.Text = "10x10";
            this.ten.Click += new System.EventHandler(this.NewGame10_Click);
            // 
            // _newGame
            // 
            this._newGame.Name = "_newGame";
            this._newGame.Size = new System.Drawing.Size(165, 26);
            this._newGame.Text = "New Game";
            this._newGame.Click += new System.EventHandler(this.NewGame_Click);
            // 
            // FoxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FoxView";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.FoxView_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FoxView_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FoxView_MouseClick);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel Just_text;
        private System.Windows.Forms.ToolStripStatusLabel _foxLife;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel _timeLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel _eatenChees;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem six;
        private System.Windows.Forms.ToolStripMenuItem eight;
        private System.Windows.Forms.ToolStripMenuItem ten;
        private System.Windows.Forms.ToolStripMenuItem _newGame;
    }
}

