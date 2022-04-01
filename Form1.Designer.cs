namespace Transfer2Raccoon
{
    // Token: 0x02000003 RID: 3
    public partial class Form1 : global::System.Windows.Forms.Form
    {
        // Token: 0x0600000B RID: 11 RVA: 0x000023D1 File Offset: 0x000005D1
        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        // Token: 0x0600000C RID: 12 RVA: 0x000023F0 File Offset: 0x000005F0
        private void InitializeComponent()
        {
            this.listBox1 = new global::System.Windows.Forms.ListBox();
            this.textBox1 = new global::System.Windows.Forms.TextBox();
            this.label1 = new global::System.Windows.Forms.Label();
            this.textBox2 = new global::System.Windows.Forms.TextBox();
            this.label2 = new global::System.Windows.Forms.Label();
            this.button1 = new global::System.Windows.Forms.Button();
            this.button2 = new global::System.Windows.Forms.Button();
            this.button3 = new global::System.Windows.Forms.Button();
            base.SuspendLayout();
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new global::System.Drawing.Point(12, 28);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new global::System.Drawing.Size(94, 160);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new global::System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.textBox1.Location = new global::System.Drawing.Point(158, 28);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new global::System.Drawing.Size(114, 22);
            this.textBox1.TabIndex = 1;
            this.label1.AutoSize = true;
            this.label1.Location = new global::System.Drawing.Point(119, 31);
            this.label1.Name = "label1";
            this.label1.Size = new global::System.Drawing.Size(24, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Key";
            this.textBox2.Location = new global::System.Drawing.Point(158, 73);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new global::System.Drawing.Size(114, 22);
            this.textBox2.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new global::System.Drawing.Point(119, 76);
            this.label2.Name = "label2";
            this.label2.Size = new global::System.Drawing.Size(32, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "Value";
            this.button1.Location = new global::System.Drawing.Point(158, 113);
            this.button1.Name = "button1";
            this.button1.Size = new global::System.Drawing.Size(84, 32);
            this.button1.TabIndex = 3;
            this.button1.Text = "新增";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new global::System.EventHandler(this.button1_Click);
            this.button2.Location = new global::System.Drawing.Point(158, 156);
            this.button2.Name = "button2";
            this.button2.Size = new global::System.Drawing.Size(84, 32);
            this.button2.TabIndex = 3;
            this.button2.Text = "修改";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new global::System.EventHandler(this.button2_Click);
            this.button3.Location = new global::System.Drawing.Point(158, 199);
            this.button3.Name = "button3";
            this.button3.Size = new global::System.Drawing.Size(84, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "刪除";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new global::System.EventHandler(this.button3_Click);
            base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 12f);
            base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
            base.ClientSize = new global::System.Drawing.Size(284, 262);
            base.Controls.Add(this.button3);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBox2);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.listBox1);
            base.Name = "Form1";
            this.Text = "Form1";
            base.Load += new global::System.EventHandler(this.Form1_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        // Token: 0x04000003 RID: 3
        private global::System.ComponentModel.IContainer components;

        // Token: 0x04000004 RID: 4
        private global::System.Windows.Forms.ListBox listBox1;

        // Token: 0x04000005 RID: 5
        private global::System.Windows.Forms.TextBox textBox1;

        // Token: 0x04000006 RID: 6
        private global::System.Windows.Forms.Label label1;

        // Token: 0x04000007 RID: 7
        private global::System.Windows.Forms.TextBox textBox2;

        // Token: 0x04000008 RID: 8
        private global::System.Windows.Forms.Label label2;

        // Token: 0x04000009 RID: 9
        private global::System.Windows.Forms.Button button1;

        // Token: 0x0400000A RID: 10
        private global::System.Windows.Forms.Button button2;

        // Token: 0x0400000B RID: 11
        private global::System.Windows.Forms.Button button3;
    }
}
