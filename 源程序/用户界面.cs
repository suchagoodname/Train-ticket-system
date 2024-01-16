using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CSharpSQL课设
{
    public partial class 用户界面 : Form
    {
        public 用户界面()
        {
            InitializeComponent();
            button3.Visible = false;
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
        private void 用户界面_Load(object sender, EventArgs e)
        {
            All();
            label9.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label12.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label14.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            label15.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            label16.Text = DATA.userid;
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
     
        public void select()//查找用户所订票
        {
            dataGridView1.Rows.Clear();//清空旧数据
            directives directives = new directives();
            string sql = $"select 车次,出发地,出站时间,目的地,预计抵达时间,''d,车票价格,日期 FROM 车票预定信息表 where 用户名='{DATA.userid}' ";
                IDataReader dr = directives.Read(sql);
            if (dr.ToString() == "") {All(); return; }//无结果时显示全部
            while (dr.Read())
            {
                dataGridView1.Rows.Add(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            directives.X();
        }
        //public bool isHave()//判断是否已经购票
        //{
        //    directives directives = new directives();
        //    string sql = $"select 车次,出发地,出站时间,目的地,预计抵达时间,''d,车票价格,日期 FROM 车票预定信息表 where 用户名='{DATA.userid}' ";
        //    IDataReader dr = directives.Read(sql);
        //    if(dr.Read())return true;
        //    return false;
        //}
        private void button2_Click(object sender, EventArgs e)
        {
            //if (isHave()) { MessageBox.Show("不能购买多张车票！"); }
            //else
            {
                if (dataGridView1.SelectedRows.Count > 0)//选择了一项
                {
                    string sql2 = $"select 车次,出发地,出站时间,目的地,预计抵达时间,''d,车票价格,日期 FROM 车票预定信息表 where 用户名='{DATA.userid}' and 车次='{dataGridView1.SelectedRows[0].Cells[0].Value}'";
                    directives directives0 = new directives();
                    IDataReader reader = directives0.Read(sql2);
                    if (reader.Read()) { MessageBox.Show("不能购买多张车票！"); return; }
                    string sql = $"insert into 车票预定信息表 values('{DATA.userid}'," +
                          
                          $"'{dataGridView1.SelectedRows[0].Cells[0].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[1].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[2].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[3].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[4].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[5].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[6].Value}'," +
                          $"'{dataGridView1.SelectedRows[0].Cells[7].Value}')";
                  

                   directives directives = new directives(); 
                    if (int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString()) >=1)
                    {       int n = directives.Update(sql);
                            if (n > 0)
                            {
                                directives.X();
                                MessageBox.Show("订票成功");
                       //     int a = int.Parse(dataGridView1.SelectedRows[0].Cells[5].Value.ToString())-1;
                       //     string sql3 = $"update 车次信息表 set 车票数量='{dataGridView1.SelectedRows[0].Cells[5].Value}'" +
                       //$"where 车次='{dataGridView1.SelectedRows[0].Cells[0].Value}' and 用户名='{DATA.userid}'";
                       //     int m = directives.Update(sql3);
                        }
                    }
                        else MessageBox.Show("订票失败:售罄");
                    if(directives.linkon())directives.X();
                }
                else All();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) { 
            label9.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            label10.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            label11.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            label12.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            label13.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            label14.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            label15.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
           
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            select();
            button2.Visible=false;
            button3.Visible=true;
            button4.Visible=true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)//选择了一项
            {
                string sql = $"delete from 车票预定信息表 where 用户名='{DATA.userid }'and 车次='{dataGridView1.SelectedRows[0].Cells[0].Value}'";
                directives directives = new directives();
                int n = directives.Update(sql);
                if (n > 0)
                {
                    directives.X();
                    MessageBox.Show("退票成功");
                }
                if(directives.linkon())directives.X();
                button4_Click(sender, e);//完成操作后返回用户初始界面
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            All();
            button3.Visible = false;
            button4.Visible = false;
            button2.Visible = true;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //private void button5_Click(object sender, EventArgs e)
        //{//利用注册及信息界面来显示用户信息
        //    注册及信息界面 my = new 注册及信息界面();
        //    my.注销ToolStripMenuItem.Visible=true;
        //    my.Text = "用户信息";
        //    my.button1.Visible= false;
        //    my.button2.Visible= false;
        //    my.button3.Visible=true;
        //    my.label1.Text = "您的信息";
        //    my.textBox1.Text=DATA.userid;
        //    my.textBox2.Text = DATA.userxm;
        //    my.textBox3.Text = DATA.usersex;
        //    my.textBox4.Text = DATA.usersfz;
        //    my.textBox5.Text=DATA.username;
        //    my.label7.Text = "";
        //    my.label8.Text = "";
        //    my.label9.Text = "";
        //    my.label10.Text = "";
        //    my.label11.Text = "";
        //    my.textBox1.ReadOnly = true;
        //    my.textBox2.ReadOnly = true;
        //    my.textBox3.ReadOnly = true;
        //    my.textBox4.ReadOnly = true;
        //    my.textBox5.ReadOnly = true;
        //    this.Hide();
        //    my.ShowDialog();
        //    if (DATA.userid == "") { this.Close(); }
        //    else this.Show();
        //}
    }
}
