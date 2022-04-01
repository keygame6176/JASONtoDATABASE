using System;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Transfer2Raccoon

{
	// Token: 0x02000003 RID: 3
	public partial class Form1 : Form
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002093 File Offset: 0x00000293
		public Form1()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020A1 File Offset: 0x000002A1
		private void Form1_Load(object sender, EventArgs e)
		{
			this.listBox1.DataSource = ConfigurationManager.AppSettings.AllKeys;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020B8 File Offset: 0x000002B8
		private void button1_Click(object sender, EventArgs e)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (!(this.textBox1.Text != "") || !(this.textBox2.Text != ""))
			{
				MessageBox.Show("Key與Value欄位不可為空");
				return;
			}
			if (!config.AppSettings.Settings.AllKeys.Contains(this.textBox1.Text))
			{
				config.AppSettings.Settings.Add(this.textBox1.Text, this.textBox2.Text);
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
				this.listBox1.DataSource = ConfigurationManager.AppSettings.AllKeys;
				this.textBox1.Text = "";
				this.textBox2.Text = "";
				MessageBox.Show("新增成功");
				return;
			}
			MessageBox.Show("這個Key已存在，請更換Key的字");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021B4 File Offset: 0x000003B4
		private void button2_Click(object sender, EventArgs e)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (!(this.textBox1.Text != "") || !(this.textBox2.Text != ""))
			{
				MessageBox.Show("Key與Value欄位不可為空");
				return;
			}
			if (!config.AppSettings.Settings.AllKeys.Contains(this.textBox1.Text) || this.listBox1.SelectedValue.ToString() == this.textBox1.Text)
			{
				config.AppSettings.Settings.Remove(this.listBox1.SelectedValue.ToString());
				config.AppSettings.Settings.Add(this.textBox1.Text, this.textBox2.Text);
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
				this.listBox1.DataSource = ConfigurationManager.AppSettings.AllKeys;
				MessageBox.Show("修改成功");
				return;
			}
			MessageBox.Show("這個Key已存在，請更換Key的字");
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022CF File Offset: 0x000004CF
		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.textBox1.Text = this.listBox1.SelectedValue.ToString();
			this.textBox2.Text = ConfigurationManager.AppSettings[this.textBox1.Text];
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000230C File Offset: 0x0000050C
		private void button3_Click(object sender, EventArgs e)
		{
			Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			if (!(this.textBox1.Text != "") || !(this.textBox2.Text != ""))
			{
				MessageBox.Show("Key與Value欄位不可為空");
				return;
			}
			if (config.AppSettings.Settings.AllKeys.Contains(this.textBox1.Text))
			{
				config.AppSettings.Settings.Remove(this.listBox1.SelectedValue.ToString());
				config.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("appSettings");
				this.listBox1.DataSource = ConfigurationManager.AppSettings.AllKeys;
				return;
			}
			MessageBox.Show("這個Key不存在");
		}
	}
}
