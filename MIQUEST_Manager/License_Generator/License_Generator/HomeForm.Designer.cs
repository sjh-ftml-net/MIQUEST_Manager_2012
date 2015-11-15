namespace WindowsFormsApplication1
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
            this.if_dtp_code = new System.Windows.Forms.DateTimePicker();
            this.if_tb_name = new System.Windows.Forms.TextBox();
            this.if_bt_generate = new System.Windows.Forms.Button();
            this.if_tb_lic = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // if_dtp_code
            // 
            this.if_dtp_code.Location = new System.Drawing.Point(27, 38);
            this.if_dtp_code.Name = "if_dtp_code";
            this.if_dtp_code.Size = new System.Drawing.Size(200, 20);
            this.if_dtp_code.TabIndex = 0;
            // 
            // if_tb_name
            // 
            this.if_tb_name.Location = new System.Drawing.Point(27, 12);
            this.if_tb_name.Name = "if_tb_name";
            this.if_tb_name.Size = new System.Drawing.Size(199, 20);
            this.if_tb_name.TabIndex = 1;
            // 
            // if_bt_generate
            // 
            this.if_bt_generate.Location = new System.Drawing.Point(150, 77);
            this.if_bt_generate.Name = "if_bt_generate";
            this.if_bt_generate.Size = new System.Drawing.Size(75, 23);
            this.if_bt_generate.TabIndex = 2;
            this.if_bt_generate.Text = "Generate";
            this.if_bt_generate.UseVisualStyleBackColor = true;
            this.if_bt_generate.Click += new System.EventHandler(this.if_bt_generate_Click);
            // 
            // if_tb_lic
            // 
            this.if_tb_lic.Location = new System.Drawing.Point(27, 248);
            this.if_tb_lic.Name = "if_tb_lic";
            this.if_tb_lic.Size = new System.Drawing.Size(906, 20);
            this.if_tb_lic.TabIndex = 3;
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 292);
            this.Controls.Add(this.if_tb_lic);
            this.Controls.Add(this.if_bt_generate);
            this.Controls.Add(this.if_tb_name);
            this.Controls.Add(this.if_dtp_code);
            this.Name = "HomeForm";
            this.Text = "License Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker if_dtp_code;
        private System.Windows.Forms.TextBox if_tb_name;
        private System.Windows.Forms.Button if_bt_generate;
        private System.Windows.Forms.TextBox if_tb_lic;

    }
}

