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
    public partial class 增删管理员 : Form
    {
        public 增删管理员()
        {
            InitializeComponent();
            label3.Text = DATA.userid;
            label4.Text = DATA.username;
         
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "注销";
            textBox1.Visible = radioButton2.Checked;
            textBox2.Visible = radioButton2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Text = "添加";
            textBox1.Visible = radioButton2.Checked;
            textBox2.Visible = radioButton2.Checked;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string sql,str;
            directives directives=new directives();
            if (radioButton1.Checked) {//注销管理员
                DialogResult dr= MessageBox.Show("一旦注销，您将失去管理员权限，确定要注销吗","确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr != DialogResult.OK) return;
                sql = $"delete from 管理员信息表 where 账户名='{DATA.userid}' AND 密码='{DATA.username}'";
                int n=directives.Update(sql);
                if (n > 0) MessageBox.Show("注销成功！您已无管理员权限");
                DATA.userid = "";
                this.Close();
            }
            else {//新增管理员
                sql = $"insert into 管理员信息表 values ('{textBox1.Text}','{textBox2.Text}')";
                str = $"select * from 管理员信息表 where 账户名='{textBox1.Text}'";
                IDataReader reader= directives.Read(str);
                if (reader.Read()) { MessageBox.Show("此账户已存在，请更改账户名！");return; }
                reader.Close();
                int n=directives.Update(sql);
                DialogResult dx= DialogResult.Cancel;
                if (n>0)dx =MessageBox.Show("新增成功!需要继续添加管理员吗？","继续",MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dx != DialogResult.OK)
                this.Close();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
