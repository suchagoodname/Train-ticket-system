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
    public partial class 游客界面 : Form
    {
        public 游客界面()
        {
            InitializeComponent();
            All();
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
       
        private void 游客界面_Load(object sender, EventArgs e)
        {

        }
        public void All()
        {
            dataGridView1.Rows.Clear();//清空旧数据
            directives directives = new directives();
            string sql = "select * from 车次信息表";
            IDataReader dr = directives.Read(sql);
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            directives.X();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text!="") {
                dataGridView1.Rows.Clear();
                string sql = "select * from 车次信息表 where 目的地=" + $"'{textBox1.Text}'";
                directives directives = new directives();
                IDataReader reader = directives.Read(sql);
                while (reader.Read())
                {
                    dataGridView1.Rows.Add(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString());
                }
                reader.Close();
                directives.X(); }
            else  All(); 
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
