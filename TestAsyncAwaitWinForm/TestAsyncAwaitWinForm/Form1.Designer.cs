namespace TestAsyncAwaitWinForm
{
    partial class Form1
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
            this.buttonProcessFile = new System.Windows.Forms.Button();
            this.labelProcessFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonProcessFile
            // 
            this.buttonProcessFile.Location = new System.Drawing.Point(95, 29);
            this.buttonProcessFile.Name = "buttonProcessFile";
            this.buttonProcessFile.Size = new System.Drawing.Size(143, 33);
            this.buttonProcessFile.TabIndex = 0;
            this.buttonProcessFile.Text = "Process File";
            this.buttonProcessFile.UseVisualStyleBackColor = true;
            this.buttonProcessFile.Click += new System.EventHandler(this.buttonProcessFile_Click);
            // 
            // labelProcessFile
            // 
            this.labelProcessFile.AutoSize = true;
            this.labelProcessFile.Location = new System.Drawing.Point(46, 98);
            this.labelProcessFile.Name = "labelProcessFile";
            this.labelProcessFile.Size = new System.Drawing.Size(0, 13);
            this.labelProcessFile.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(349, 261);
            this.Controls.Add(this.labelProcessFile);
            this.Controls.Add(this.buttonProcessFile);
            this.Name = "Form1";
            this.Text = "Count characters in a file";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonProcessFile;
        private System.Windows.Forms.Label labelProcessFile;
    }
}

