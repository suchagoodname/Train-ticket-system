using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpSQL课设
{
    public partial class 注册及信息界面 : Form
    {
        public 注册及信息界面()
        {
            InitializeComponent();
            this.Load += MainForm_Load1;
            this.SizeChanged += MainForm_SizeChanged;
            
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("一旦注册将不可更改信息，请牢记您的用户名和密码！");
            //检查用户是否已经被注册
            string sql0 = "Select * from 用户信息表 where 用户名='" + this.textBox1.Text + "'";
            directives directives0 = new directives();
            IDataReader reader = directives0.Read(sql0);
            if (reader.Read()) { MessageBox.Show("此用户以经存在，请重新输入用户名"); return; }
            //向数据库写入用户数据
            directives directives = new directives();
            string sql =$"insert into 用户信息表 values('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{textBox4.Text}','{textBox5.Text}')";
            int n = directives.Update(sql);
            if (n > 0) {
                MessageBox.Show("注册成功");
                if (directives.linkon())
                directives.X();
                foreach (Control item in this.Controls) {if (item is TextBox) item.Text = "";}
                this.Close();
            }
            else MessageBox.Show("注册失败！");
            if (directives.linkon())
                directives.X();
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

        private void 注册界面_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 注销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr=MessageBox.Show("注销将永久删除您的账号，确定要注销吗?","请确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK) { 
                directives directives= new directives();
                string sql = $"delete from 用户信息表 where 用户名='{DATA.userid}'";
                string str = $"delete from 车票预定信息表 where 用户名='{DATA.userid}'";
                int n=directives.Update(sql);
                if (n > 0) { MessageBox.Show("您的账号已注销，期待您的再次使用!");
                    directives.Update(str);
                    DATA.userid = "";
                this.Close();}
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
