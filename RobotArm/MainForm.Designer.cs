namespace RobotArm
{
    partial class RobotControlForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.init = new System.Windows.Forms.Button();
            this.move = new System.Windows.Forms.Button();
            this.positionTrackBar = new System.Windows.Forms.TrackBar();
            this.txtPosition = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.positionPanel = new RobotArm.PositionPanel();
            this.clampPanel = new RobotArm.ClampPanel();
            this.txtrotation = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // init
            // 
            this.init.Location = new System.Drawing.Point(90, 390);
            this.init.Name = "init";
            this.init.Size = new System.Drawing.Size(75, 23);
            this.init.TabIndex = 1;
            this.init.Text = "INIT";
            this.init.UseVisualStyleBackColor = true;
            this.init.Click += new System.EventHandler(this.init_Click_1);
            // 
            // move
            // 
            this.move.Location = new System.Drawing.Point(274, 390);
            this.move.Name = "move";
            this.move.Size = new System.Drawing.Size(75, 23);
            this.move.TabIndex = 2;
            this.move.Text = "MOVE";
            this.move.UseVisualStyleBackColor = true;
            this.move.Click += new System.EventHandler(this.move_Click_1);
            // 
            // positionTrackBar
            // 
            this.positionTrackBar.Location = new System.Drawing.Point(49, 22);
            this.positionTrackBar.Name = "positionTrackBar";
            this.positionTrackBar.Size = new System.Drawing.Size(104, 45);
            this.positionTrackBar.TabIndex = 3;
            // 
            // txtPosition
            // 
            this.txtPosition.Location = new System.Drawing.Point(49, 74);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(100, 21);
            this.txtPosition.TabIndex = 4;
            // 
            // positionPanel
            // 
            this.positionPanel.Location = new System.Drawing.Point(12, 111);
            this.positionPanel.Name = "positionPanel";
            this.positionPanel.Size = new System.Drawing.Size(432, 239);
            this.positionPanel.TabIndex = 8;
            this.positionPanel.Text = "positionPanel1";
            // 
            // clampPanel
            // 
            this.clampPanel.Location = new System.Drawing.Point(450, 125);
            this.clampPanel.Name = "clampPanel";
            this.clampPanel.Size = new System.Drawing.Size(225, 210);
            this.clampPanel.TabIndex = 7;
            this.clampPanel.Text = "clampPanel";
            // 
            // txtrotation
            // 
            this.txtrotation.Location = new System.Drawing.Point(466, 74);
            this.txtrotation.Name = "txtrotation";
            this.txtrotation.Size = new System.Drawing.Size(100, 21);
            this.txtrotation.TabIndex = 9;
            // 
            // RobotControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 569);
            this.Controls.Add(this.txtrotation);
            this.Controls.Add(this.positionPanel);
            this.Controls.Add(this.clampPanel);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.positionTrackBar);
            this.Controls.Add(this.move);
            this.Controls.Add(this.init);
            this.Name = "RobotControlForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.RobotControlForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.positionTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button init;
        private System.Windows.Forms.Button move;
        private System.Windows.Forms.TrackBar positionTrackBar;
        private System.Windows.Forms.TextBox txtPosition;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        //private PositionPanel robotArmPositionPanel;
        private ClampPanel clampPanel;
        private PositionPanel positionPanel;
        private System.Windows.Forms.TextBox txtrotation;
    }
}

