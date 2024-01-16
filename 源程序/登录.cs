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
    public partial class 登录 : Form
    {
        public 登录()
        {
            InitializeComponent();
          
            this.textBox1.Text = DATA.userid;
            this.textBox2.Text = DATA.username;
        }
        

       private void button2_Click(object sender, EventArgs e)
        {
            DATA.userid = "";
            DATA.username = "";
            DATA.usersfz = "";
            DATA.userxm = "";
            this.Close();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            this.label4.Text = "正在登录...";
            if (!radioButton2.Checked)//不是游客
                if (textBox1.Text != "" && textBox1.Text != "")
                {
                    if (Login())
                    {
                        string sql = "Select * from 用户信息表 where 用户名='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                       
                        directives a = new directives();
                        IDataReader reader = a.Read(sql);
                        if (reader.Read())
                        {
                            this.label4.Text = "";
                            用户界面 userfrom = new 用户界面();
                            this.Hide();//隐藏登录窗口
                            userfrom.ShowDialog();
                            this.Show();
                        }
                    }else
                    if(Login1()) {
                        string sql = "Select * from 管理员信息表 where 账户名='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                        directives admini = new directives();
                        IDataReader reader = admini.Read(sql);
                        if (reader.Read())
                        {
                            this.label4.Text = "";
                            管理员界面 adminifrom = new 管理员界面();
                            this.Hide();
                            adminifrom.ShowDialog();
                            this.Show();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("请输入账号密码！");
                }
            else
            if (radioButton2.Checked)
            { 游客界面 visitor=new 游客界面();
                this.Hide();//隐藏登录窗口
                visitor.ShowDialog();
                this.Show();
            }
        }
        //验证登陆的账号密码
        public Boolean Login()
        {
            if (radioButton1.Checked)
            {
                string sql = "Select * from 用户信息表 where 用户名='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                directives a = new directives();
                IDataReader reader = a.Read(sql);
                DATA.userid = this.textBox1.Text;
                DATA.username = this.textBox2.Text;
                Boolean y = reader.Read();
                if (!y) this.label4.Text = "账户名或密码有误！";
                a.X();
                return y;
            }
            return false;
        }
        public Boolean Login1()
        {
            if (radioButton3.Checked)
            {
                string sql = "Select 账户名 from 管理员信息表 where  账户名='" + this.textBox1.Text + "' and 密码='" + this.textBox2.Text + "'";
                directives a = new directives();
                IDataReader reader = a.Read(sql);
                DATA.userid = this.textBox1.Text;
                DATA.username = this.textBox2.Text;
                Boolean y = reader.Read();
                if(!y) this.label4.Text = "账户名或密码有误！";
                a.X();
                return y;
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            注册及信息界面 zc = new 注册及信息界面();
            zc.ShowDialog();
            this.Show();
        }

        private void 登录_FormClosing(object sender, FormClosingEventArgs e)
        {
            DATA.userid = "";
            DATA.username = "";
            DATA.usersfz = "";
            DATA.userxm = "";
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label6.Visible= radioButton2.Checked;
        }
    }
}
