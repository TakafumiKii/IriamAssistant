namespace IriamAssistant
{
    partial class FormCapture
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
            pictureBox_Capture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Capture).BeginInit();
            SuspendLayout();
            // 
            // pictureBox_Capture
            // 
            pictureBox_Capture.BackColor = Color.Silver;
            pictureBox_Capture.Location = new Point(0, 0);
            pictureBox_Capture.Name = "pictureBox_Capture";
            pictureBox_Capture.Size = new Size(966, 448);
            pictureBox_Capture.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Capture.TabIndex = 1;
            pictureBox_Capture.TabStop = false;
            // 
            // FormCapture
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(966, 450);
            Controls.Add(pictureBox_Capture);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "FormCapture";
            Text = "FormCapture";
            Load += FormCapture_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox_Capture).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox_Capture;
    }
}