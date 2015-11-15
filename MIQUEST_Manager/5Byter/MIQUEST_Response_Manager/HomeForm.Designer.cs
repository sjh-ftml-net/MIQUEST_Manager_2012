namespace MIQUEST_Response_Manager
{
    partial class HomeForm
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
            this.MQ_tb_ResponseFolderPath = new System.Windows.Forms.TextBox();
            this.MQ_bt_ResponseFolderSelect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.MQ_bt_Run = new System.Windows.Forms.Button();
            this.MQ_bt_Quit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.MQ_bt_OutputFolderSelect = new System.Windows.Forms.Button();
            this.MQ_tb_OutputFolderPath = new System.Windows.Forms.TextBox();
            this.MQ_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.MQ_tb_status = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MQ_tb_ResponseFolderPath
            // 
            this.MQ_tb_ResponseFolderPath.Location = new System.Drawing.Point(121, 55);
            this.MQ_tb_ResponseFolderPath.Name = "MQ_tb_ResponseFolderPath";
            this.MQ_tb_ResponseFolderPath.Size = new System.Drawing.Size(548, 20);
            this.MQ_tb_ResponseFolderPath.TabIndex = 0;
            // 
            // MQ_bt_ResponseFolderSelect
            // 
            this.MQ_bt_ResponseFolderSelect.Location = new System.Drawing.Point(676, 55);
            this.MQ_bt_ResponseFolderSelect.Name = "MQ_bt_ResponseFolderSelect";
            this.MQ_bt_ResponseFolderSelect.Size = new System.Drawing.Size(26, 20);
            this.MQ_bt_ResponseFolderSelect.TabIndex = 1;
            this.MQ_bt_ResponseFolderSelect.Text = "...";
            this.MQ_bt_ResponseFolderSelect.UseVisualStyleBackColor = true;
            this.MQ_bt_ResponseFolderSelect.Click += new System.EventHandler(this.MQ_bt_ResponseFolderSelect_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Response Folder";
            // 
            // MQ_bt_Run
            // 
            this.MQ_bt_Run.Location = new System.Drawing.Point(632, 209);
            this.MQ_bt_Run.Name = "MQ_bt_Run";
            this.MQ_bt_Run.Size = new System.Drawing.Size(75, 23);
            this.MQ_bt_Run.TabIndex = 3;
            this.MQ_bt_Run.Text = "Run";
            this.MQ_bt_Run.UseVisualStyleBackColor = true;
            this.MQ_bt_Run.Click += new System.EventHandler(this.MQ_bt_Run_Click);
            // 
            // MQ_bt_Quit
            // 
            this.MQ_bt_Quit.Location = new System.Drawing.Point(551, 208);
            this.MQ_bt_Quit.Name = "MQ_bt_Quit";
            this.MQ_bt_Quit.Size = new System.Drawing.Size(75, 23);
            this.MQ_bt_Quit.TabIndex = 4;
            this.MQ_bt_Quit.Text = "Quit";
            this.MQ_bt_Quit.UseVisualStyleBackColor = true;
            this.MQ_bt_Quit.Click += new System.EventHandler(this.MQ_bt_Quit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output Folder";
            // 
            // MQ_bt_OutputFolderSelect
            // 
            this.MQ_bt_OutputFolderSelect.Location = new System.Drawing.Point(676, 81);
            this.MQ_bt_OutputFolderSelect.Name = "MQ_bt_OutputFolderSelect";
            this.MQ_bt_OutputFolderSelect.Size = new System.Drawing.Size(26, 20);
            this.MQ_bt_OutputFolderSelect.TabIndex = 6;
            this.MQ_bt_OutputFolderSelect.Text = "...";
            this.MQ_bt_OutputFolderSelect.UseVisualStyleBackColor = true;
            this.MQ_bt_OutputFolderSelect.Click += new System.EventHandler(this.MQ_bt_OutputFolderSelect_Click);
            // 
            // MQ_tb_OutputFolderPath
            // 
            this.MQ_tb_OutputFolderPath.Location = new System.Drawing.Point(121, 81);
            this.MQ_tb_OutputFolderPath.Name = "MQ_tb_OutputFolderPath";
            this.MQ_tb_OutputFolderPath.Size = new System.Drawing.Size(548, 20);
            this.MQ_tb_OutputFolderPath.TabIndex = 5;
            // 
            // MQ_ProgressBar
            // 
            this.MQ_ProgressBar.Location = new System.Drawing.Point(30, 193);
            this.MQ_ProgressBar.Name = "MQ_ProgressBar";
            this.MQ_ProgressBar.Size = new System.Drawing.Size(675, 10);
            this.MQ_ProgressBar.TabIndex = 8;
            // 
            // MQ_tb_status
            // 
            this.MQ_tb_status.Enabled = false;
            this.MQ_tb_status.Location = new System.Drawing.Point(30, 209);
            this.MQ_tb_status.Name = "MQ_tb_status";
            this.MQ_tb_status.Size = new System.Drawing.Size(515, 20);
            this.MQ_tb_status.TabIndex = 9;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 262);
            this.Controls.Add(this.MQ_tb_status);
            this.Controls.Add(this.MQ_ProgressBar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MQ_bt_OutputFolderSelect);
            this.Controls.Add(this.MQ_tb_OutputFolderPath);
            this.Controls.Add(this.MQ_bt_Quit);
            this.Controls.Add(this.MQ_bt_Run);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.MQ_bt_ResponseFolderSelect);
            this.Controls.Add(this.MQ_tb_ResponseFolderPath);
            this.Name = "HomeForm";
            this.Text = "MIQUEST Response Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeForm_FormClosing);
            this.Load += new System.EventHandler(this.HomeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MQ_tb_ResponseFolderPath;
        private System.Windows.Forms.Button MQ_bt_ResponseFolderSelect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button MQ_bt_Run;
        private System.Windows.Forms.Button MQ_bt_Quit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button MQ_bt_OutputFolderSelect;
        private System.Windows.Forms.TextBox MQ_tb_OutputFolderPath;
        public System.Windows.Forms.ProgressBar MQ_ProgressBar;
        private System.Windows.Forms.TextBox MQ_tb_status;
    }
}

