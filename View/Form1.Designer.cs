namespace x86_simple_compiler
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
            this.ChooseFileBtn = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SaveFileBtn = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CompileBtn = new System.Windows.Forms.Button();
            this.ProgramText = new System.Windows.Forms.RichTextBox();
            this.ObjectText = new System.Windows.Forms.RichTextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.ObjSaveBtn = new System.Windows.Forms.Button();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChooseFileBtn
            // 
            this.ChooseFileBtn.BackColor = System.Drawing.Color.MediumBlue;
            this.ChooseFileBtn.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChooseFileBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ChooseFileBtn.Location = new System.Drawing.Point(3, 109);
            this.ChooseFileBtn.Name = "ChooseFileBtn";
            this.ChooseFileBtn.Size = new System.Drawing.Size(200, 100);
            this.ChooseFileBtn.TabIndex = 0;
            this.ChooseFileBtn.Text = "Открыть";
            this.ChooseFileBtn.UseVisualStyleBackColor = false;
            this.ChooseFileBtn.Click += new System.EventHandler(this.ChooseFileBtn_Click);
            // 
            // OpenFileDialog1
            // 
            this.OpenFileDialog1.FileName = "OpenFileDialog1";
            this.OpenFileDialog1.Filter = "Assembler files (*.asm)|*.asm";
            // 
            // SaveFileBtn
            // 
            this.SaveFileBtn.BackColor = System.Drawing.Color.ForestGreen;
            this.SaveFileBtn.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveFileBtn.ForeColor = System.Drawing.Color.White;
            this.SaveFileBtn.Location = new System.Drawing.Point(3, 3);
            this.SaveFileBtn.Name = "SaveFileBtn";
            this.SaveFileBtn.Size = new System.Drawing.Size(200, 100);
            this.SaveFileBtn.TabIndex = 1;
            this.SaveFileBtn.Text = "Сохранить";
            this.SaveFileBtn.UseVisualStyleBackColor = false;
            this.SaveFileBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CompileBtn);
            this.panel1.Controls.Add(this.SaveFileBtn);
            this.panel1.Controls.Add(this.ChooseFileBtn);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(207, 321);
            this.panel1.TabIndex = 2;
            // 
            // CompileBtn
            // 
            this.CompileBtn.BackColor = System.Drawing.Color.Purple;
            this.CompileBtn.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CompileBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.CompileBtn.Location = new System.Drawing.Point(4, 215);
            this.CompileBtn.Name = "CompileBtn";
            this.CompileBtn.Size = new System.Drawing.Size(200, 100);
            this.CompileBtn.TabIndex = 5;
            this.CompileBtn.Text = "Компилировать";
            this.CompileBtn.UseVisualStyleBackColor = false;
            this.CompileBtn.Click += new System.EventHandler(this.CompileBtn_Click);
            // 
            // ProgramText
            // 
            this.ProgramText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ProgramText.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ProgramText.Location = new System.Drawing.Point(225, 12);
            this.ProgramText.Name = "ProgramText";
            this.ProgramText.Size = new System.Drawing.Size(520, 540);
            this.ProgramText.TabIndex = 3;
            this.ProgramText.Text = "";
            // 
            // ObjectText
            // 
            this.ObjectText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ObjectText.Enabled = false;
            this.ObjectText.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ObjectText.Location = new System.Drawing.Point(754, 12);
            this.ObjectText.Name = "ObjectText";
            this.ObjectText.ReadOnly = true;
            this.ObjectText.ShortcutsEnabled = false;
            this.ObjectText.Size = new System.Drawing.Size(520, 540);
            this.ObjectText.TabIndex = 4;
            this.ObjectText.Text = "";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Assembler files (*.asm)|*.asm";
            // 
            // ObjSaveBtn
            // 
            this.ObjSaveBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ObjSaveBtn.BackColor = System.Drawing.Color.DarkGreen;
            this.ObjSaveBtn.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ObjSaveBtn.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.ObjSaveBtn.Location = new System.Drawing.Point(12, 452);
            this.ObjSaveBtn.Name = "ObjSaveBtn";
            this.ObjSaveBtn.Size = new System.Drawing.Size(200, 100);
            this.ObjSaveBtn.TabIndex = 6;
            this.ObjSaveBtn.Text = "Сохранить объектный файл";
            this.ObjSaveBtn.UseVisualStyleBackColor = false;
            this.ObjSaveBtn.Click += new System.EventHandler(this.ObjSaveBtn_Click);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "Object file (*.OBJ)|*.OBJ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1154, 564);
            this.Controls.Add(this.ObjSaveBtn);
            this.Controls.Add(this.ObjectText);
            this.Controls.Add(this.ProgramText);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Kentik Assembler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChooseFileBtn;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.Button SaveFileBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox ProgramText;
        private System.Windows.Forms.RichTextBox ObjectText;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button CompileBtn;
        private System.Windows.Forms.Button ObjSaveBtn;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}

