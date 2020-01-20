using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winQRcode_ch
{
    public partial class Form1 : Form
    {
        int resolution = 10;
        bool autoclear = true;
        bool gamemode = false;
        bool autogenerate = false;
        string savename = "";
        LinkedList<string> QRcodes = new LinkedList<string>(); 
        public Form1()
        {
            InitializeComponent();
            QR qr = new QR();

            toolStripStatusLabel1.Text = "在右侧文本框输入文字/代码/网址 生成二维码";

        }

        private void filllistbox()
        {
            listBox1.Items.Clear();
            foreach(var item in QRcodes)
            {
                listBox1.Items.Add(item);
            }
        }

        //autogenerate qrcode on text changed
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("")) return;
            if (autogenerate == true)
            {
                if(gamemode == true)
                {
                    QRcodes.AddFirst(textBox1.Text);
                    filllistbox();
                }
                else
                {
                    QR.RegenerateQr(resolution, textBox1.Text, pictureBox1,progressBar1);
                }
            }
        }
        //normaly generate a QRcode use button
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("")) return;
            toolStripStatusLabel1.Text = "如果没有生成，请耐心等待，不要重复点击";
            QR.RegenerateQr(resolution,textBox1.Text, pictureBox1,progressBar1);
            QRcodes.AddFirst(textBox1.Text);
            filllistbox();
            if(autoclear == true)
            {
                textBox1.Clear();
            }
        }

        // set resolution
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            resolution = trackBar1.Value;
            label1.Text = "清晰度:" + resolution;
            QR.RegenerateQr(resolution, textBox1.Text, pictureBox1,progressBar1);
            toolStripStatusLabel1.Text = "清晰度调节到 ：" + resolution;
            if(resolution > 21)
            {
                toolStripStatusLabel1.Text = "清晰度调节到 ：" + resolution +" 推荐内容文字长的内容使用";
            }
        }
        // reopen a history QRcode
        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem != null)
            {
                string reopenQR = listBox1.SelectedItem.ToString();
                
                textBox1.Text = reopenQR;
                //using tempflag to avoid re write in listbox
                bool tempflag = false;
                if(autogenerate == true)
                {
                    tempflag = true;
                    autogenerate = false;
                }
                if(gamemode != true)
                {
                    QR.RegenerateQr(resolution, reopenQR, pictureBox1,progressBar1);
                    toolStripStatusLabel1.Text = "重新生成 ：" + reopenQR;
                }
                if (tempflag)
                {
                    autogenerate = true;
                }
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                autogenerate = true;
                toolStripStatusLabel1.Text = "在输入框内容改变时重新生成二维码 在内容很长的时候会卡";
            }
            else
            {
                autogenerate = false;
                toolStripStatusLabel1.Text = "自动重新生成 已关闭";
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                autoclear = true;
                toolStripStatusLabel1.Text = "会在点击生成按钮后自动清空输入框";
            }
            else
            {
                autoclear = false;
                toolStripStatusLabel1.Text = "自动清空 已关闭";
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                gamemode = true;
                autogenerate = true;
                checkBox1.Checked = true;
                toolStripStatusLabel1.Text = "gamemode ON";

            }
            else
            {
                gamemode = false;
                toolStripStatusLabel1.Text = "gamemode OFF";

            }
        }
        // clear all list things
        private void button2_Click(object sender, EventArgs e)
        {
            QRcodes.Clear();
            filllistbox();
        }
        //open big pic from
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Image transfer_pic = pictureBox1.Image;
            bigpic childForm = new bigpic(transfer_pic);
            childForm.Show();
        }
        //save pic
        private void 保存图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
             savename = textBox1.Text + ".png";
            if(textBox1.Text.Equals(""))
            {
                savename = "QR.png";
            }
            pictureBox1.Image.Save(savename, ImageFormat.Png);
            toolStripStatusLabel1.Text = "已保存为 "+savename +" 。";
        }
        //open save dir
        private void 打开保存位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filepath fp = new Filepath();
            fp.Open(fp.path+savename);
            toolStripStatusLabel1.Text = "打开"+ fp.path + savename;
            Clipboard.SetText(fp.path + savename);

        }
        //open about
        private void 帮助ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 childForm = new Form2();
            childForm.Show();
        }
    }
}
