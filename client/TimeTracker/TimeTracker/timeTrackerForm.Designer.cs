namespace TimeTracker
{
    partial class timeTrackerForm
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
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.txtActivity = new System.Windows.Forms.TextBox();
            this.btnTrack = new System.Windows.Forms.Button();
            this.lblActivity = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtActivity
            // 
            this.txtActivity.Location = new System.Drawing.Point(201, 157);
            this.txtActivity.Name = "txtActivity";
            this.txtActivity.Size = new System.Drawing.Size(415, 38);
            this.txtActivity.TabIndex = 0;
            // 
            // btnTrack
            // 
            this.btnTrack.Location = new System.Drawing.Point(317, 239);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(190, 55);
            this.btnTrack.TabIndex = 1;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = true;
            // 
            // lblActivity
            // 
            this.lblActivity.AutoSize = true;
            this.lblActivity.Location = new System.Drawing.Point(31, 160);
            this.lblActivity.Name = "lblActivity";
            this.lblActivity.Size = new System.Drawing.Size(106, 32);
            this.lblActivity.TabIndex = 2;
            this.lblActivity.Text = "Activity";
            // 
            // timeTrackerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblActivity);
            this.Controls.Add(this.btnTrack);
            this.Controls.Add(this.txtActivity);
            this.Name = "timeTrackerForm";
            this.Text = "Personal Time Tracker";
            this.Load += new System.EventHandler(this.timeTrackerForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox txtActivity;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Label lblActivity;
    }
}