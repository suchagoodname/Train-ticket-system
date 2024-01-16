using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpSQL课设
{
    public partial class 添加车次 : Form
    {
        public string[] s = new string[8];
        public 添加车次()
        {
            InitializeComponent(); 
            this.Load += MainForm_Load1;
            this.SizeChanged += MainForm_SizeChanged;
        }
        //同登录页面自适应缩放
        Dictionary<string, Rectangle> normalControl = new Dictionary<string, Rectangle>();
        int oldformwidth = 0;
        int oldformheight = 0;
        private void MainForm_Load1(object sender, EventArgs e)
        {
            oldformwidth = this.Width;
            oldformheight = this.Height;

            foreach (Control item in this.Controls)
            {
                normalControl.Add(item.Name, new Rectangle(item.Left, item.Top, item.Width, item.Height));
            }
        }
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            int w = this.Width;
            int h = this.Height;
            foreach (Control item in this.Controls)
            {
                int newX = (int)(w * 1.0 / oldformwidth * normalControl[item.Name].X);
                int newY = (int)(h * 1.0 / oldformheight * normalControl[item.Name].Y);
                int newW = (int)(w * 1.0 / oldformwidth * normalControl[item.Name].Width);
                int newH = (int)(h * 1.0 / oldformheight * normalControl[item.Name].Height);
                item.Left = newX;
                item.Top = newY;
                item.Width = newW;
                item.Height = newH;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Resize += new EventHandler(Form1_Resize);
            X = this.Width;
            Y = this.Height;
            setTag(this);
            Form1_Resize(new object(), new EventArgs());
        }
        private float X;
        private float Y;
        private void setTag(Control cons)
        {
            foreach (Control con in cons.Controls)
            {
                con.Tag = con.Width + ":" + con.Height + ":" + con.Left + ":" + con.Top + ":" + con.Font.Size;
                if (con.Controls.Count > 0)
                    setTag(con);
            }
        }
        private void setControls(float newx, float newy, Control cons)
        {
            foreach (Control con in cons.Controls)
            {

                string[] mytag = con.Tag.ToString().Split(new char[] { ':' });
                float a = Convert.ToSingle(mytag[0]) * newx;
                con.Width = (int)a;
                a = Convert.ToSingle(mytag[1]) * newy;
                con.Height = (int)(a);
                a = Convert.ToSingle(mytag[2]) * newx;
                con.Left = (int)(a);
                a = Convert.ToSingle(mytag[3]) * newy;
                con.Top = (int)(a);
                Single currentSize = Convert.ToSingle(mytag[4]) * Math.Min(newx, newy);
                con.Font = new Font(con.Font.Name, currentSize, con.Font.Style, con.Font.Unit);
                if (con.Controls.Count > 0)
                {
                    setControls(newx, newy, con);
                }
            }

        }
        void Form1_Resize(object sender, EventArgs e)
        {
            float newx = (this.Width) / X;
            float newy = this.Height / Y;
            setControls(newx, newy, this);
        }//至此完成自适应缩放
        private void label1_Click(object sender, EventArgs e)
        {

        }
        void ins(string str,bool ins)//此函数对数据库进行修改或添加
        {
            string sql;
            directives directives = new directives();
            if (ins)//确定sql为添加或修改语句
            {
                sql = "insert into 车次信息表 values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "'," + textBox6.Text + "," + textBox7.Text + ",'" + textBox8.Text + "')";

            }
            else sql = $"update 车次信息表 set 车次='{textBox1.Text}',出发地='{textBox2.Text}',出站时间='{textBox3.Text}',目的地='{textBox4.Text}',预计抵达时间='{textBox5.Text}',车票数量='{textBox6.Text}',车票价格='{textBox7.Text}',日期='{textBox8.Text}' " +
                    $"where 车次='{s[0]}' and 出发地='{s[1]}' and 出站时间='{s[2]}'";
            int n = directives.Update(sql);
            if (n > 0)
            {
                MessageBox.Show(str+"成功，" + n + "行受影响");
                foreach (Control item in this.Controls) { if (item is TextBox) item.Text = ""; }
            }
            else MessageBox.Show(str+"失败！");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            bool unnull = textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "";
            if (!unnull) { MessageBox.Show("不能存在空值！");return; }
            if (unnull && this.Text == "添加车次")
            {
                ins("添加",true);
            }
            else if (unnull && this.Text == "修改信息")
            {
                bool isupdate = false;
                foreach (Control item in this.Controls)
                {
                    if (item is TextBox)
                        if (s[int.Parse(item.Name.Substring(7, 1))-1] != item.Text)
                        isupdate = true;  
                }
                if (!isupdate) { MessageBox.Show("未做出任何修改！"); return; }
                ins("修改", false); }
            this.Close();
           
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            { if (item is TextBox) item.Text = ""; }
            this.Close();
        }

        private void 添加车次_Load(object sender, EventArgs e)
        {

        }
    }
}
