using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.IO.Directory;
using System.Drawing.Imaging;

namespace imagecrop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 불러오기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Title = "이미지 열기";
            dialog.Filter = "그림 파일 (*.jpg, *.gif, *.bmp) | *.jpg; *.gif; *.bmp; | 모든 파일 (*.*) | *.*";

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                listBox1.Items.Clear();
                foreach (String file in dialog.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        listBox1.Items.Add(file);
                    }
                    catch {}
                }
                label2.Text = listBox1.Items.Count.ToString();
            }
            else if (result == DialogResult.Cancel){}
        }

        private void 처리후저장ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(listBox1.Items.Count > 0)
            {
                string path = "OB_Gyn/" + DateTime.Now.ToString("yyyy-MM-dd. HH-mm-ss");
                DirectoryInfo dic = new DirectoryInfo(path);
                if (dic.Exists == false) dic.Create();

                for(int i=0; i<listBox1.Items.Count; i++)
                {
                    int y = 0;
                    if (radioButton1.Checked) y = 12;
                    else if (radioButton2.Checked) y = 50;
                    else if (radioButton3.Checked)
                    {
                        try
                        {
                            y = int.Parse(textBox2.Text);
                        }
                        catch
                        {
                            MessageBox.Show("값 에러");
                        }
                    }

                    string file = listBox1.Items[i].ToString();
                    String[] file_split = file.Split('\\');
                    string filename = file_split[file_split.Length - 1];
                    Bitmap image = new Bitmap(file);
                    Rectangle Rect = new Rectangle(0, y, image.Width, image.Height-y);
                    image = image.Clone(Rect, image.PixelFormat);
                    image.Save(path + "/" + filename, ImageFormat.Jpeg);
                }
                label2.Text = 0.ToString();
                listBox1.Items.Clear();
                MessageBox.Show("처리 완료");
            }
            else
            {
                MessageBox.Show("처리할 파일이 없습니다.");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked) textBox2.Enabled = true;
            else textBox2.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
