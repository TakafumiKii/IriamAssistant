namespace IriamAssistant
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            imageList1 = new ImageList(components);
            button_refresh = new Button();
            pictureBox_Capture = new PictureBox();
            menuStrip1 = new MenuStrip();
            WindowToolStripMenuItem = new ToolStripMenuItem();
            button_analize = new Button();
            button_speak = new Button();
            textBox_speak = new TextBox();
            checkBox_AutoRefresh = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox_Capture).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // button_refresh
            // 
            button_refresh.Location = new Point(0, 36);
            button_refresh.Name = "button_refresh";
            button_refresh.Size = new Size(101, 38);
            button_refresh.TabIndex = 1;
            button_refresh.Text = "画面更新";
            button_refresh.UseVisualStyleBackColor = true;
            button_refresh.Click += button_refresh_Click;
            // 
            // pictureBox_Capture
            // 
            pictureBox_Capture.BackColor = Color.Silver;
            pictureBox_Capture.Location = new Point(0, 36);
            pictureBox_Capture.Name = "pictureBox_Capture";
            pictureBox_Capture.Size = new Size(640, 1176);
            pictureBox_Capture.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox_Capture.TabIndex = 0;
            pictureBox_Capture.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { WindowToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(641, 33);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // WindowToolStripMenuItem
            // 
            WindowToolStripMenuItem.Name = "WindowToolStripMenuItem";
            WindowToolStripMenuItem.Size = new Size(130, 29);
            WindowToolStripMenuItem.Text = "Window選択";
            WindowToolStripMenuItem.DropDownOpening += SelectWindowMenuItem_DropDownOpening;
            // 
            // button_analize
            // 
            button_analize.Location = new Point(107, 36);
            button_analize.Name = "button_analize";
            button_analize.Size = new Size(99, 38);
            button_analize.TabIndex = 2;
            button_analize.Text = "解析";
            button_analize.UseVisualStyleBackColor = true;
            button_analize.Click += button_analize_Click;
            // 
            // button_speak
            // 
            button_speak.Location = new Point(212, 36);
            button_speak.Name = "button_speak";
            button_speak.Size = new Size(101, 38);
            button_speak.TabIndex = 3;
            button_speak.Text = "発音";
            button_speak.UseVisualStyleBackColor = true;
            button_speak.Click += button_speak_Click;
            // 
            // textBox_speak
            // 
            textBox_speak.ImeMode = ImeMode.Disable;
            textBox_speak.Location = new Point(583, 40);
            textBox_speak.Name = "textBox_speak";
            textBox_speak.Size = new Size(53, 31);
            textBox_speak.TabIndex = 4;
            textBox_speak.Text = "5";
            textBox_speak.TextAlign = HorizontalAlignment.Right;
            textBox_speak.TextChanged += textBox1_TextChanged;
            // 
            // checkBox_speak
            // 
            checkBox_AutoRefresh.Appearance = Appearance.Button;
            checkBox_AutoRefresh.AutoSize = true;
            checkBox_AutoRefresh.Location = new Point(483, 38);
            checkBox_AutoRefresh.Name = "checkBox_speak";
            checkBox_AutoRefresh.Size = new Size(94, 35);
            checkBox_AutoRefresh.TabIndex = 5;
            checkBox_AutoRefresh.Text = "自動更新";
            checkBox_AutoRefresh.UseVisualStyleBackColor = true;
            checkBox_AutoRefresh.CheckedChanged += checkBox_speak_CheckedChanged;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(641, 1212);
            Controls.Add(checkBox_AutoRefresh);
            Controls.Add(textBox_speak);
            Controls.Add(button_speak);
            Controls.Add(button_analize);
            Controls.Add(button_refresh);
            Controls.Add(pictureBox_Capture);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "IriamAssistant";
            Load += FormMain_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox_Capture).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ImageList imageList1;
        private Button button_refresh;
        private PictureBox pictureBox_Capture;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem WindowToolStripMenuItem;
        private Button button_analize;
        private Button button_speak;
        private TextBox textBox_speak;
        private CheckBox checkBox_AutoRefresh;
    }
}