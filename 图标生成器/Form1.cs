using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 图标生成器
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            int xx = 0;
            if (radioButton1.Checked) xx = 32;
            if (radioButton2.Checked) xx = 64;
            if (radioButton3.Checked) xx = 128;
            if (radioButton4.Checked) xx = 256;
            Size size = new Size(xx, xx);
            string img1 = s[0];
            string[] sname=img1.Split('\\');
            string[] wname = sname[sname.Length - 1].Split('.');
            //MessageBox.Show(img1);
            createico(img1, size, wname[0]);
            label1.Text = "生成成功";
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }
        void createico(string img,Size size,string savename)
        {
            Image bmico = new Bitmap(new Bitmap(img), size);
            MemoryStream bitmapstream = new MemoryStream();
            MemoryStream icostream = new MemoryStream();
            bmico.Save(bitmapstream, ImageFormat.Png);
            BinaryWriter binw = new BinaryWriter(icostream);
            binw.Write((short)0);
            binw.Write((short)1);
            binw.Write((short)1);
            binw.Write((byte)bmico.Width);
            binw.Write((byte)bmico.Height);
            binw.Write((short)0);
            binw.Write((short)0);
            binw.Write((short)32);
            binw.Write((int)bitmapstream.Length);
            binw.Write(22);
            binw.Write(bitmapstream.ToArray());
            binw.Flush();
            binw.Seek(0, SeekOrigin.Begin);
            string path = System.Environment.CurrentDirectory + "\\" + savename + ".ico";
            Stream iconfilestream = new FileStream(path, FileMode.Create);
            Icon ico = new Icon(icostream);
            ico.Save(iconfilestream);
            iconfilestream.Close();
            binw.Close();
            icostream.Close();
            bitmapstream.Close();
            ico.Dispose();
            bmico.Dispose();
        }


 
    }
}
