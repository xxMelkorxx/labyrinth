
namespace Labyrinth
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.GroupBox gB_labyrinth;
            System.Windows.Forms.GroupBox gB_paramGenLabyrinth;
            System.Windows.Forms.Label label1;
            this.panel_picture = new System.Windows.Forms.Panel();
            this.pictureBox_labyrinth = new System.Windows.Forms.PictureBox();
            this.button_saveImage = new System.Windows.Forms.Button();
            this.checkBox_fixedDots = new System.Windows.Forms.CheckBox();
            this.button_passLabyrinth = new System.Windows.Forms.Button();
            this.numUpDown_sizeLabyrinth = new System.Windows.Forms.NumericUpDown();
            this.button_generatedLabyrinth = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            gB_labyrinth = new System.Windows.Forms.GroupBox();
            gB_paramGenLabyrinth = new System.Windows.Forms.GroupBox();
            label1 = new System.Windows.Forms.Label();
            gB_labyrinth.SuspendLayout();
            this.panel_picture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_labyrinth)).BeginInit();
            gB_paramGenLabyrinth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_sizeLabyrinth)).BeginInit();
            this.SuspendLayout();
            // 
            // gB_labyrinth
            // 
            gB_labyrinth.Controls.Add(this.panel_picture);
            gB_labyrinth.Controls.Add(this.button_saveImage);
            gB_labyrinth.Location = new System.Drawing.Point(12, 12);
            gB_labyrinth.Name = "gB_labyrinth";
            gB_labyrinth.Size = new System.Drawing.Size(563, 608);
            gB_labyrinth.TabIndex = 0;
            gB_labyrinth.TabStop = false;
            gB_labyrinth.Text = "Лабиринт:";
            // 
            // panel_picture
            // 
            this.panel_picture.AutoScroll = true;
            this.panel_picture.Controls.Add(this.pictureBox_labyrinth);
            this.panel_picture.Location = new System.Drawing.Point(6, 19);
            this.panel_picture.Name = "panel_picture";
            this.panel_picture.Size = new System.Drawing.Size(551, 552);
            this.panel_picture.TabIndex = 1;
            // 
            // pictureBox_labyrinth
            // 
            this.pictureBox_labyrinth.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox_labyrinth.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox_labyrinth.Location = new System.Drawing.Point(5, 5);
            this.pictureBox_labyrinth.Name = "pictureBox_labyrinth";
            this.pictureBox_labyrinth.Size = new System.Drawing.Size(540, 540);
            this.pictureBox_labyrinth.TabIndex = 0;
            this.pictureBox_labyrinth.TabStop = false;
            // 
            // button_saveImage
            // 
            this.button_saveImage.Enabled = false;
            this.button_saveImage.Location = new System.Drawing.Point(297, 577);
            this.button_saveImage.Name = "button_saveImage";
            this.button_saveImage.Size = new System.Drawing.Size(260, 25);
            this.button_saveImage.TabIndex = 0;
            this.button_saveImage.Text = "Сохранить изображение";
            this.button_saveImage.UseVisualStyleBackColor = true;
            this.button_saveImage.Click += new System.EventHandler(this.OnClickButtonSaveImage);
            // 
            // gB_paramGenLabyrinth
            // 
            gB_paramGenLabyrinth.Controls.Add(this.checkBox_fixedDots);
            gB_paramGenLabyrinth.Controls.Add(this.button_passLabyrinth);
            gB_paramGenLabyrinth.Controls.Add(label1);
            gB_paramGenLabyrinth.Controls.Add(this.numUpDown_sizeLabyrinth);
            gB_paramGenLabyrinth.Controls.Add(this.button_generatedLabyrinth);
            gB_paramGenLabyrinth.Location = new System.Drawing.Point(12, 626);
            gB_paramGenLabyrinth.Name = "gB_paramGenLabyrinth";
            gB_paramGenLabyrinth.Size = new System.Drawing.Size(563, 75);
            gB_paramGenLabyrinth.TabIndex = 1;
            gB_paramGenLabyrinth.TabStop = false;
            gB_paramGenLabyrinth.Text = "Панель управления:";
            // 
            // checkBox_fixedDots
            // 
            this.checkBox_fixedDots.AutoSize = true;
            this.checkBox_fixedDots.Location = new System.Drawing.Point(415, 20);
            this.checkBox_fixedDots.Name = "checkBox_fixedDots";
            this.checkBox_fixedDots.Size = new System.Drawing.Size(142, 17);
            this.checkBox_fixedDots.TabIndex = 5;
            this.checkBox_fixedDots.Text = "Фиксированные точки";
            this.checkBox_fixedDots.UseVisualStyleBackColor = true;
            // 
            // button_passLabyrinth
            // 
            this.button_passLabyrinth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_passLabyrinth.Enabled = false;
            this.button_passLabyrinth.Location = new System.Drawing.Point(297, 44);
            this.button_passLabyrinth.Name = "button_passLabyrinth";
            this.button_passLabyrinth.Size = new System.Drawing.Size(260, 25);
            this.button_passLabyrinth.TabIndex = 0;
            this.button_passLabyrinth.Text = "Пройти лабиринт";
            this.button_passLabyrinth.UseVisualStyleBackColor = true;
            this.button_passLabyrinth.Click += new System.EventHandler(this.OnClickButtonPassLabyrinth);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 21);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(76, 13);
            label1.TabIndex = 2;
            label1.Text = "Размер, NxN:";
            // 
            // numUpDown_sizeLabyrinth
            // 
            this.numUpDown_sizeLabyrinth.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numUpDown_sizeLabyrinth.Location = new System.Drawing.Point(166, 19);
            this.numUpDown_sizeLabyrinth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numUpDown_sizeLabyrinth.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numUpDown_sizeLabyrinth.Name = "numUpDown_sizeLabyrinth";
            this.numUpDown_sizeLabyrinth.Size = new System.Drawing.Size(100, 20);
            this.numUpDown_sizeLabyrinth.TabIndex = 1;
            this.numUpDown_sizeLabyrinth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // button_generatedLabyrinth
            // 
            this.button_generatedLabyrinth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_generatedLabyrinth.Location = new System.Drawing.Point(6, 44);
            this.button_generatedLabyrinth.Name = "button_generatedLabyrinth";
            this.button_generatedLabyrinth.Size = new System.Drawing.Size(260, 25);
            this.button_generatedLabyrinth.TabIndex = 0;
            this.button_generatedLabyrinth.Text = "Сгенерировать лабиринт";
            this.button_generatedLabyrinth.UseVisualStyleBackColor = true;
            this.button_generatedLabyrinth.Click += new System.EventHandler(this.OnClickButtonGeneratedLabyrinth);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(584, 707);
            this.Controls.Add(gB_paramGenLabyrinth);
            this.Controls.Add(gB_labyrinth);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "ННГУ ИТФИ | Лабиринт";
            this.Load += new System.EventHandler(this.OnLoadMainForm);
            gB_labyrinth.ResumeLayout(false);
            this.panel_picture.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_labyrinth)).EndInit();
            gB_paramGenLabyrinth.ResumeLayout(false);
            gB_paramGenLabyrinth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown_sizeLabyrinth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.NumericUpDown numUpDown_sizeLabyrinth;
        private System.Windows.Forms.Button button_generatedLabyrinth;
        private System.Windows.Forms.Button button_passLabyrinth;
        private System.Windows.Forms.Button button_saveImage;
        private System.Windows.Forms.Panel panel_picture;
        private System.Windows.Forms.PictureBox pictureBox_labyrinth;
        private System.Windows.Forms.CheckBox checkBox_fixedDots;
    }
}

