namespace com.alexleekt.aideNotebook
{
    partial class HUD
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
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pbExtraControlLogo = new System.Windows.Forms.PictureBox();
            this.lblExtraControlText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbExtraControlLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(351, 53);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(144, 18);
            this.lblDescription.TabIndex = 2;
            this.lblDescription.Text = "[[ description ]]";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPercentage
            // 
            this.lblPercentage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblPercentage.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentage.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblPercentage.Image = global::com.alexleekt.aideNotebook.Properties.Resources.cpu_frequency_image;
            this.lblPercentage.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblPercentage.Location = new System.Drawing.Point(40, 8);
            this.lblPercentage.Margin = new System.Windows.Forms.Padding(8);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(496, 113);
            this.lblPercentage.TabIndex = 1;
            this.lblPercentage.Text = " [[ % ]]";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.DimGray;
            this.label2.Font = new System.Drawing.Font("Segoe Print", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(16, 2, 16, 2);
            this.label2.Size = new System.Drawing.Size(190, 41);
            this.label2.TabIndex = 4;
            this.label2.Text = "aideNotebook";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbExtraControlLogo
            // 
            this.pbExtraControlLogo.Location = new System.Drawing.Point(407, 107);
            this.pbExtraControlLogo.Name = "pbExtraControlLogo";
            this.pbExtraControlLogo.Size = new System.Drawing.Size(32, 32);
            this.pbExtraControlLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbExtraControlLogo.TabIndex = 5;
            this.pbExtraControlLogo.TabStop = false;
            // 
            // lblExtraControlText
            // 
            this.lblExtraControlText.AutoSize = true;
            this.lblExtraControlText.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExtraControlText.Location = new System.Drawing.Point(438, 107);
            this.lblExtraControlText.Name = "lblExtraControlText";
            this.lblExtraControlText.Size = new System.Drawing.Size(50, 25);
            this.lblExtraControlText.TabIndex = 6;
            this.lblExtraControlText.Text = "+/-";
            this.lblExtraControlText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblExtraControlText.Visible = false;
            // 
            // HUD
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.MenuBar;
            this.ClientSize = new System.Drawing.Size(560, 154);
            this.Controls.Add(this.lblExtraControlText);
            this.Controls.Add(this.pbExtraControlLogo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblPercentage);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HUD";
            this.Opacity = 0.7D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.Highlight;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HUD_FormClosed);
            this.Load += new System.EventHandler(this.HUD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.hudKeyDownHandler);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbExtraControlLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPercentage;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pbExtraControlLogo;
        private System.Windows.Forms.Label lblExtraControlText;
    }
}

