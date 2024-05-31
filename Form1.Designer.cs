namespace utils
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
            this.removeTipBonesButton = new System.Windows.Forms.Button();
            this.sortMaterialByTexButton = new System.Windows.Forms.Button();
            this.removeAndReplaceButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // removeTipBonesButton
            // 
            this.removeTipBonesButton.Location = new System.Drawing.Point(13, 13);
            this.removeTipBonesButton.Name = "removeTipBonesButton";
            this.removeTipBonesButton.Size = new System.Drawing.Size(775, 23);
            this.removeTipBonesButton.TabIndex = 0;
            this.removeTipBonesButton.Text = "Remove Tip Bones";
            this.removeTipBonesButton.UseVisualStyleBackColor = true;
            this.removeTipBonesButton.Click += new System.EventHandler(this.RemoveTipBonesButton_Click);
            // 
            // sortMaterialByTexButton
            // 
            this.sortMaterialByTexButton.Location = new System.Drawing.Point(13, 43);
            this.sortMaterialByTexButton.Name = "sortMaterialByTexButton";
            this.sortMaterialByTexButton.Size = new System.Drawing.Size(775, 23);
            this.sortMaterialByTexButton.TabIndex = 1;
            this.sortMaterialByTexButton.Text = "Sort Material by Texture";
            this.sortMaterialByTexButton.UseVisualStyleBackColor = true;
            this.sortMaterialByTexButton.Click += new System.EventHandler(this.SortMaterialByTexButton_Click);
            // 
            // removeAndReplaceButton
            // 
            this.removeAndReplaceButton.Location = new System.Drawing.Point(13, 73);
            this.removeAndReplaceButton.Name = "removeAndReplaceButton";
            this.removeAndReplaceButton.Size = new System.Drawing.Size(775, 23);
            this.removeAndReplaceButton.TabIndex = 2;
            this.removeAndReplaceButton.Text = "Replace and Remove Selected Bones";
            this.removeAndReplaceButton.UseVisualStyleBackColor = true;
            this.removeAndReplaceButton.Click += new System.EventHandler(this.RemoveAndReplaceButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.removeAndReplaceButton);
            this.Controls.Add(this.sortMaterialByTexButton);
            this.Controls.Add(this.removeTipBonesButton);
            this.Name = "Form1";
            this.Text = "ExportUtils";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removeTipBonesButton;
        private System.Windows.Forms.Button sortMaterialByTexButton;
        private System.Windows.Forms.Button removeAndReplaceButton;
    }
}